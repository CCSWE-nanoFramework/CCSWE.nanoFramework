namespace CCSWE.nanoFramework.MdnsServer;

/// <summary>
/// Options used to configure <see cref="MdnsServer"/>.
/// </summary>
public class MdnsServerOptions
{
    /// <summary>
    /// The default time-to-live in seconds for DNS records.
    /// </summary>
    public int DefaultTtl { get; set; } = 4500;
}

/// <summary>
/// A delegate for configuring <see cref="MdnsServerOptions"/>.
/// </summary>
/// <param name="options">The <see cref="MdnsServerOptions"/> to configure.</param>
public delegate void ConfigureMdnsServerOptions(MdnsServerOptions options);
