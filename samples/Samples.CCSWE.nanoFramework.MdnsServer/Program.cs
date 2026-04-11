using System;
using CCSWE.nanoFramework.Hosting;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.MdnsServer;
using Samples.CCSWE.nanoFramework.MdnsServer.Services;
using Samples.CCSWE.nanoFramework.Networking;
using CCSWE.nanoFramework.WebServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Samples.CCSWE.nanoFramework.MdnsServer;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Starting Samples.CCSWE.nanoFramework.MdnsServer");

        // TODO: Replace with your WiFi network credentials before deploying to a device.
        var networkConfig = new NetworkConfiguration("YOUR-SSID", "your_password");

        var host = DeviceHost.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                // Registers WiFi, logging, and the web server (boilerplate for this sample).
                services.ConfigureServices(networkConfig);

                // AddMdnsServer registers the mDNS responder (224.0.0.251:5353).
                // It responds to A, PTR, SRV, and TXT queries for the configured hostname
                // and services per RFC 6762/6763.
                services.AddMdnsServer(options =>
                {
                    // Used for A record responses — clients resolve the device IP by name.
                    options.Hostname = "mdns-sample";

                    // Publishes PTR/SRV/TXT records so mDNS browsers (Bonjour, avahi-browse,
                    // dns-sd) can discover the HTTP service without knowing the device IP:
                    //
                    //   Instance name : "mdns-sample"       (human-readable label)
                    //   Service type  : "_http._tcp.local"  (standard DNS-SD type)
                    //   Port          : 80
                    //   TXT record    : "path=/"            (optional metadata)
                    options.AddService(new MdnsServiceRegistration(
                        serviceType: MdnsServiceType.Http,
                        port: 80,
                        txt: "path=/"));

                    // Custom service type — shows the API works for any protocol, not just HTTP.
                    // Standard browsers won't enumerate this type, but protocol-specific clients can.
                    options.AddService(new MdnsServiceRegistration(
                        serviceType: "_custom-service._tcp.local",
                        port: 80,
                        txt: "path=/"));
                });

                // Resolves the device IP after WiFi connects, then starts the mDNS responder.
                services.AddHostedService(typeof(MdnsServerHostedService));
            })
            .Build();

        host.Run();
    }
}

public static class Boilerplate
{
    /// <summary>
    /// Registers services required by the sample that are not specific to the mDNS demonstration.
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

        services.AddWebServer(options =>
        {
            options.Port = 80;
            options.Protocol = HttpProtocol.Http;
        });

        services.AddController(typeof(IndexController));
        services.AddHostedService(typeof(WebServerHostedService));

        return services;
    }
}
