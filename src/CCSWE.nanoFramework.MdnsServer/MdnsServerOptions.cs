using System;
using System.Collections;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// Options used to configure <see cref="MdnsServer"/>.
/// </summary>
public class MdnsServerOptions
{
    private readonly ArrayList _services = new();

    /// <summary>
    /// The default time-to-live in seconds for DNS records.
    /// </summary>
    public int DefaultTtl { get; set; } = 4500;

    /// <summary>
    /// The simple hostname of the device (e.g., <c>"mydevice"</c>). Required for the server to function.
    /// The fully qualified domain name used in DNS records is derived as <c>{Hostname}.local</c>.
    /// </summary>
    public string? Hostname { get; set; }

    /// <summary>Gets the service registrations to apply when <see cref="MdnsServer"/> is constructed.</summary>
    internal ArrayList Services => _services;

    /// <summary>
    /// Registers a service to be advertised when the <see cref="MdnsServer"/> starts.
    /// </summary>
    /// <param name="registration">The <see cref="MdnsServiceRegistration"/> to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="registration"/> is <see langword="null"/>.</exception>
    public void AddService(MdnsServiceRegistration registration)
    {
        ArgumentNullException.ThrowIfNull(registration);

        _services.Add(registration);
    }
}

/// <summary>
/// A delegate for configuring <see cref="MdnsServerOptions"/>.
/// </summary>
/// <param name="options">The <see cref="MdnsServerOptions"/> to configure.</param>
public delegate void ConfigureMdnsServerOptions(MdnsServerOptions options);
