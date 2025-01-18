using System.Net.NetworkInformation;

namespace CCSWE.nanoFramework.WebServer.Samples.Networking
{
    internal static class WirelessAccessPointManager
    {
        public static void Disable()
        {
            var networkInterface = NetworkInterfaceProvider.RequireInterface(NetworkInterfaceType.WirelessAP);
            networkInterface.EnableDhcp();

            var configuration = GetConfiguration();
            configuration.Options = WirelessAPConfiguration.ConfigurationOptions.Disable;
            configuration.SaveConfiguration();
        }

        private static WirelessAPConfiguration GetConfiguration()
        {
            var networkInterface = NetworkInterfaceProvider.RequireInterface(NetworkInterfaceType.WirelessAP);
            return WirelessAPConfiguration.GetAllWirelessAPConfigurations()[networkInterface.SpecificConfigId];
        }

        public static bool IsEnabled()
        {
            return (GetConfiguration().Options & WirelessAPConfiguration.ConfigurationOptions.Enable) == WirelessAPConfiguration.ConfigurationOptions.Enable;
        }
    }
}
