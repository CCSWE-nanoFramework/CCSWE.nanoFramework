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
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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
