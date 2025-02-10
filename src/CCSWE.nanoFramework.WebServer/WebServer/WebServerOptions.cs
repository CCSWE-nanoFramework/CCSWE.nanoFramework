using System.Security.Cryptography.X509Certificates;

namespace CCSWE.nanoFramework.WebServer;

/// <summary>
/// Options used to configure <see cref="WebServer"/>.
/// </summary>
// TODO: Add option to immediately build pipeline?
public class WebServerOptions
{
    /// <summary>
    /// The <see cref="X509Certificate"/> used for HTTPS requests.
    /// </summary>
    public X509Certificate? Certificate { get; set; }

    /// <summary>
    /// The port the <see cref="WebServer"/> listens on.
    /// </summary>
    public ushort Port { get; set; } = 80;

    /// <summary>
    /// The <see cref="HttpProtocol"/> to use.
    /// </summary>
    public HttpProtocol Protocol { get; set; } = HttpProtocol.Http;
}

/// <summary>
/// An action for configuring the <see cref="WebServer"/>.
/// </summary>
public delegate void ConfigureWebServerOptions(WebServerOptions options);