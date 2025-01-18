using System;
using System.Diagnostics;
using System.Threading;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.WebServer.Internal;
using CCSWE.nanoFramework.WebServer.Samples.Authentication;
using CCSWE.nanoFramework.WebServer.Samples.Controllers;
using CCSWE.nanoFramework.WebServer.Samples.Networking;
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

            // Routes require authentication by default if an AuthenticationHandler has been registered
            serviceCollection.AddAuthentication(typeof(QueryStringAuthenticationHandler));

            serviceCollection.AddController(typeof(AllowAnonymousController));
            serviceCollection.AddController(typeof(AuthenticatedController));

            serviceCollection.AddSingleton(typeof(ITestClass), typeof(TestClass));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var authenticationHandlers = serviceProvider.GetServices(typeof(AuthenticationHandlerDescriptor));
            var controllers = serviceProvider.GetServices(typeof(ControllerDescriptor));

            /*            foreach (var authenticationHandler in authenticationHandlers)
            {
                Console.WriteLine(authenticationHandler.ToString());
                //Console.WriteLine("Authentication handler: " + authenticationHandler.ImplementationType.FullName);
            }

            foreach (var controller in controllers)
            {
                Console.WriteLine(controller.ToString());
                //Console.WriteLine("Controller: " + controller.ImplementationType.FullName);
            }
*/
            var testClass = (ITestClass)serviceProvider.GetService(typeof(ITestClass));

            foreach (var controllerDescriptor in testClass.ControllerDescriptors)
            {
                Console.WriteLine("Controller: " + controllerDescriptor.ImplementationType.FullName);
            }

            var webServer = (IWebServer) serviceProvider.GetService(typeof(IWebServer));

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

    internal interface ITestClass
    {
        AuthenticationHandlerDescriptor[] AuthenticationHandlerDescriptors { get; }
        ControllerDescriptor[] ControllerDescriptors { get; }
    }

    /*
            AuthenticationHandlerDescriptors = (AuthenticationHandlerDescriptor[]) authenticationHandlerDescriptors.ConvertAll(typeof(AuthenticationHandlerDescriptor));
       ControllerDescriptors = (ControllerDescriptor[]) controllerDescriptors.ConvertAll(typeof(ControllerDescriptor));
     */
    internal class TestClass: ITestClass
    {
        public TestClass(AuthenticationHandlerDescriptor[] authenticationHandlerDescriptors, ControllerDescriptor[] controllerDescriptors)
        {
            AuthenticationHandlerDescriptors = authenticationHandlerDescriptors;

            AuthenticationHandlerDescriptors = (AuthenticationHandlerDescriptor[]) authenticationHandlerDescriptors.ToArray(typeof(AuthenticationHandlerDescriptor));
            ControllerDescriptors = (ControllerDescriptor[])controllerDescriptors.ToArray(typeof(ControllerDescriptor));
        }

        public AuthenticationHandlerDescriptor[] AuthenticationHandlerDescriptors { get; }
        public ControllerDescriptor[] ControllerDescriptors { get; }
    }
}
