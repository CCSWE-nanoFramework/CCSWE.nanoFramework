using System;
using System.Drawing;

namespace Benchmarks.CCSWE.nanoFramework.NeoPixel;

internal static class BenchmarkData
{
    // ReSharper disable once InconsistentNaming
    private static readonly Random _random = new();

    public static readonly Color Color = GetRandomColor();
    public static readonly Color[] Colors = GetRandomColors(10);

    public const int IterationCount = 100;
    
    public static Color GetRandomColor()
    {
        return Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
    }

    public static Color[] GetRandomColors(byte count)
    {
        var colors = new Color[count];

        for (var i = 0; i < count; i++)
        {
            colors[i] = GetRandomColor();
        }

        return colors;
    }
}