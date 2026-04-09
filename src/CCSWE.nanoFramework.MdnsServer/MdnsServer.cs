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
    public string? Hostname
    {
        get => _hostname;
        set
        {
            lock (_lock)
            {
                _hostname = value;
            }
        }
    }

    /// <inheritdoc />
    public IPAddress? IPAddress
    {
        get => _ipAddress;
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
                _multicastDnsService = new MulticastDnsService();
                _multicastDnsService.MessageReceived += HandleMessageReceived;
                _multicastDnsService.Start();
                
                SendAnnouncement();

                Log(LogLevel.Information, "Started");

                return true;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"Exception occurred. Message: {ex.Message}", ex);
                Stop();
            }
        }

        return _started;
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

        string? hostname;
        IPAddress? ipAddress;
        MdnsServiceRegistration[] services;

        lock (_lock)
        {
            hostname = _hostname;
            ipAddress = _ipAddress;
            services = (MdnsServiceRegistration[])_services.ToArray(typeof(MdnsServiceRegistration));
        }

        var response = new MdnsResponse();
        var shouldRespond = false;

        foreach (var question in e.Message.GetQuestions())
        {
            var answered = false;
            
            var queryType = question.QueryType;
            var domain = question.Domain;
            var domainLower = domain?.ToLower();

            if (queryType is DnsResourceType.A or DnsResourceType.ANY)
            {
                if (hostname is not null && ipAddress is not null && domainLower == hostname.ToLower())
                {
                    answered = true;
                    shouldRespond = true;
                    
                    response.AddAnswer(new ARecord(domain, ipAddress, _defaultTtl));
                }
            }

            if (queryType is DnsResourceType.PTR or DnsResourceType.ANY)
            {
                if (domainLower == ServiceEnumerationDomain)
                {
                    var addedTypes = new ArrayList();

                    foreach (var registration in services)
                    {
                        if (addedTypes.Contains(registration.ServiceType))
                        {
                            continue;
                        }
                        
                        answered = true;
                        shouldRespond = true;
                        
                        addedTypes.Add(registration.ServiceType);
                        response.AddAnswer(new PtrRecord(domain, registration.ServiceType, GetTtl(registration)));
                    }
                }
                else
                {
                    foreach (var registration in services)
                    {
                        if (domainLower != registration.ServiceType.ToLower())
                        {
                            continue;
                        }
                        
                        answered = true;
                        shouldRespond = true;
                        
                        response.AddAnswer(new PtrRecord(domain, registration.FullyQualifiedName, GetTtl(registration)));
                        response.AddAdditional(new SrvRecord(registration.FullyQualifiedName, registration.Priority, registration.Weight, registration.Port, hostname ?? string.Empty, GetTtl(registration)));

                        if (!string.IsNullOrEmpty(registration.Txt))
                        {
                            response.AddAdditional(new TxtRecord(registration.FullyQualifiedName, registration.Txt, GetTtl(registration)));
                        }

                        if (hostname is not null && ipAddress is not null)
                        {
                            response.AddAdditional(new ARecord(hostname, ipAddress, GetTtl(registration)));
                        }
                    }
                }
            }

            if (queryType is DnsResourceType.SRV or DnsResourceType.ANY)
            {
                foreach (var registration in services)
                {
                    if (domainLower != registration.FullyQualifiedName.ToLower())
                    {
                        continue;
                    }
                    
                    answered = true;
                    shouldRespond = true;
                    
                    response.AddAnswer(new SrvRecord(domain, registration.Priority, registration.Weight, registration.Port, hostname ?? string.Empty, GetTtl(registration)));

                    if (hostname is not null && ipAddress is not null)
                    {
                        response.AddAdditional(new ARecord(hostname, ipAddress, GetTtl(registration)));
                    }
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


                    if (domainLower != registration.FullyQualifiedName.ToLower())
                    {
                        continue;
                    }
                    
                    answered = true;
                    shouldRespond = true;
                    
                    response.AddAnswer(new TxtRecord(domain, registration.Txt, GetTtl(registration)));
                }
            }

            Log(LogLevel.Trace, $"Question: [{QueryTypeToString(queryType)}] {domain}{(answered ? " - ANSWERED" : "")}");
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
        var hostname = _hostname;
        var ipAddress = _ipAddress;
        var services = (MdnsServiceRegistration[])_services.ToArray(typeof(MdnsServiceRegistration));

        if (services.Length == 0)
        {
            return;
        }

        var announcement = new MdnsResponse();
        var hostAnnounced = false;

        foreach (var registration in services)
        {
            announcement.AddAnswer(new PtrRecord(registration.ServiceType, registration.FullyQualifiedName, GetTtl(registration)));
            announcement.AddAnswer(new SrvRecord(registration.FullyQualifiedName, registration.Priority, registration.Weight, registration.Port, hostname ?? string.Empty, GetTtl(registration)));

            if (!string.IsNullOrEmpty(registration.Txt))
            {
                announcement.AddAnswer(new TxtRecord(registration.FullyQualifiedName, registration.Txt, GetTtl(registration)));
            }

            if (hostname is not null && ipAddress is not null && !hostAnnounced)
            {
                announcement.AddAnswer(new ARecord(hostname, ipAddress, _defaultTtl));
                hostAnnounced = true;
            }
        }

        _multicastDnsService!.Send(announcement);
        
        Log(LogLevel.Information, "Announced");
    }
}
