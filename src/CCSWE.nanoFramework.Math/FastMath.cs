namespace CCSWE.nanoFramework
{
    /// <summary>
    /// Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.
    /// </summary>
    /// <remarks>
    /// In some cases exact IEEE 754:2019 compliance has been sacrificed in order to achieve higher performance.
    /// </remarks>
    public static partial class FastMath
    {
        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        /// <remarks>The value of this field is 3.14159265358979323846.</remarks>
        public const double PI = 3.14159265358979323846;

        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant, e.
        /// </summary>
        /// <remarks>The value of this field is 2.7182818284590452354.</remarks>
        public const double E = 2.7182818284590452354;
    }
}
