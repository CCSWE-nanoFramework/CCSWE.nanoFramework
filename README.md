[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

# CCSWE.nanoFramework

A collection of libraries for [.NET nanoFramework](https://www.nanoframework.net/) targeting embedded/IoT devices such as ESP32 and STM32.

## Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) with the [.NET nanoFramework extension](https://marketplace.visualstudio.com/items?itemName=nanoframework.nanoFramework-VS2022-Extension) installed
- A supported nanoFramework device or the nanoFramework nanoCLR Win32 simulator

## Samples

Working examples for several libraries can be found in the [samples](samples/) directory:

- [Samples.CCSWE.nanoFramework.DhcpServer](samples/Samples.CCSWE.nanoFramework.DhcpServer) — DHCP server setup
- [Samples.CCSWE.nanoFramework.MdnsServer](samples/Samples.CCSWE.nanoFramework.MdnsServer) — mDNS service advertisement with a web controller
- [Samples.CCSWE.nanoFramework.NeoPixel](samples/Samples.CCSWE.nanoFramework.NeoPixel) — Driving a WS2812B/NeoPixel LED strip with animations
- [Samples.CCSWE.nanoFramework.Networking](samples/Samples.CCSWE.nanoFramework.Networking) — Shared WiFi/networking helpers used by other samples
- [Samples.CCSWE.nanoFramework.WebServer](samples/Samples.CCSWE.nanoFramework.WebServer) — Full web server with controllers, middleware, auth, and file serving

## Libraries included in this repository

## [CCSWE.nanoFramework.Collections.Concurrent](tree/master/src/CCSWE.nanoFramework.Collections.Concurrent) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Collections.Concurrent.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Collections.Concurrent/) 

Simple thread-safe collections. Includes `ConcurrentList` (a thread-safe `ArrayList`) and `ConcurrentQueue` (a thread-safe `Queue`).

## [CCSWE.nanoFramework.Configuration](tree/master/src/CCSWE.nanoFramework.Configuration) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Configuration.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Configuration/) 

Pluggable configuration management for nanoFramework applications. Defines `IConfigurationManager` (get/save/clear with change events), `IConfigurationStorage` (swappable backends), and `IValidateConfiguration`. Supports in-memory and file-system storage backends via `CCSWE.nanoFramework.FileStorage`.

## [CCSWE.nanoFramework.Core](tree/master/src/CCSWE.nanoFramework.Core) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Core.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Core/) 

Shared utility classes used across the CCSWE.nanoFramework libraries. Includes `Ensure`/`ThrowHelper` for argument validation, `StringExtensions` (case-insensitive compare, truncate), and reflection extension helpers.

## [CCSWE.nanoFramework.DhcpServer](tree/master/src/CCSWE.nanoFramework.DhcpServer) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.DhcpServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.DhcpServer/) 

A [RFC 2131](https://datatracker.ietf.org/doc/html/rfc2131) compliant DHCP server for nanoFramework. Supports the full DHCP message set, lease time with renewal and rebinding, captive portal URL, DNS server configuration, and an extensible option system. Written as a corrected rewrite of the official `Iot.Device.DhcpServer`. See the [DhcpServer sample](samples/Samples.CCSWE.nanoFramework.DhcpServer).

## [CCSWE.nanoFramework.FileStorage](tree/master/src/CCSWE.nanoFramework.FileStorage) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.FileStorage.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.FileStorage/) 

An `IFileStorage` abstraction over the device file system. Covers Create, Delete, Exists, GetDirectories, GetFiles, OpenRead, OpenWrite, OpenText, ReadAllBytes, ReadAllText, WriteAllBytes, and WriteAllText — making storage access testable and swappable.

## [CCSWE.nanoFramework.Graphics](tree/master/src/CCSWE.nanoFramework.Graphics) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Graphics.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Graphics/) 

Color utility library for nanoFramework. Provides `ColorConverter` (RGB/HSB/HSL conversion and brightness scaling), `ColorWheel` (255-step rainbow), and `ColorExtensions` (color-to-byte-array for any `ColorOrder`). Designed to pair with `CCSWE.nanoFramework.NeoPixel` but usable independently.

## [CCSWE.nanoFramework.Hosting](tree/master/src/CCSWE.nanoFramework.Hosting) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Hosting.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Hosting/) 

Hosting and startup infrastructure for nanoFramework applications, modelled after the .NET Generic Host. `DeviceHostBuilder`/`DeviceHost` wire up dependency injection with `IDeviceInitializer` support — initializers run before hosted services start and can abort startup on failure.

## [CCSWE.nanoFramework.Logging](tree/master/src/CCSWE.nanoFramework.Logging) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Logging.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Logging/) 

Logging infrastructure for nanoFramework backed by `Microsoft.Extensions.Logging`. Provides `ConsoleLogger` and `ConsoleLoggerFactory` with configurable minimum log level via `LoggerOptions`.

## [CCSWE.nanoFramework.Math](tree/master/src/CCSWE.nanoFramework.Math) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Math.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Math/) 

A fast, but not necessarily IEEE 754:2019 compliant, implementation of `System.Math` for nanoFramework. The primary optimization is removing NaN-handling overhead. Exposes a `FastMath` class with `Abs`, `Clamp`, `Max`, and `Min`. Any compliant improvements are contributed back to `nanoFramework/System.Math`.

## [CCSWE.nanoFramework.MdnsServer](tree/master/src/CCSWE.nanoFramework.MdnsServer) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.MdnsServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.MdnsServer/) 

A reusable mDNS responder for nanoFramework with dependency injection support. Automatically answers A, PTR, SRV, and TXT queries based on the device's registered hostname, IP address, and service instances. Integrates with `CCSWE.nanoFramework.Hosting` via `AddMdnsServer()`. See the [MdnsServer sample](samples/Samples.CCSWE.nanoFramework.MdnsServer).

## [CCSWE.nanoFramework.Mediator](tree/master/src/CCSWE.nanoFramework.Mediator) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Mediator.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Mediator/) 

A simple asynchronous publisher/subscriber mediator for nanoFramework. Events implement `IMediatorEvent`; handlers implement `IMediatorEventHandler`. Keeps publishers and subscribers fully decoupled. Supports singleton handlers registered via DI and transient handlers registered at runtime.

## [CCSWE.nanoFramework.NeoPixel](tree/master/src/CCSWE.nanoFramework.NeoPixel) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.NeoPixel.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.NeoPixel/) 

A fast ESP32 RMT library for controlling LED chipsets (NeoPixel, WS2812B, etc.). Significantly faster than the official `Ws28xx.Esp32` device. Supports multiple color orders via `NeoPixelStrip` and integrates with `CCSWE.nanoFramework.Graphics` for brightness scaling. See the [NeoPixel sample](samples/Samples.CCSWE.nanoFramework.NeoPixel).

## [CCSWE.nanoFramework.Threading](tree/master/src/CCSWE.nanoFramework.Threading) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Threading.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Threading/) 

Utilities to simplify thread management. Provides `ThreadPool` (general-purpose managed worker pool), `ConsumerThreadPool` (queue-backed pool that processes items via `ConsumerCallback`), and `WaitHandles` (multi-handle wait helpers).

## [CCSWE.nanoFramework.Threading.TestFramework](tree/master/src/CCSWE.nanoFramework.Threading.TestFramework) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Threading.TestFramework.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Threading.TestFramework/) 

Helpers for managing `ThreadPool` state in nanoFramework unit tests. `ThreadPoolTestHelper.ExecuteAndReset(action)` runs a test action and resets the pool on completion; `ThreadPoolManager.Reset()` reinitializes the pool between tests.

## [CCSWE.nanoFramework.WebServer](tree/master/src/CCSWE.nanoFramework.WebServer) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.WebServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.WebServer/) 

A simple asynchronous web server for nanoFramework modelled after ASP.NET Core. Features attribute-based controllers with routing and parameter binding, custom middleware via `IMiddleware`, a request thread pool, pluggable authentication via `IAuthenticationProvider`, and HTTPS support. See the [WebServer sample](samples/Samples.CCSWE.nanoFramework.WebServer).
