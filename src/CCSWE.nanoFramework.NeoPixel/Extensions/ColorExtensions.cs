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
    /// <param name="color">The <see cref="Color"/> to convert.</param>
    /// <param name="order">The NeoPixel <see cref="ColorOrder"/> byte arrangement.</param>
    /// <returns>A 3-element byte array with the color channels in the requested order.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="order"/> is not a recognized <see cref="ColorOrder"/> value.</exception>
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
