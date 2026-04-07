using System.Drawing;

namespace CCSWE.nanoFramework.Graphics;

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

        return FastMath.Abs(Hue - color.Hue) < 0.001f &&
               FastMath.Abs(Saturation - color.Saturation) < 0.001f &&
               FastMath.Abs(Light - color.Light) < 0.001f &&
               Alpha == color.Alpha;
    }

    public static HslColor FromColor(Color color)
    {
        return ColorConverter.ToHslColor(color);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        // Use integer-truncated values so the hash is consistent with the 0.001f
        // equality tolerance used in Equals for typical integer-degree hue and
        // integer-percentage saturation/light values.
        unchecked
        {
            var hash = 17;
            hash = hash * 31 + ((int)Hue).GetHashCode();
            hash = hash * 31 + ((int)Saturation).GetHashCode();
            hash = hash * 31 + ((int)Light).GetHashCode();
            hash = hash * 31 + Alpha.GetHashCode();
            return hash;
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
