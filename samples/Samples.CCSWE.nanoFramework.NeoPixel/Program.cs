using System;
using System.Drawing;
using System.Threading;
using CCSWE.nanoFramework.Graphics;
using CCSWE.nanoFramework.NeoPixel;
using CCSWE.nanoFramework.NeoPixel.Drivers;
using ColorOrder = CCSWE.nanoFramework.NeoPixel.ColorOrder;

// ReSharper disable FunctionNeverReturns
// ReSharper disable RedundantArgumentDefaultValue

namespace Samples.CCSWE.nanoFramework.NeoPixel;

public class Program
{
    public static void Main()
    {
        // Configure the number of LEDs
        const ushort count = 47;

        // Adjust the pin
        const byte pin = 19;

        // Choose the correct driver and color order
        var driver = new Ws2812B(ColorOrder.GRB);

        // Create the strip
        var strip = new NeoPixelStrip(pin, count, driver);

        Console.WriteLine("Fill: Red");
        strip.Fill(Color.Red);
        strip.Update();
        Thread.Sleep(1000);

        Console.WriteLine("Fill: Green");
        strip.Fill(Color.Green);
        strip.Update();
        Thread.Sleep(1000);

        Console.WriteLine("Fill: Blue");
        strip.Fill(Color.Blue);
        strip.Update();
        Thread.Sleep(1000);

        while (true)
        {
            Console.WriteLine("FadeBrightness: White");
            FadeBrightness(strip, Color.White);
            Console.WriteLine("FadeBrightness: Red");
            FadeBrightness(strip, Color.Red);
            Console.WriteLine("FadeBrightness: Green");
            FadeBrightness(strip, Color.Green);
            Console.WriteLine("FadeBrightness: Blue");
            FadeBrightness(strip, Color.Blue);

            Console.WriteLine("ColorWipe: White");
            ColorWipe(strip, Color.White);
            Console.WriteLine("ColorWipe: Red");
            ColorWipe(strip, Color.Red);
            Console.WriteLine("ColorWipe: Green");
            ColorWipe(strip, Color.Green);
            Console.WriteLine("ColorWipe: Blue");
            ColorWipe(strip, Color.Blue);

            Console.WriteLine("TheaterChase: White");
            TheaterChase(strip, Color.White);
            Console.WriteLine("TheaterChase: Red");
            TheaterChase(strip, Color.Red);
            Console.WriteLine("TheaterChase: Green");
            TheaterChase(strip, Color.Green);
            Console.WriteLine("TheaterChase: Blue");
            TheaterChase(strip, Color.Blue);

            Console.WriteLine("Rainbow");
            Rainbow(strip);
            Console.WriteLine("RainbowCycle");
            RainbowCycle(strip);
            Console.WriteLine("TheaterChaseRainbow");
            TheaterChaseRainbow(strip);

            Console.WriteLine("SplitThirds");
            SplitThirds(strip);
        }
    }

    private static void FadeBrightness(NeoPixelStrip strip, Color color, short duration = 250)
    {
        var steps = 20;
        var brightness = 0.0f;
        var brightnessStep = 1.0f / steps;
        var stepDuration = (duration / steps) / 2;

        strip.Clear();
        strip.Update();

        for (var i = 0; i < steps; i++)
        {
            brightness += brightnessStep;

            strip.Fill(color, brightness);
            strip.Update();

            Thread.Sleep(stepDuration);
        }

        for (var i = 0; i < steps; i++)
        {
            brightness -= brightnessStep;

            strip.Fill(color, brightness);
            strip.Update();

            Thread.Sleep(stepDuration);
        }
    }

    private static void ColorWipe(NeoPixelStrip strip, Color color)
    {
        for (var i = 0; i < strip.Count; i++)
        {
            strip.SetLed(i, color);
            strip.Update();
        }
    }

    private static void Rainbow(NeoPixelStrip strip, int iterations = 1)
    {
        for (var i = 0; i < 255 * iterations; i++)
        {
            for (var j = 0; j < strip.Count; j++)
            {
                strip.SetLed(j, ColorWheel.GetColor((i + j) & 255));
            }

            strip.Update();
        }
    }

    private static void RainbowCycle(NeoPixelStrip strip, int iterations = 1)
    {
        for (var i = 0; i < 255 * iterations; i++)
        {
            for (var j = 0; j < strip.Count; j++)
            {
                strip.SetLed(j, ColorWheel.GetColor(((j * 255 / strip.Count) + i) & 255));
            }

            strip.Update();
        }
    }

    private static void TheaterChase(NeoPixelStrip strip, Color color, int iterations = 10)
    {
        for (var i = 0; i < iterations; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                for (var k = 0; k < strip.Count; k += 3)
                {
                    if (j + k < strip.Count)
                    {
                        strip.SetLed(j + k, color);
                    }
                }

                strip.Update();
                Thread.Sleep(100);

                for (var k = 0; k < strip.Count; k += 3)
                {
                    if (j + k < strip.Count)
                    {
                        strip.SetLed(j + k, Color.Black);
                    }
                }
            }
        }
    }

    private static void TheaterChaseRainbow(NeoPixelStrip strip)
    {
        for (var i = 0; i < 255; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                for (var k = 0; k < strip.Count; k += 3)
                {
                    if (k + j < strip.Count)
                    {
                        strip.SetLed(k + j, ColorWheel.GetColor((k + i) % 255));
                    }
                }

                strip.Update();
                Thread.Sleep(100);

                for (var k = 0; k < strip.Count; k += 3)
                {
                    if (k + j < strip.Count)
                    {
                        strip.SetLed(k + j, Color.Black);
                    }
                }
            }
        }
    }

    private static void SplitThirds(NeoPixelStrip strip, short duration = 2000)
    {
        var third = strip.Count / 3;

        // Full brightness: one solid color per third
        strip.SetLeds(0, third - 1, Color.Red);
        strip.SetLeds(third, third * 2 - 1, Color.Green);
        strip.SetLeds(third * 2, strip.Count - 1, Color.Blue);
        strip.Update();
        Thread.Sleep(duration);

        // Half brightness using the brightness overload — same three colors, scaled once per section
        strip.SetLeds(0, third - 1, Color.Red, 0.5f);
        strip.SetLeds(third, third * 2 - 1, Color.Green, 0.5f);
        strip.SetLeds(third * 2, strip.Count - 1, Color.Blue, 0.5f);
        strip.Update();
        Thread.Sleep(duration);
    }
}
