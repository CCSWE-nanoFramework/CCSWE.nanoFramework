using System;
using System.Diagnostics;
using System.Threading;
using CCSWE.nanoFramework.Hosting;
using nanoFramework.Runtime.Native;

namespace CCSWE.nanoFramework.Samples.Networking
{
    /// <summary>
    /// An <see cref="IDeviceInitializer"/> that connects the device to a WiFi network before any
    /// hosted services start. Returns <see langword="false"/> on connection failure, which causes
    /// the host to abort startup.
    /// </summary>
    public class NetworkInitializer : IDeviceInitializer
    {
        private readonly NetworkConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="NetworkInitializer"/>.
        /// </summary>
        /// <param name="configuration">The WiFi credentials to use when connecting.</param>
        public NetworkInitializer(NetworkConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public bool Initialize()
        {
            var needReboot = false;

            // Embedded devices often boot with the wireless access point (AP) mode enabled.
            // AP mode and client (station) mode can conflict, so disable AP mode first.
            if (WirelessAccessPointManager.IsEnabled())
            {
                Console.WriteLine("Wireless access point is enabled. Disabling.");
                WirelessAccessPointManager.Disable();

                needReboot = true;
            }

            // Ensure WiFi client (station) mode is enabled so the device can join a network.
            if (!WirelessClientManager.IsEnabled())
            {
                Console.WriteLine("Wireless client is disabled. Enabling.");
                WirelessClientManager.Enable();

                needReboot = true;
            }

            // Network configuration changes require a device reboot to take effect.
            if (needReboot)
            {
                Reboot();
            }

            Console.WriteLine($"Connecting to {_configuration.Ssid}...");

            // WirelessClientManager.Connect() uses WifiNetworkHelper.ConnectDhcp() internally.
            // It blocks for up to 60 seconds waiting for the connection and an IP address.
            if (!WirelessClientManager.Connect(_configuration.Ssid, _configuration.Password))
            {
                Console.WriteLine("Failed to connect to WiFi. Halting.");

                return false;
            }

            return true;
        }

        private static void Reboot()
        {
            Console.WriteLine("Rebooting...");

            if (Debugger.IsAttached)
            {
                // Power.RebootDevice() does nothing while a debugger is attached.
                // Prompt the user to power-cycle the board manually instead.
                Console.WriteLine("Device will not reboot while debugger attached.");
                Console.WriteLine("Please power cycle device.");
            }

            Power.RebootDevice();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
