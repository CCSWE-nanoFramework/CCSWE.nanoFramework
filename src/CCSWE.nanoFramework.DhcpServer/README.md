[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.DhcpServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.DhcpServer/) 

# CCSWE.nanoFramework.DhcpServer

A simple DHCP server for nanoFramework.

## Overview

This started as an effort to resolve some of the issues I was having with [Iot.Device.DhcpServer](https://github.com/nanoframework/nanoFramework.IoT.Device/tree/develop/devices/DhcpServer) and ending up turning into a complete re-write. I'll take to the team about backporting some of the fixes or using this code directly.

## Features

- Entire DHCP message set is supported per [RFC 2131](https://datatracker.ietf.org/doc/html/rfc2131)
- Lease time with renewal and rebinding options are supported
- Extensible option support with several data types supported out of the box

## Usage

The simplest usage is to create a new `DhcpServer` specifying the IP address of the server.

```csharp
var dhcpServer = new DhcpServer(new IPAddress(new byte[] { 192, 168, 4, 1 }));
dhcpServer.Start();
```

## Configuration

The `DhcpServer` constructor also has a parameter for the subnet mask that should be used. Internally the address pool only supports a class C (255.255.255.0) network which provides 253 addresses.

```csharp
var dhcpServer = new DhcpServer(new IPAddress(new byte[] { 192, 168, 4, 1 }), new IPAddress(new byte[] { 255, 255, 255, 0 }));
dhcpServer.Start();
```

The default `LeaseTime` is two hours but this can be adjusted as needed.

```csharp
dhcpServer.LeaseTime = TimeSpan.FromMinutes(5);
```

## DHCP options

Several DHCP options are exposed directly as properties:

- `CaptivePortalUrl`: Gets or sets the captive portal URL.
- `DnsServer`: Gets or sets the DNS server.

Additional options (including custom options) can be added to the `OptionCollection` exposed in the `Options` property.

```csharp
dhcpServer.Options.Add(...);
```

Options are defined using the `IOption` interface. Several strongly typed option implementations are provided:

- `IPAddressOption`: Represents a DHCP option with a single `IPAddress` value.
- `StringOption`: Represents a DHCP option with a `string` value.
- `TimeSpanOption`: Represents a DHCP option with a `TimeSpan` value.

If none of these types meet your needs you can implement the `IOption` interface or use the base `Option` class which handles common logic for you.

## Fixes (notes for myself for back porting)

- Proper handling of unicast and broadcast requests and repsonses
- Address pool expiration no longer crashes
- Captive portal uses the correct option code
