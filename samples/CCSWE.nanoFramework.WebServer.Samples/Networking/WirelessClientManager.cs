using nanoFramework.Networking;
using System.Net.NetworkInformation;
using System.Threading;

namespace CCSWE.nanoFramework.WebServer.Samples.Networking
{
    internal static class WirelessClientManager
    {
        public static bool Connect(string ssid, string password)
        {
            var cancellationTokenSource = new CancellationTokenSource(60_000);
            return WifiNetworkHelper.ConnectDhcp(ssid, password, requiresDateTime: true, token: cancellationTokenSource.Token);
        }

        public static void Enable()
        {
            var configuration = GetConfiguration();
            configuration.Options = Wireless80211Configuration.ConfigurationOptions.Enable;
            configuration.SaveConfiguration();
        }

        private static Wireless80211Configuration GetConfiguration()
        {
            var networkInterface = NetworkInterfaceProvider.RequireInterface(NetworkInterfaceType.Wireless80211);
            return Wireless80211Configuration.GetAllWireless80211Configurations()[networkInterface.SpecificConfigId];
        }

        public static bool IsEnabled()
        {
            return (GetConfiguration().Options & Wireless80211Configuration.ConfigurationOptions.Enable) == Wireless80211Configuration.ConfigurationOptions.Enable;
        }
    }
}
