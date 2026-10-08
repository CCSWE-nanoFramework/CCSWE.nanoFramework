[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework/blob/master/LICENSE.md) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.Graphics.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.Graphics/)

# CCSWE.nanoFramework.Graphics

Color utility library for .NET nanoFramework. Provides color conversion, brightness scaling, and a color wheel — usable independently of any LED hardware driver.

## API

### ColorConverter

Scale brightness.

```csharp
// Scale brightness (0.0 = off, 1.0 = full brightness)
Color dimRed = ColorConverter.ScaleBrightness(Color.Red, 0.5f);
```

### ColorWheel

A 255-step color wheel that cycles smoothly through red → green → blue → red. Useful for rainbow animations.

```csharp
// position: 0–255
Color c = ColorWheel.GetColor(position);
```

### ColorExtensions

Convert a `System.Drawing.Color` to a byte array in any `System.Drawing.ColorOrder` (Rgb, Bgr, Grb, etc.).

```csharp
byte[] bytes = color.ToBytes(ColorOrder.Grb);
```
