using System;
using System.Drawing;

namespace CCSWE.nanoFramework.Graphics;

/// <summary>
/// Extension methods for <see cref="Color"/>
/// </summary>
public static class ColorExtensions
{
    /// <summary>
    /// Converts a <see cref="Color"/> to a <see cref="T:byte[]"/> in the given <see cref="ColorOrder"/>.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert.</param>
    /// <param name="order">The <see cref="ColorOrder"/> byte arrangement.</param>
    /// <returns>A 3-element byte array with the color channels in the requested order.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="order"/> is not a recognized <see cref="ColorOrder"/> value.</exception>
    public static byte[] ToBytes(this Color color, ColorOrder order) =>
        order switch
        {
            ColorOrder.Bgr => [color.B, color.G, color.R],
            ColorOrder.Brg => [color.B, color.R, color.G],
            ColorOrder.Gbr => [color.G, color.B, color.R],
            ColorOrder.Grb => [color.G, color.R, color.B],
            ColorOrder.Rbg => [color.R, color.B, color.G],
            ColorOrder.Rgb => [color.R, color.G, color.B],
            _ => throw new ArgumentOutOfRangeException(nameof(order))
        };
}
