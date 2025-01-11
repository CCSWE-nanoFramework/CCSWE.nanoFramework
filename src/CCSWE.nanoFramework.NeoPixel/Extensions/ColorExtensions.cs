using System;
using System.Drawing;

// ReSharper disable once CheckNamespace
namespace CCSWE.nanoFramework.NeoPixel
{
    /// <summary>
    /// Extension methods for <see cref="Color"/>
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a <see cref="Color"/> to a <see cref="T:byte[]"/> in the given <see cref="ColorOrder"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] ToBytes(this Color color, ColorOrder order)
        {
            return order switch
            {
                ColorOrder.RGB => new[] { color.R, color.G, color.B },
                ColorOrder.GRB => new[] { color.G, color.R, color.B },
                _ => throw new ArgumentOutOfRangeException(nameof(order))
            };
        }
    }
}
