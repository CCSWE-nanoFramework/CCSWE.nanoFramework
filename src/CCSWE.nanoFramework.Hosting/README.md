[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Hosting.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Hosting/) 

# CCSWE.nanoFramework.Hosting

Hosting and startup infrastructure for nanoFramework applications, modelled after the .NET Generic Host. Provides a structured way to wire up dependency injection, run initialization logic, and start long-running background services.

## Startup sequence

1. Services are registered via `ConfigureServices`
2. `IDeviceInitializer` implementations run in registration order — if any returns `false`, startup is aborted
3. `IHostedService.StartAsync` is called for each registered hosted service

## API

### `DeviceHost`

Static factory with a single entry point:

```csharp
DeviceHost.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton(typeof(IDeviceInitializer), typeof(MyInitializer));
        services.AddHostedService(typeof(MyService));
    })
    .Build()
    .Run();
```

### `IDeviceInitializer`

Implement to run initialization logic before hosted services start:

```csharp
public class MyInitializer : IDeviceInitializer
{
    public bool Initialize()
    {
        // return false to abort startup
        return true;
    }
}
```

### `IHostedService`

Standard nanoFramework hosting interface for long-running background work. Register via `services.AddHostedService(typeof(MyService))`.
