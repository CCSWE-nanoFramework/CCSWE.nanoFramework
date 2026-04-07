using System;
using System.Drawing;

namespace CCSWE.nanoFramework.Graphics.Benchmarks.Reference;

/// <summary>
/// Reference implementation of the original <see cref="ColorConverter.ScaleBrightness"/> that
/// used an RGB -> HSB -> RGB round-trip. Preserved here to benchmark against the replacement
/// channel-multiply approach.
/// </summary>
internal static class ScaleBrightnessReference
{
    public static Color ScaleBrightness(Color color, float brightness)
    {
        var scale = FastMath.Clamp(brightness, 0.0f, 1.0f);

        // Convert to HSB with an overridden brightness value, then back to RGB.
        return HsbRoundTrip(color, scale);
    }

    private static Color HsbRoundTrip(Color color, float brightness)
    {
        // --- ToHsbColor (with brightness override) ---
        var r = color.R / 255f;
        var g = color.G / 255f;
        var b = color.B / 255f;

        MinMax(out var min, out var max, r, g, b);
        var delta = max - min;

        var hue = 0f;
        var saturation = 0f;
        var brightnessHsb = brightness * 100f;

        if (max == 0 || delta == 0)
        {
            saturation = 0;
        }
        else if (min == 0)
        {
            saturation = 100;
        }
        else if (max != 0)
        {
            saturation = delta / max * 100;
        }

        // ReSharper disable CompareOfFloatsByEqualityOperator
        if (max == 0 || delta == 0)
        {
            hue = 0;
        }
        else if (max == r && g >= b)
        {
            hue = 60 * (g - b) / delta;
        }
        else if (max == r && g < b)
        {
            hue = 60 * (g - b) / delta + 360;
        }
        else if (max == g)
        {
            hue = 60 * (b - r) / delta + 120;
        }
        else if (max == b)
        {
            hue = 60 * (r - g) / delta + 240;
        }
        // ReSharper restore CompareOfFloatsByEqualityOperator

        // --- ToColor(HsbColor) ---
        float red = 0, green = 0, blue = 0;
        var satF = saturation / 100.0f;
        var briF = brightnessHsb / 100.0f;

        if (FastMath.Abs(satF - 0) < float.Epsilon)
        {
            red = briF;
            green = briF;
            blue = briF;
        }
        else
        {
            var sectorPosition = hue / 60;
            var sectorNumber = (int)Math.Floor(sectorPosition);
            var fractionalSector = sectorPosition - sectorNumber;

            var p = briF * (1 - satF);
            var q = briF * (1 - satF * fractionalSector);
            var t = briF * (1 - satF * (1 - fractionalSector));

            switch (sectorNumber)
            {
                case 0:
                    red = briF;
                    green = t;
                    blue = p;
                    break;

                case 1:
                    red = q;
                    green = briF;
                    blue = p;
                    break;

                case 2:
                    red = p;
                    green = briF;
                    blue = t;
                    break;

                case 3:
                    red = p;
                    green = q;
                    blue = briF;
                    break;

                case 4:
                    red = t;
                    green = p;
                    blue = briF;
                    break;

                case 5:
                    red = briF;
                    green = p;
                    blue = q;
                    break;
            }
        }

        // Original used truncation (int cast without +0.5f rounding)
        return Color.FromArgb(color.A, (int)(red * 255), (int)(green * 255), (int)(blue * 255));
    }

    private static void MinMax(out float min, out float max, float r, float g, float b)
    {
        if (r > g)
        {
            max = r;
            min = g;
        }
        else
        {
            max = g;
            min = r;
        }

        if (b > max)
        {
            max = b;
        }
        else if (b < min)
        {
            min = b;
        }
    }
}
