using System;
using CCSWE.nanoFramework.Hosting;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.Samples.Networking;
using CCSWE.nanoFramework.WebServer.Samples.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Samples;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Starting CCSWE.nanoFramework.WebServer.Samples");

        // TODO: Replace with your WiFi network credentials before deploying to a device.
        var networkConfig = new NetworkConfiguration("YOUR-SSID", "your_password");

        var host = DeviceHost.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.ConfigureServices(networkConfig);

                services.AddWebServer(options =>
                {
                    options.Port = 80;
                    options.Protocol = HttpProtocol.Http;
                });

                // Enable optional middleware
                services.AddCors();
                services.AddStaticFiles(typeof(ExampleFileProvider));

                services.AddAuthentication(typeof(ExampleAuthenticationHandler));

                // Add controllers individually
                services.AddController(typeof(ExampleController));

                // You can also use `AddControllers` to find all controllers through reflection
                //services.AddControllers();

                services.AddMiddleware(typeof(ExampleMiddleware));

                // Add your other dependencies
                services.AddSingleton(typeof(IDataService), typeof(DataService));

                services.AddHostedService(typeof(WebServerHostedService));
            })
            .Build();

        host.Run();
    }
}

public static class Boilerplate
{
    /// <summary>
    /// Registers services required by the sample that are not specific to the WebServer demonstration.
    /// </summary>
    public static IServiceCollection ConfigureServices(this IServiceCollection services, NetworkConfiguration networkConfig)
    {
        // Connects to WiFi before any IHostedService starts; aborts startup on failure.
        services.AddSingleton(typeof(NetworkConfiguration), networkConfig);
        services.AddSingleton(typeof(IDeviceInitializer), typeof(NetworkInitializer));

        // Trace shows all output — raise MinLogLevel in production.
        services.AddLogging(options =>
        {
            options.MinLogLevel = LogLevel.Trace;
        });

        return services;
    }
}
