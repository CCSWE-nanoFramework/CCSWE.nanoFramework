using System.Drawing;

namespace CCSWE.nanoFramework.NeoPixel
{
    // Based on: https://gist.github.com/UweKeim/fb7f829b852c209557bc49c51ba14c8b

    /// <summary>
    /// Represents an HSL color space.
    /// https://en.wikipedia.org/wiki/HSL_and_HSV
    /// </summary>
    internal readonly struct HslColor
    {
        public HslColor(float hue, float saturation, float light, byte alpha)
        {
            Hue = hue;
            Saturation = saturation;
            Light = light;
            Alpha = alpha;
        }

        /// <summary>
        /// Gets the hue. Values from 0 to 360.
        /// </summary>
        public float Hue { get; }

        /// <summary>
        /// Gets the saturation. Values from 0 to 100.
        /// </summary>
        public float Saturation { get; }

        /// <summary>
        /// Gets the light. Values from 0 to 100.
        /// </summary>
        public float Light { get; }

        /// <summary>
        /// Gets the alpha. Values from 0 to 255
        /// </summary>
        public byte Alpha { get; }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj is not HslColor color)
            {
                return false;
            }

            return FastMath.Abs(Hue - color.Hue) < float.Epsilon &&
                   FastMath.Abs(Saturation - color.Saturation) < float.Epsilon &&
                   FastMath.Abs(Light - color.Light) < float.Epsilon &&
                   Alpha == color.Alpha;
        }

        public static HslColor FromColor(Color color)
        {
            return ColorConverter.ToHslColor(color);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            // ReSharper disable once RedundantOverflowCheckingContext
            unchecked
            {
                return Hue.GetHashCode() ^ Saturation.GetHashCode() ^ Light.GetHashCode() ^ Alpha.GetHashCode();
            }
        }

        public Color ToColor()
        {
            return ColorConverter.ToColor(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Hue: {Hue}; Saturation: {Saturation}; Light: {Light}; Alpha: {Alpha}";
        }
    }
}
