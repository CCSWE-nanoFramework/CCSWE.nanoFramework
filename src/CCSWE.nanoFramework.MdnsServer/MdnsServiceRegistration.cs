using System;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// Represents a registered mDNS service instance.
/// </summary>
public class MdnsServiceRegistration
{
    /// <summary>
    /// Creates a new instance of <see cref="MdnsServiceRegistration"/>.
    /// </summary>
    /// <param name="instanceName">The service instance name (e.g., "my-device").</param>
    /// <param name="serviceType">The service type (e.g., "_http._tcp.local").</param>
    /// <param name="port">The port the service listens on.</param>
    /// <param name="txt">Optional TXT record data (e.g., "path=/").</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="instanceName"/> or <paramref name="serviceType"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="instanceName"/> or <paramref name="serviceType"/> is empty.</exception>
    public MdnsServiceRegistration(string instanceName, string serviceType, ushort port, string? txt = null)
    {
        ArgumentNullException.ThrowIfNull(instanceName);
        ArgumentNullException.ThrowIfNull(serviceType);

        if (instanceName.Length == 0)
        {
            throw new ArgumentException("Value cannot be empty.", nameof(instanceName));
        }

        if (serviceType.Length == 0)
        {
            throw new ArgumentException("Value cannot be empty.", nameof(serviceType));
        }

        InstanceName = instanceName;
        ServiceType = serviceType;
        Port = port;
        Txt = txt;
    }

    /// <summary>
    /// Gets the fully qualified service instance name (e.g., "my-device._http._tcp.local").
    /// </summary>
    public string FullyQualifiedName => $"{InstanceName}.{ServiceType}";

    /// <summary>
    /// Gets the service instance name (e.g., "my-device").
    /// </summary>
    public string InstanceName { get; }

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
}
