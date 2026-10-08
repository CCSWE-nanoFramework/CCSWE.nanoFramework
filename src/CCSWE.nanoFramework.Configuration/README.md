[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/blob/master/LICENSE.md) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Configuration.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Configuration/) 

# CCSWE.nanoFramework.Configuration

Pluggable configuration management for nanoFramework applications. Configuration objects are loaded from a storage backend, validated, and surfaced through `IConfigurationManager`. A `ConfigurationChanged` event notifies listeners when a section is updated.

## API

### `IConfigurationManager`

The primary interface for reading and writing configuration:

- `Get(string section)` — retrieve a configuration object by section name (cast to the expected type)
- `Save(string section, object configuration)` / `SaveAsync(...)` — persist a configuration object
- `Clear()` / `Clear(string section)` — reset sections to their defaults and delete them from storage
- `Contains(string section)` — check whether a section is registered
- `GetSections()` — list all registered section names
- `ConfigurationChanged` — event raised after a section is saved or cleared

### `IConfigurationStorage`

Pluggable backend that the `ConfigurationManager` delegates persistence to:

- `ReadConfiguration(string section, Type type)` — read configuration data
- `WriteConfiguration(string section, object configuration)` — write configuration data
- `DeleteConfiguration(string section)` — remove a section

The default backend stores configuration in the device's internal file system (`I:\`).

### `IValidateConfiguration`

Implement and pass to `BindConfiguration` to validate a configuration before it is saved:

- `Validate(object? configuration)` — returns `ValidateConfigurationResult`; a failed result aborts the save with `ValidateConfigurationException`

### `ConfigurationManagerOptions`

Options passed during DI registration:

- `UseInternalStorage` — when `true` (default), uses the internal `I:\` file system

### DI Registration

Define a configuration class and bind it with defaults during startup:

```csharp
public class WifiConfiguration
{
    public string Ssid { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

// In ConfigureServices:
services.AddConfigurationManager()
        .BindConfiguration("wifi", new WifiConfiguration { Ssid = "MyNetwork" });
```

Retrieve and save via `IConfigurationManager` at runtime:

```csharp
// Read the current configuration
var config = (WifiConfiguration)configurationManager.Get("wifi");

// Persist changes
config.Password = "secret";
configurationManager.Save("wifi", config);
```

To use a custom storage backend instead of the internal `I:\` drive:

```csharp
services.AddConfigurationManager(options => options.UseInternalStorage = false);
services.AddSingleton(typeof(IConfigurationStorage), typeof(YourConfigurationStorage));
```
