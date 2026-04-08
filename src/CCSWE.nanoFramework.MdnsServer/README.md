# CCSWE.nanoFramework.MdnsServer

A reusable mDNS responder server for [nanoFramework](https://nanoframework.net/) with dependency injection support.

## Usage

Register the mDNS server in your `DeviceHostBuilder`:

```csharp
builder.ConfigureServices((context, services) =>
{
    services.AddMdnsServer(options =>
    {
        options.DefaultTtl = 4500;
    });
});
```

Then configure and start the server after obtaining an IP address:

```csharp
var mdns = host.Services.GetRequiredService<IMdnsServer>();
mdns.Hostname = "my-device.local";
mdns.IPAddress = IPAddress.Parse("192.168.1.100");
mdns.AddService(new MdnsServiceRegistration("my-device", "_http._tcp.local", 80, "path=/"));
mdns.Start();
```
