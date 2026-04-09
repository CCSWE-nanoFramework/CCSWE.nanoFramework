using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using CCSWE.nanoFramework.Samples.Networking;
using Microsoft.Extensions.Hosting;

namespace CCSWE.nanoFramework.MdnsServer.Samples.Services
{
    internal class MdnsServerHostedService : IHostedService
    {
        private readonly IMdnsServer _mdnsServer;

        public MdnsServerHostedService(IMdnsServer mdnsServer)
        {
            _mdnsServer = mdnsServer;
        }

        /// <inheritdoc />
        public void StartAsync(CancellationToken cancellationToken)
        {
            // By the time StartAsync is called, NetworkInitializer has already run and
            // WiFi is connected, so the device has a valid IP address to advertise.
            var wirelessInterface = NetworkInterfaceProvider.GetInterface(NetworkInterfaceType.Wireless80211);
            var localIp = wirelessInterface!.IPv4Address;

            // IPAddress is the IP to return in A record responses — the device's own IP.
            // Hostname and services were already applied from MdnsServerOptions at construction.
            _mdnsServer.IPAddress = IPAddress.Parse(localIp);

            _mdnsServer.Start();
        }

        /// <inheritdoc />
        public void StopAsync(CancellationToken cancellationToken)
        {
            _mdnsServer.Stop();
        }
    }
}
