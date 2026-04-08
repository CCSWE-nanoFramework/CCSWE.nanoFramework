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

    // ANY query type (255) is not in DnsResourceType enum
    private const DnsResourceType AnyQueryType = (DnsResourceType)255;

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
        _logger = logger;
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
    public bool RemoveService(MdnsServiceRegistration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);

        lock (_lock)
        {
            if (_services.Contains(registration))
            {
                _services.Remove(registration);
                return true;
            }
        }

        return false;
    }

    /// <inheritdoc />
    public bool Start()
    {
        if (!_started)
        {
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

                    Log(LogLevel.Information, "Started");

                    return true;
                }
                catch (Exception ex)
                {
                    Log(LogLevel.Error, $"Exception occurred. Message: {ex.Message}", ex);
                    Stop();
                }
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

        var answered = false;
        var response = new MdnsResponse();

        foreach (var question in e.Message.GetQuestions())
        {
            var queryType = question.QueryType;
            var domain = question.Domain;
            var domainLower = domain?.ToLower();

            Log(LogLevel.Trace, $"Question: [{QueryTypeToString(queryType)}] {domain}");

            if (queryType == DnsResourceType.A || queryType == AnyQueryType)
            {
                if (hostname is not null && ipAddress is not null && domainLower == hostname.ToLower())
                {
                    answered = true;
                    response.AddAnswer(new ARecord(domain, ipAddress, _defaultTtl));
                }
            }

            if (queryType == DnsResourceType.PTR || queryType == AnyQueryType)
            {
                if (domainLower == ServiceEnumerationDomain)
                {
                    var addedTypes = new ArrayList();

                    foreach (MdnsServiceRegistration registration in services)
                    {
                        if (!addedTypes.Contains(registration.ServiceType))
                        {
                            addedTypes.Add(registration.ServiceType);
                            answered = true;
                            response.AddAnswer(new PtrRecord(domain, registration.ServiceType, GetTtl(registration)));
                        }
                    }
                }
                else
                {
                    foreach (MdnsServiceRegistration registration in services)
                    {
                        if (domainLower == registration.ServiceType.ToLower())
                        {
                            answered = true;
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
            }

            if (queryType == DnsResourceType.SRV || queryType == AnyQueryType)
            {
                foreach (MdnsServiceRegistration registration in services)
                {
                    if (domainLower == registration.FullyQualifiedName.ToLower())
                    {
                        answered = true;
                        response.AddAnswer(new SrvRecord(domain, registration.Priority, registration.Weight, registration.Port, hostname ?? string.Empty, GetTtl(registration)));

                        if (hostname is not null && ipAddress is not null)
                        {
                            response.AddAdditional(new ARecord(hostname, ipAddress, GetTtl(registration)));
                        }
                    }
                }
            }

            if (queryType == DnsResourceType.TXT || queryType == AnyQueryType)
            {
                foreach (MdnsServiceRegistration registration in services)
                {
                    if (domainLower == registration.FullyQualifiedName.ToLower() && !string.IsNullOrEmpty(registration.Txt))
                    {
                        answered = true;
                        response.AddAnswer(new TxtRecord(domain, registration.Txt, GetTtl(registration)));
                    }
                }
            }
        }

        e.Response = answered ? response : null;
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
            DnsResourceType.PTR => "PTR",
            DnsResourceType.SRV => "SRV",
            DnsResourceType.TXT => "TXT",
            DnsResourceType.AAAA => "AAAA",
            DnsResourceType.CNAME => "CNAME",
            AnyQueryType => "ANY",
            _ => $"Unknown ({(ushort)queryType})"
        };
    }
}
