using System.Net;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// An mDNS responder server that automatically handles A, PTR, SRV, and TXT queries.
/// </summary>
public interface IMdnsServer
{
    /// <summary>
    /// Gets or sets the simple hostname of the device (e.g., <c>"my-device"</c>). Required for the server to function.
    /// The fully qualified domain name used in DNS records is derived as <c>{Hostname}.local</c>.
    /// </summary>
    string Hostname { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the device. Required for the server to function.
    /// Used in A record responses for <c>{Hostname}.local</c>.
    /// </summary>
    IPAddress IPAddress { get; set; }

    /// <summary>
    /// Gets a value indicating whether the server is running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Registers a service instance for mDNS advertisement.
    /// </summary>
    /// <param name="registration">The service registration to add.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="registration"/> is <see langword="null"/>.</exception>
    void AddService(MdnsServiceRegistration registration);

    /// <summary>
    /// Sends a gratuitous mDNS announcement with all registered services.
    /// All records (PTR, SRV, TXT, A) are placed in the answers section so that
    /// mDNS proxy/repeater devices on the network cache and forward the complete
    /// service information across subnet boundaries.
    /// </summary>
    void Announce();

    /// <summary>
    /// Starts the mDNS server.
    /// </summary>
    /// <returns><see langword="true"/> if started successfully; otherwise, <see langword="false"/>.</returns>
    bool Start();

    /// <summary>
    /// Stops the mDNS server.
    /// </summary>
    void Stop();
}
