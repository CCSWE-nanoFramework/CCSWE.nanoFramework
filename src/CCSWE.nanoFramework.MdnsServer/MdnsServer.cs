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
        _hostname = options.Hostname;
        _logger = logger;

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
        get => _hostname ?? throw new InvalidOperationException("Hostname has not been set");
        set
        {
            lock (_lock)
            {
                _hostname = value;
            }
        }
    }

    /// <inheritdoc />
    public IPAddress IPAddress
    {
        get => _ipAddress ?? throw new InvalidOperationException("IPAddress has not been set");
        set
        {
            lock (_lock)
            {
                _ipAddress = value;
            }
        }
    }

    /// <inheritdoc />
    public bool IsRunning => _started;

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
                ArgumentException.ThrowIfNullOrEmpty(_hostname);
                ArgumentNullException.ThrowIfNull(_ipAddress);

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

        string hostname;
        IPAddress ipAddress;
        MdnsServiceRegistration[] services;

        lock (_lock)
        {
            hostname = Hostname;
            ipAddress = IPAddress;
            services = (MdnsServiceRegistration[])_services.ToArray(typeof(MdnsServiceRegistration));
        }

        var hostDomain = $"{hostname}.local";
        var hostDomainLower = hostDomain.ToLower();
        
        var response = new MdnsResponse();
        var shouldRespond = false;

        foreach (var question in e.Message.GetQuestions())
        {
            var answered = false;
            
            var queryType = question.QueryType;
            var questionDomain = question.Domain;
            var questionDomainLower = questionDomain?.ToLower();

            if (queryType is DnsResourceType.A or DnsResourceType.ANY)
            {
                if (hostDomainLower.Equals(questionDomainLower))
                {
                    answered = true;
                    shouldRespond = true;
                    
                    response.AddAnswer(new ARecord(questionDomain, ipAddress, _defaultTtl));
                }
            }

            if (queryType is DnsResourceType.PTR or DnsResourceType.ANY)
            {
                if (ServiceEnumerationDomain.Equals(questionDomainLower))
                {
                    var addedTypes = new ArrayList();
                    
                    foreach (var registration in services)
                    {
                        var serviceType = registration.ServiceType;
                        var serviceTypeLower = serviceType.ToLower();
                        
                        if (addedTypes.Contains(serviceTypeLower))
                        {
                            continue;
                        }
                        
                        answered = true;
                        shouldRespond = true;
                        
                        addedTypes.Add(serviceTypeLower);
                        response.AddAnswer(new PtrRecord(questionDomain, serviceType, GetTtl(registration)));
                    }
                }
                else
                {
                    foreach (var registration in services)
                    {
                        var serviceType = registration.ServiceType;
                        var serviceTypeLower = serviceType.ToLower();
                        
                        if (!serviceTypeLower.Equals(questionDomainLower))
                        {
                            continue;
                        }
                        
                        answered = true;
                        shouldRespond = true;
                        
                        var fullyQualifiedInstance = registration.GetFullyQualifiedInstance(hostname);

                        response.AddAnswer(new PtrRecord(questionDomain, fullyQualifiedInstance, GetTtl(registration)));
                        response.AddAdditional(new SrvRecord(fullyQualifiedInstance, registration.Priority, registration.Weight, registration.Port, hostDomain, GetTtl(registration)));

                        if (!string.IsNullOrEmpty(registration.Txt))
                        {
                            response.AddAdditional(new TxtRecord(fullyQualifiedInstance, registration.Txt, GetTtl(registration)));
                        }

                        response.AddAdditional(new ARecord(hostDomain, ipAddress, GetTtl(registration)));
                    }
                }
            }

            if (queryType is DnsResourceType.SRV or DnsResourceType.ANY)
            {
                foreach (var registration in services)
                {
                    var fullyQualifiedInstanceLower = registration.GetFullyQualifiedInstance(hostname).ToLower();
                    if (!fullyQualifiedInstanceLower.Equals(questionDomainLower))
                    {
                        continue;
                    }

                    answered = true;
                    shouldRespond = true;

                    response.AddAnswer(new SrvRecord(questionDomain, registration.Priority, registration.Weight, registration.Port, hostDomain, GetTtl(registration)));
                    response.AddAdditional(new ARecord(hostDomain, ipAddress, GetTtl(registration)));
                }
            }

            if (queryType is DnsResourceType.TXT or DnsResourceType.ANY)
            {
                foreach (var registration in services)
                {
                    if (string.IsNullOrEmpty(registration.Txt))
                    {
                        continue;
                    }

                    var fullyQualifiedInstanceLower = registration.GetFullyQualifiedInstance(hostname).ToLower();
                    if (!fullyQualifiedInstanceLower.Equals(questionDomainLower))
                    {
                        continue;
                    }

                    answered = true;
                    shouldRespond = true;
                    
                    response.AddAnswer(new TxtRecord(questionDomain, registration.Txt, GetTtl(registration)));
                }
            }

            Log(LogLevel.Trace, $"Question: [{QueryTypeToString(queryType)}] {questionDomain}{(answered ? " - ANSWERED" : "")}");
        }

        e.Response = shouldRespond ? response : null;
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
        var services = (MdnsServiceRegistration[])_services.ToArray(typeof(MdnsServiceRegistration));

        if (services.Length == 0)
        {
            return;
        }

        var hostname = Hostname;
        var ipAddress = IPAddress;

        var hostDomain = $"{hostname}.local";

        var announcement = new MdnsResponse();
        var hostAnnounced = false;

        foreach (var registration in services)
        {
            var fullyQualifiedInstance = registration.GetFullyQualifiedInstance(hostname);

            announcement.AddAnswer(new PtrRecord(registration.ServiceType, fullyQualifiedInstance, GetTtl(registration)));
            announcement.AddAnswer(new SrvRecord(fullyQualifiedInstance, registration.Priority, registration.Weight, registration.Port, hostDomain, GetTtl(registration)));

            if (!string.IsNullOrEmpty(registration.Txt))
            {
                announcement.AddAnswer(new TxtRecord(fullyQualifiedInstance, registration.Txt, GetTtl(registration)));
            }

            if (!hostAnnounced)
            {
                announcement.AddAnswer(new ARecord(hostDomain, ipAddress, _defaultTtl));
                hostAnnounced = true;
            }
        }

        _multicastDnsService!.Send(announcement);
        
        Log(LogLevel.Information, "Announced");
    }
}
