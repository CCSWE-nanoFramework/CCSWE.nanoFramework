using System;
using System.Collections;
using System.Net;
using CCSWE.nanoFramework.MdnsServer.Internal;
using Iot.Device.MulticastDns;
using Iot.Device.MulticastDns.Entities;
using Iot.Device.MulticastDns.Enum;
using Iot.Device.MulticastDns.EventArgs;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// An mDNS responder server that wraps <see cref="MulticastDnsService"/> and automatically
/// handles A, PTR, SRV, and TXT queries based on registered hostname, IP address, and services.
/// </summary>
public sealed class MdnsServer : IMdnsServer, IDisposable
{
    private const string ServiceEnumerationDomain = "_services._dns-sd._udp.local";

    private readonly int _defaultTtl;
    private bool _disposed;
    private string? _fullyQualifiedHostname;
    private string? _fullyQualifiedHostnameLower;
    private string? _hostname;
    private IPAddress? _ipAddress;
    private readonly object _lock = new();
    private readonly ILogger? _logger;
    private MulticastDnsService? _multicastDnsService;
    private readonly ArrayList _services = new();
    private bool _started;

    /// <summary>
    /// Creates a new instance of <see cref="MdnsServer"/>.
    /// </summary>
    /// <param name="options">The <see cref="MdnsServerOptions"/> to configure this server.</param>
    /// <param name="logger">An optional <see cref="ILogger"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> is <see langword="null"/>.</exception>
    public MdnsServer(MdnsServerOptions options, ILogger? logger = null)
    {
        ArgumentNullException.ThrowIfNull(options);

        _defaultTtl = options.DefaultTtl;
        _logger = logger;

        SetHostname(options.Hostname, false);
        SetIPAddress(options.IPAddress, false);
        
        foreach (MdnsServiceRegistration registration in options.Services)
        {
            AddService(registration);
        }
    }

    /// <summary>
    /// Finalizes the <see cref="MdnsServer"/>.
    /// </summary>
    ~MdnsServer()
    {
        Dispose(false);
    }

    /// <inheritdoc />
    public string Hostname
    {
        get => CheckHostname(_hostname);
        set => SetHostname(value, true);
    }

    /// <inheritdoc />
    public IPAddress IPAddress
    {
        get => CheckIPAddress();
        set => SetIPAddress(value, true);
    }

    /// <inheritdoc />
    public bool IsRunning => _started;

    /// <inheritdoc />
    public void AddService(MdnsServiceRegistration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);

        lock (_lock)
        {
            _services.Add(registration);
        }
    }

    /// <inheritdoc />
    public void Announce()
    {
        lock (_lock)
        {
            if (_multicastDnsService is null)
            {
                return;
            }

            SendAnnouncement();
        }
    }

    private static string CheckHostname(string? value)
    {
        return string.IsNullOrEmpty(value) ? throw new InvalidOperationException("Hostname has not been set.") : value;
    }

    private IPAddress CheckIPAddress()
    {
        return _ipAddress ?? throw new InvalidOperationException("IPAddress has not been set.");
    }
    
    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        lock (_lock)
        {
            if (_disposed)
            {
                return;
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        Stop();

        if (disposing)
        {
        }

        _disposed = true;
    }

    private static string FormatLogMessage(string message) => $"[{nameof(MdnsServer)}] {message}";

    private int GetTtl(MdnsServiceRegistration registration) => registration.Ttl > 0 ? registration.Ttl : _defaultTtl;

    private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        if (e.Message is null)
        {
            return;
        }

        string fullyQualifiedHostname;
        string fullyQualifiedHostnameLower;
        string hostname;
        IPAddress ipAddress;
        MdnsServiceRegistration[] services;

        lock (_lock)
        {
            fullyQualifiedHostname = CheckHostname(_fullyQualifiedHostname);
            fullyQualifiedHostnameLower = CheckHostname(_fullyQualifiedHostnameLower);
            hostname = Hostname;
            ipAddress = IPAddress;
            services = (MdnsServiceRegistration[])_services.ToArray(typeof(MdnsServiceRegistration));
        }

        var fullyQualifiedInstanceNames = new string[services.Length];
        var fullyQualifiedInstanceNameLowers = new string[services.Length];
        for (var i = 0; i < services.Length; i++)
        {
            fullyQualifiedInstanceNames[i] = services[i].GetFullyQualifiedInstanceName(hostname);
            fullyQualifiedInstanceNameLowers[i] = fullyQualifiedInstanceNames[i].ToLower();
        }

        var response = new MdnsResponse();
        
        foreach (var question in e.Message.GetQuestions())
        {
            var domain = question.Domain;
            var domainLower = domain?.ToLower();
            var queryType = question.QueryType;

            if (queryType is DnsResourceType.A or DnsResourceType.ANY)
            {
                if (fullyQualifiedHostnameLower.Equals(domainLower))
                {
                    response.AddAnswer(new ARecord(domain, ipAddress, _defaultTtl));
                }
            }

            if (queryType is DnsResourceType.PTR or DnsResourceType.ANY)
            {
                if (ServiceEnumerationDomain.Equals(domainLower))
                {
                    var addedTypes = new ArrayList();
                    
                    foreach (var registration in services)
                    {
                        var serviceTypeLower = registration.ServiceTypeLower;

                        if (addedTypes.Contains(serviceTypeLower))
                        {
                            continue;
                        }

                        addedTypes.Add(serviceTypeLower);
                        response.AddAnswer(new PtrRecord(domain, registration.ServiceType, GetTtl(registration)));
                    }
                }
                else
                {
                    for (var i = 0; i < services.Length; i++)
                    {
                        var registration = services[i];

                        if (!registration.ServiceTypeLower.Equals(domainLower))
                        {
                            continue;
                        }

                        var fullyQualifiedInstance = fullyQualifiedInstanceNames[i];

                        response.AddAnswer(new PtrRecord(domain, fullyQualifiedInstance, GetTtl(registration)));
                        response.AddAdditional(new SrvRecord(fullyQualifiedInstance, registration.Priority, registration.Weight, registration.Port, fullyQualifiedHostname, GetTtl(registration)));

                        if (!string.IsNullOrEmpty(registration.Txt))
                        {
                            response.AddAdditional(new TxtRecord(fullyQualifiedInstance, registration.Txt, GetTtl(registration)));
                        }

                        response.AddAdditional(new ARecord(fullyQualifiedHostname, ipAddress, GetTtl(registration)));
                    }
                }
            }

            if (queryType is DnsResourceType.SRV or DnsResourceType.ANY)
            {
                for (var i = 0; i < services.Length; i++)
                {
                    if (!fullyQualifiedInstanceNameLowers[i].Equals(domainLower))
                    {
                        continue;
                    }

                    var registration = services[i];

                    response.AddAnswer(new SrvRecord(domain, registration.Priority, registration.Weight, registration.Port, fullyQualifiedHostname, GetTtl(registration)));
                    response.AddAdditional(new ARecord(fullyQualifiedHostname, ipAddress, GetTtl(registration)));
                }
            }

            if (queryType is DnsResourceType.TXT or DnsResourceType.ANY)
            {
                for (var i = 0; i < services.Length; i++)
                {
                    var registration = services[i];

                    if (string.IsNullOrEmpty(registration.Txt))
                    {
                        continue;
                    }

                    if (!fullyQualifiedInstanceNameLowers[i].Equals(domainLower))
                    {
                        continue;
                    }

                    response.AddAnswer(new TxtRecord(domain, registration.Txt, GetTtl(registration)));
                }
            }
        }

        e.Response = !response.IsEmpty() ? response : null;
    }

    private void Log(LogLevel logLevel, string message, Exception? exception = null)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        if (_logger is null)
        {
#if !DEBUG
            if (logLevel > LogLevel.Debug)
            {
#endif
                Console.WriteLine(FormatLogMessage(message));
#if !DEBUG
            }
#endif
            return;
        }

        _logger.Log(logLevel, exception, FormatLogMessage(message));
    }

    private static string QueryTypeToString(DnsResourceType queryType)
    {
        return queryType switch
        {
            DnsResourceType.A => "A",
            DnsResourceType.AAAA => "AAAA",
            DnsResourceType.ANY => "ANY",
            DnsResourceType.CNAME => "CNAME",
            DnsResourceType.PTR => "PTR",
            DnsResourceType.SRV => "SRV",
            DnsResourceType.TXT => "TXT",
            _ => $"Unknown ({(ushort)queryType})"
        };
    }

    private void SendAnnouncement()
    {
        if (_services.Count == 0)
        {
            return;
        }

        var services = (MdnsServiceRegistration[])_services.ToArray(typeof(MdnsServiceRegistration));

        var fullyQualifiedHostname = CheckHostname(_fullyQualifiedHostname);
        var hostname = Hostname;
        var ipAddress = IPAddress;

        var announcement = new MdnsResponse();

        announcement.AddAnswer(new ARecord(fullyQualifiedHostname, ipAddress, _defaultTtl));

        foreach (var registration in services)
        {
            var fullyQualifiedInstance = registration.GetFullyQualifiedInstanceName(hostname);

            announcement.AddAnswer(new PtrRecord(registration.ServiceType, fullyQualifiedInstance, GetTtl(registration)));
            announcement.AddAnswer(new SrvRecord(fullyQualifiedInstance, registration.Priority, registration.Weight, registration.Port, fullyQualifiedHostname, GetTtl(registration)));

            if (!string.IsNullOrEmpty(registration.Txt))
            {
                announcement.AddAnswer(new TxtRecord(fullyQualifiedInstance, registration.Txt, GetTtl(registration)));
            }
        }

        _multicastDnsService?.Send(announcement);

        Log(LogLevel.Information, "Announced");
    }

    private void SetHostname(string? value, bool required)
    {
        if (required && string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException("Hostname cannot be null or empty.");
        }
        
        lock (_lock)
        {
            _hostname = value;
            _fullyQualifiedHostname = value is not null ? $"{value}.local" : null;
            _fullyQualifiedHostnameLower = value is not null ? $"{value.ToLower()}.local" : null;
        }
    }

    private void SetIPAddress(IPAddress? value, bool required)
    {
        if (required && value is null)
        {
            throw new InvalidOperationException("IPAddress cannot be null.");
        }

        lock (_lock)
        {
            _ipAddress = value;
        }
    }

    /// <inheritdoc />
    public bool Start()
    {
        if (_started)
        {
            return true;
        }

        lock (_lock)
        {
            if (_started)
            {
                return true;
            }

            _started = true;

            try
            {
                CheckHostname(_hostname);
                CheckIPAddress();
                
                _multicastDnsService = new MulticastDnsService();
                _multicastDnsService.MessageReceived += HandleMessageReceived;
                _multicastDnsService.Start();

                SendAnnouncement();

                Log(LogLevel.Information, "Started");

                return true;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"Failed to start MDNS service. Message: {ex.Message}", ex);
                Stop();
            }
        }

        return false;
    }

    /// <inheritdoc />
    public void Stop()
    {
        lock (_lock)
        {
            if (_multicastDnsService is not null)
            {
                try
                {
                    _multicastDnsService.MessageReceived -= HandleMessageReceived;
                    _multicastDnsService.Stop();
                    _multicastDnsService.Dispose();
                    _multicastDnsService = null;
                }
                catch (Exception e)
                {
                    Log(LogLevel.Error, $"Error stopping mDNS service: {e.Message}", e);
                }
            }

            _started = false;
        }

        Log(LogLevel.Information, "Stopped");
    }
}
