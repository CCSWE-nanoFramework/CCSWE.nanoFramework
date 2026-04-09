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
    /// The hostname to advertise in A record responses (e.g. <c>"mydevice.local"</c>).
    /// When set, the <see cref="MdnsServer"/> will answer A queries for this name.
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
