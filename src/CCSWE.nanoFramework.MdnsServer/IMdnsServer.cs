using System.Net;

namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// An mDNS responder server that automatically handles A, PTR, SRV, and TXT queries.
/// </summary>
public interface IMdnsServer
{
    /// <summary>
    /// Gets or sets the hostname for A record responses (e.g., "my-device.local").
    /// </summary>
    string? Hostname { get; set; }

    /// <summary>
    /// Gets or sets the IP address for A record responses.
    /// </summary>
    IPAddress? IPAddress { get; set; }

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
    /// Removes a previously registered service instance.
    /// </summary>
    /// <param name="registration">The service registration to remove.</param>
    /// <returns><see langword="true"/> if the service was found and removed; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="registration"/> is <see langword="null"/>.</exception>
    bool RemoveService(MdnsServiceRegistration registration);

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
