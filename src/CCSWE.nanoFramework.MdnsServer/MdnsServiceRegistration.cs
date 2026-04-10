using System;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// Represents a registered mDNS service instance.
/// </summary>
public class MdnsServiceRegistration
{
    /// <summary>
    /// Creates a new instance of <see cref="MdnsServiceRegistration"/> without a specific instance name.
    /// When <see cref="InstanceName"/> is <see langword="null"/>, the device hostname will be used.
    /// </summary>
    /// <param name="serviceType">The service type (e.g., "_http._tcp.local").</param>
    /// <param name="port">The port the service listens on.</param>
    /// <param name="txt">Optional TXT record data (e.g., "path=/").</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceType"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="serviceType"/> is empty.</exception>
    public MdnsServiceRegistration(string serviceType, ushort port, string? txt = null)
        : this(null, serviceType, port, txt)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="MdnsServiceRegistration"/>.
    /// </summary>
    /// <param name="instanceName">The service instance name (e.g., "my-device"). If <see langword="null"/>, the device hostname will be used.</param>
    /// <param name="serviceType">The service type (e.g., "_http._tcp.local").</param>
    /// <param name="port">The port the service listens on.</param>
    /// <param name="txt">Optional TXT record data (e.g., "path=/").</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceType"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="serviceType"/> is empty.</exception>
    public MdnsServiceRegistration(string? instanceName, string serviceType, ushort port, string? txt = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(serviceType);

        InstanceName = instanceName;
        ServiceType = serviceType;
        Port = port;
        Txt = txt;
    }

    /// <summary>
    /// Gets the service instance name (e.g., "my-device"). If <see langword="null"/>, the device hostname will be used.
    /// </summary>
    public string? InstanceName { get; }

    /// <summary>
    /// Gets the port the service listens on.
    /// </summary>
    public ushort Port { get; }

    /// <summary>
    /// Gets or sets the SRV record priority. Default is 0.
    /// </summary>
    public ushort Priority { get; set; }

    /// <summary>
    /// Gets the service type (e.g., "_http._tcp.local").
    /// </summary>
    public string ServiceType { get; }

    /// <summary>
    /// Gets or sets the TTL override in seconds. If <c>0</c>, uses <see cref="MdnsServerOptions.DefaultTtl"/>.
    /// </summary>
    public int Ttl { get; set; }

    /// <summary>
    /// Gets or sets the TXT record data (e.g., "path=/").
    /// </summary>
    public string? Txt { get; set; }

    /// <summary>
    /// Gets or sets the SRV record weight. Default is 0.
    /// </summary>
    public ushort Weight { get; set; }

    /// <summary>
    /// Gets the fully qualified service instance name (e.g., "my-device._http._tcp.local").
    /// Uses <see cref="InstanceName"/> if set; otherwise falls back to <paramref name="hostname"/>.
    /// </summary>
    /// <param name="hostname">The hostname to use as the instance name when <see cref="InstanceName"/> is <see langword="null"/>.</param>
    /// <returns>The fully qualified service instance name.</returns>
    public string GetFullyQualifiedName(string hostname) => $"{InstanceName ?? hostname}.{ServiceType}";
}
