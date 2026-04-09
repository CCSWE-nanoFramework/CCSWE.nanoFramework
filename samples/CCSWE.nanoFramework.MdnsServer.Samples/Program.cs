using System;
using CCSWE.nanoFramework.Hosting;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.MdnsServer.Samples.Services;
using CCSWE.nanoFramework.Samples.Networking;
using CCSWE.nanoFramework.WebServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.MdnsServer.Samples
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Starting CCSWE.nanoFramework.MdnsServer.Samples");

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

                    // AddLogging registers the logging infrastructure. Trace level shows all log
                    // output, which is helpful while exploring the sample. Raise this in production.
                    services.AddLogging(options =>
                    {
                        options.MinLogLevel = LogLevel.Trace;
                    });

                    // AddWebServer registers the HTTP server. Port 80 is the default HTTP port.
                    // The server runs on a dedicated background thread — it does not block the host.
                    services.AddWebServer(options =>
                    {
                        options.Port = 80;
                        options.Protocol = HttpProtocol.Http;
                    });

                    // AddController registers a single controller class. The web server discovers
                    // routes by inspecting the [Route] and [HttpGet]/[HttpPost]/etc. attributes.
                    // IndexController handles GET / and returns "Hello World".
                    services.AddController(typeof(IndexController));

                    // AddMdnsServer registers the mDNS responder. It listens on the multicast
                    // address 224.0.0.251:5353 and responds to queries for registered service
                    // types and hostnames. Services registered here are applied automatically
                    // when the MdnsServer is constructed.
                    services.AddMdnsServer(options =>
                    {
                        // Hostname is used for A record responses. Clients on the LAN can resolve
                        // the device's IP address by name (e.g., ping mdns-sample.local).
                        options.Hostname = "mdns-sample.local";

                        // Register the HTTP service. This publishes a PTR, SRV, and TXT record
                        // so that mDNS browsers (like Bonjour, avahi-browse, or dns-sd) can
                        // find the service:
                        //
                        //   Instance name : "mdns-sample"        (human-readable service name)
                        //   Service type  : "_http._tcp.local"   (standard mDNS type for HTTP)
                        //   Port          : 80                   (where the web server listens)
                        //   TXT record    : "path=/"             (optional metadata)
                        //
                        // After registration, other devices on the LAN can browse for
                        // "_http._tcp.local" and find "mdns-sample" without knowing the IP.
                        options.AddService(new MdnsServiceRegistration(
                            instanceName: "mdns-sample",
                            serviceType: MdnsServiceType.Http,
                            port: 80,
                            txt: "path=/"));

                        // We'll also register a custom service type to show that mDNS works for
                        // any service, not just HTTP. This won't be discoverable by standard mDNS
                        // browsers since they look for specific types (like _http._tcp.local),
                        // but it demonstrates how to use the API for other protocols.
                        options.AddService(new MdnsServiceRegistration(
                            instanceName: "mdns-sample",
                            serviceType: "_custom-service._tcp.local",
                            port: 80,
                            txt: "path=/"));

                    });

                    // WebServerHostedService wraps IWebServer.Start()/Stop() so the host
                    // manages the web server lifetime alongside other hosted services.
                    services.AddHostedService(typeof(WebServerHostedService));

                    // MdnsServerHostedService resolves the device IP (after WiFi is up),
                    // sets the hostname and IP address, then starts the mDNS responder.
                    // Services were already registered via AddMdnsServer options above.
                    services.AddHostedService(typeof(MdnsServerHostedService));
                })
                .Build();

            // Run() starts all IDeviceInitializer and IHostedService instances in order,
            // then blocks the calling thread until the host is stopped.
            host.Run();
        }
    }
}
