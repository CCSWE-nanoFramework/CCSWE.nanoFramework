using System;
using System.Diagnostics;
using System.Threading;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.WebServer.Samples.Networking;
using CCSWE.nanoFramework.WebServer.Samples.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using nanoFramework.Runtime.Native;

namespace CCSWE.nanoFramework.WebServer.Samples
{
    public class Program
    {
        // TODO: Set your SSID and password here
        private const string Ssid = "YOUR-SSID";
        private const string Password = "your_password";

        public static void Main()
        {
            Console.WriteLine("Starting CCSWE.nanoFramework.WebServer.Samples");

            if (!InitializeNetwork())
            {
                Console.WriteLine("Failed to initialize network. Halting.");
                Thread.Sleep(Timeout.Infinite);
            }

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(options =>
            {
                options.MinLogLevel = LogLevel.Trace;
            });

            serviceCollection.AddWebServer(options =>
            {
                options.Port = 80;
                options.Protocol = HttpProtocol.Http;
            });

            // Enable optional middleware
            serviceCollection.AddCors();
            serviceCollection.AddStaticFiles(typeof(ExampleFileProvider));

            // Add AuthenticationHandler
            serviceCollection.AddAuthentication(typeof(ExampleAuthenticationHandler));

            // Add controllers
            serviceCollection.AddController(typeof(ExampleController));

            // Add custom middleware
            serviceCollection.AddMiddleware(typeof(ExampleMiddleware));

            // Add your other dependencies
            serviceCollection.AddSingleton(typeof(IDataService), typeof(DataService));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var webServer = (IWebServer) serviceProvider.GetService(typeof(IWebServer));

            // Start the web server
            webServer.Start();

            Thread.Sleep(Timeout.Infinite);
        }

        private static bool InitializeNetwork()
        {
            var needReboot = false;

            WirelessAccessPointManager.Disable();

            if (WirelessAccessPointManager.IsEnabled())
            {
                Console.WriteLine("Wireless access point is enabled. Disabling.");
                WirelessAccessPointManager.Disable();

                needReboot = true;
            }

            if (!WirelessClientManager.IsEnabled())
            {
                Console.WriteLine("Wireless client is disabled. Enabling.");
                WirelessClientManager.Enable();

                needReboot = true;
            }

            if (needReboot)
            {
                Reboot();
            }

            Console.WriteLine($"Connecting to {Ssid}...");

            return WirelessClientManager.Connect(Ssid, Password);
        }

        public static void Reboot()
        {
            Console.WriteLine("Rebooting...");

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Device will not reboot while debugger attached.");
                Console.WriteLine("Please power cycle device.");
            }

            Power.RebootDevice();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
