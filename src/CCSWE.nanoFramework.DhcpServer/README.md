[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.DhcpServer.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.DhcpServer/) 

# CCSWE.nanoFramework.DhcpServer

A simple DHCP server for nanoFramework.

## Overview

This started as an effort to resolve some of the issues I was having with [Iot.Device.DhcpServer](https://github.com/nanoframework/nanoFramework.IoT.Device/tree/develop/devices/DhcpServer) and ending up turning into a complete re-write. I'll take to the team about backporting some of the fixes or using this code directly.

## Fixes (notes for myself for back porting)

- Proper handling of unicast and broadcast requests and repsonses
- Address pool expiration no longer crashes
- Captive portal uses the correct option code

## Features

- Entire DHCP message set is supported per [RFC 2131](https://datatracker.ietf.org/doc/html/rfc2131)
- Lease time with renewal and rebinding options are supported
- Extensible option support with several data types supported out of the box
