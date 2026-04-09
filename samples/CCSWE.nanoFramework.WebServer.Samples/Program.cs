using System;
using CCSWE.nanoFramework.Hosting;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.Samples.Networking;
using CCSWE.nanoFramework.WebServer;
using CCSWE.nanoFramework.WebServer.Samples.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Samples
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Starting CCSWE.nanoFramework.WebServer.Samples");

            // TODO: Replace with your WiFi network credentials before deploying to a device.
            var networkConfig = new NetworkConfiguration("YOUR-SSID", "your_password");

            // DeviceHost.CreateDefaultBuilder() creates an IHostBuilder pre-configured with
            // logging and DI. ConfigureServices() registers application services; the builder
            // pattern lets you chain multiple registration calls.
            var host = DeviceHost.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    // NetworkInitializer runs before any IHostedService. It connects to WiFi
                    // and returns false on failure, which causes the host to abort startup.
                    services.AddSingleton(typeof(NetworkConfiguration), networkConfig);
                    services.AddSingleton(typeof(IDeviceInitializer), typeof(NetworkInitializer));

                    services.AddLogging(options =>
                    {
                        options.MinLogLevel = LogLevel.Trace;
                    });

                    services.AddWebServer(options =>
                    {
                        options.Port = 80;
                        options.Protocol = HttpProtocol.Http;
                    });

                    // Enable optional middleware
                    services.AddCors();
                    services.AddStaticFiles(typeof(ExampleFileProvider));

                    // Add AuthenticationHandler
                    services.AddAuthentication(typeof(ExampleAuthenticationHandler));

                    // Add controllers individually
                    services.AddController(typeof(ExampleController));

                    // You can also use `AddControllers` to find all controllers through reflection
                    //services.AddControllers();

                    // Add custom middleware
                    services.AddMiddleware(typeof(ExampleMiddleware));

                    // Add your other dependencies
                    services.AddSingleton(typeof(IDataService), typeof(DataService));

                    // WebServerHostedService wraps IWebServer.Start()/Stop() so the host
                    // manages the web server lifetime alongside other hosted services.
                    services.AddHostedService(typeof(WebServerHostedService));
                })
                .Build();

            // Run() starts all IDeviceInitializer and IHostedService instances in order,
            // then blocks the calling thread until the host is stopped.
            host.Run();
        }
    }
}
