[![Build](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.NeoPixel/actions/workflows/build-solution.yml/badge.svg)](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.NeoPixel/actions/workflows/build-solution.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/CCSWE.nanoFramework.NeoPixel.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/CCSWE.nanoFramework.NeoPixel/) 

# CCSWE.nanoFramework.NeoPixel

A fast ESP32 RMT library for controlling LED chipsets (NeoPixel, WS2812B, etc.)

I was originally using the [Ws28xx.Esp32](https://github.com/nanoframework/nanoFramework.IoT.Device/tree/develop/devices/Ws28xx.Esp32) but it was taking 600-700ms to update a strip of 50 LEDs. This was not fast enough for my use case so I set out to find a faster solution.

The [NeoPixelStripLowMemory](https://github.com/nanoframework/Samples/tree/main/samples/Hardware.Esp32.Rmt/NeoPixelStripLowMemory) sample code was much faster at 50-60ms to update the same strip so that's where I started. I've also been looking at the [FastLED](https://github.com/FastLED/FastLED) library as I've used that in the past and it is indeed fast.

I've never spent time learning how these LEDs actually work so I'm learning as I go along and would love feedback or contributions from others with more experience in this area.

## Usage
See [samples](https://github.com/CCSWE-nanoFramework/CCSWE.nanoFramework.NeoPixel/tree/master/CCSWE.nanoFramework.NeoPixel.Samples) for more details.

Initialize the strip
```c#
// Configure the number of LEDs
ushort count = 47;

// Adjust the pin number
byte pin = 19;

// Choose the correct driver and color order
var driver = new Ws2812B(ColorOrder.GRB);

// Create the strip
var strip = new NeoPixelStrip(pin, count, driver);
```

Set the LEDs
```c#
// Set all LEDs
strip.Fill(Color.Pink);

for (var i = 0; i < strip.Count; i++)
{
    if (i % 2 == 0)
    {
        // Set an individual LED
        strip.SetLed(i, Color.Blue);
    }
}

// Send the data to update the strip
strip.Update();
```

### Brightness scaling

Both the `Fill` and `SetPixel` methods have overloads that allow you to pass a brightness scaling factor. If you will be using the same color for multiple calls then it is more efficient to call `ColorConverter.ScaleBrightness(Color, double)` directly and use the pre-scaled `Color` across multiple calls.