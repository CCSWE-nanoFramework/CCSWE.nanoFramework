[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.WebServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.WebServer/) 

# CCSWE.nanoFramework.WebServer

A simple asynchronous web server for nanoFramework modelled after ASP.NET Core. See the [WebServer sample](tree/master/samples/Samples.CCSWE.nanoFramework.WebServer) for a complete example.

## Features

- Attribute-based controllers with routing and parameter binding
- Custom middleware via `IMiddleware`
- Request thread pool for concurrent request processing
- Pluggable authentication via `IAuthenticationProvider`
- HTTPS support

## Quick Start

### Controller

```csharp
[Route("api/status")]
public class StatusController : ControllerBase
{
    [HttpGet]
    public void Get()
    {
        Ok("running");
    }
}
```

### Middleware

```csharp
public class LoggingMiddleware : IMiddleware
{
    public void Invoke(HttpListenerContext context, MiddlewareDelegate next)
    {
        // pre-processing
        next(context);
        // post-processing
    }
}
```

### DI Registration

Register controllers individually or let reflection discover all controllers in an assembly:

```csharp
services.AddWebServer(options =>
{
    options.Port = 80;
})
.AddMiddleware(typeof(LoggingMiddleware))
.AddController(typeof(StatusController));  // register one controller explicitly
```

`AddControllers` uses reflection to scan an assembly and automatically register every class that derives from `ControllerBase`, eliminating the need to list them individually:

```csharp
services.AddWebServer(options =>
{
    options.Port = 80;
})
.AddMiddleware(typeof(LoggingMiddleware))
.AddControllers();                         // scans the executing assembly
// or target a specific assembly:
.AddControllers(Assembly.GetExecutingAssembly());
```
