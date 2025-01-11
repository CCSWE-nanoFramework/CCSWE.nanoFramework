using System;

namespace CCSWE.nanoFramework
{
    public static partial class FastMath
    {
        /// <summary>
        /// Returns the smaller of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
        /// <remarks>This does not match the IEEE 754:2019 `minimum` function with respect to <see cref="double.NaN"/> and <c>+0</c> / <c>-0</c></remarks>
        [Obsolete("** This is slower than System.Math **")]
        public static double Min(double val1, double val2)
        {
            return (val1 <= val2) ? val1 : val2;
        }

        /// <summary>
        /// Returns the smaller of two single-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
        /// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
        /// <remarks>This does not match the IEEE 754:2019 `minimum` function with respect to <see cref="float.NaN"/> and <c>+0</c> / <c>-0</c></remarks>
        [Obsolete("** This is slower than System.Math **")]
        public static float Min(float val1, float val2)
        {
            return (val1 <= val2) ? val1 : val2;
        }

        /// <inheritdoc cref="Math.Min(int,int)"/>
        [Obsolete("** This is slower than System.Math **")]
        public static int Min(int val1, int val2)
        {
            return (val1 <= val2) ? val1 : val2;
        }
    }
}
