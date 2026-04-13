[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Logging.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Logging/) 

# CCSWE.nanoFramework.Logging

Logging infrastructure for nanoFramework backed by `Microsoft.Extensions.Logging`. Provides a console logger implementation with configurable minimum log level.

## API

### `ConsoleLogger`

`ILogger` implementation that writes formatted log entries to the console. Create directly or via `ConsoleLoggerFactory`:

```csharp
var logger = ConsoleLogger.Create(new LoggerOptions { MinLogLevel = LogLevel.Information });
```

### `ConsoleLoggerFactory`

`ILoggerFactory` implementation that creates `ConsoleLogger` instances sharing the same options:

```csharp
var factory = new ConsoleLoggerFactory(new LoggerOptions { MinLogLevel = LogLevel.Warning });
ILogger logger = factory.CreateLogger("MyCategory");
```

### `LoggerOptions`

| Property | Default | Description |
|---|---|---|
| `MinLogLevel` | `LogLevel.Debug` | Messages below this level are suppressed |

### DI Registration

```csharp
services.AddLogging();
// or with options:
services.AddLogging(options => options.MinLogLevel = LogLevel.Information);
```
