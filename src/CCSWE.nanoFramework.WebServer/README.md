[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.WebServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.WebServer/) 

# CCSWE.nanoFramework.WebServer

A (not so) simple web server for .NET nanoFramework that tries to mimic the ASP.NET Core implementation.

## Features

- Controllers with support for routing and parameter binding
- Custom middleware support through `IMiddleware`
- Request thread pool for processing requests
- Flexible authentication through `IAuthenticationProvider`
- HTTPS support

## Usage

I'll add more documentation later, but for now you can check out the [sample project](tree/master/samples/CCSWE.nanoFramework.WebServer.Samples) for an example of how to use the web server.