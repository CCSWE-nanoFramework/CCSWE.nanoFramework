using System;
using System.Drawing;

namespace CCSWE.nanoFramework.NeoPixel;

/// <summary>
/// Extension methods for <see cref="Color"/> with NeoPixel <see cref="ColorOrder"/>.
/// </summary>
public static class ColorExtensions
{
    /// <summary>
    /// Converts a <see cref="Color"/> to a <see cref="T:byte[]"/> in the given NeoPixel <see cref="ColorOrder"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static byte[] ToBytes(this Color color, ColorOrder order)
    {
        var sdOrder = order switch
        {
            ColorOrder.GRB => System.Drawing.ColorOrder.Grb,
            ColorOrder.RGB => System.Drawing.ColorOrder.Rgb,
            _ => throw new ArgumentOutOfRangeException(nameof(order))
        };

        return CCSWE.nanoFramework.Graphics.ColorExtensions.ToBytes(color, sdOrder);
    }
}
