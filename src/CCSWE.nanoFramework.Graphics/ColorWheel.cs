using System.Drawing;

namespace CCSWE.nanoFramework.Graphics;

/// <summary>
/// Provides a 255-step color wheel for smooth RGB transitions.
/// </summary>
public static class ColorWheel
{
    /// <summary>
    /// Returns a color from a 255-step wheel that cycles through red → green → blue → red.
    /// </summary>
    /// <param name="position">Position on the wheel (0–255).</param>
    public static Color GetColor(int position) =>
        position switch
        {
            < 85 => Color.FromArgb(position * 3, 255 - position * 3, 0),
            < 170 => Color.FromArgb(255 - (position - 85) * 3, 0, (position - 85) * 3),
            _ => Color.FromArgb(0, (position - 170) * 3, 255 - (position - 170) * 3)
        };
}
