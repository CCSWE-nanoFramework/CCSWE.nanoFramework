using System.Threading;
using CCSWE.nanoFramework.WebServer;
using Microsoft.Extensions.Hosting;

namespace CCSWE.nanoFramework.MdnsServer.Samples.Services
{
    internal class WebServerHostedService : IHostedService
    {
        private readonly IWebServer _webServer;

        public WebServerHostedService(IWebServer webServer)
        {
            _webServer = webServer;
        }

        /// <inheritdoc />
        public void StartAsync(CancellationToken cancellationToken)
        {
            // The web server runs on its own background thread after Start() returns,
            // so this call does not block the hosted service startup sequence.
            _webServer.Start();
        }

        /// <inheritdoc />
        public void StopAsync(CancellationToken cancellationToken)
        {
            _webServer.Stop();
        }
    }
}
