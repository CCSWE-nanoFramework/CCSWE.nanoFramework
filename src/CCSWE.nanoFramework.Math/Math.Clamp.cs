using System;

namespace CCSWE.nanoFramework
{
    internal static partial class Math
    {
        /// <summary>Returns <paramref name="value" /> clamped to the inclusive range of <paramref name="min" /> and <paramref name="max" />.</summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        ///   <paramref name="value" /> if <paramref name="min" /> ≤ <paramref name="value" /> ≤ <paramref name="max" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="min" /> if <paramref name="value" /> &lt; <paramref name="min" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="max" /> if <paramref name="max" /> &lt; <paramref name="value" />.
        /// </returns>
        /// <remarks>From https://github.com/dotnet/runtime/blob/aea45f7aaa8f73cb2a585d8b14ce66634d58cb68/src/libraries/System.Private.CoreLib/src/System/Math.cs#L499</remarks>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(double value, double min, double max)
        {
            if (max < min)
            {
                throw new ArgumentException();
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>Returns <paramref name="value" /> clamped to the inclusive range of <paramref name="min" /> and <paramref name="max" />.</summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        ///   <paramref name="value" /> if <paramref name="min" /> ≤ <paramref name="value" /> ≤ <paramref name="max" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="min" /> if <paramref name="value" /> &lt; <paramref name="min" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="max" /> if <paramref name="max" /> &lt; <paramref name="value" />.
        /// </returns>
        /// <remarks>From https://github.com/dotnet/runtime/blob/aea45f7aaa8f73cb2a585d8b14ce66634d58cb68/src/libraries/System.Private.CoreLib/src/System/Math.cs#L635</remarks>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
        {
            if (max < min)
            {
                throw new ArgumentException();
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>Returns <paramref name="value" /> clamped to the inclusive range of <paramref name="min" /> and <paramref name="max" />.</summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        ///   <paramref name="value" /> if <paramref name="min" /> ≤ <paramref name="value" /> ≤ <paramref name="max" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="min" /> if <paramref name="value" /> &lt; <paramref name="min" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="max" /> if <paramref name="max" /> &lt; <paramref name="value" />.
        /// </returns>
        /// <remarks>This just calls through to <see cref="System.Math"/> so it's faster to call it directly.</remarks>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Clamp(long value, long min, long max) => System.Math.Clamp(value, min, max);

        /// <summary>Returns <paramref name="value" /> clamped to the inclusive range of <paramref name="min" /> and <paramref name="max" />.</summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The lower bound of the result.</param>
        /// <param name="max">The upper bound of the result.</param>
        /// <returns>
        ///   <paramref name="value" /> if <paramref name="min" /> ≤ <paramref name="value" /> ≤ <paramref name="max" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="min" /> if <paramref name="value" /> &lt; <paramref name="min" />.
        ///
        ///   -or-
        ///
        ///   <paramref name="max" /> if <paramref name="max" /> &lt; <paramref name="value" />.
        /// </returns>
        /// <remarks>This just calls through to <see cref="System.Math"/> so it's faster to call it directly.</remarks>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Clamp(ulong value, ulong min, ulong max) => System.Math.Clamp(value, min, max);

        // TODO: These are faster that System.Math but slower than the .NET Core implementations above

        /// <inheritdoc cref="Math.Clamp(double,double,double)"/>
        internal static double Clamp_System(double value, double min, double max)
        {
            // An optimized implementation of the System.Math code. Looking at latest .NET I'm not sure the IsNan checks are needed.
            if (min > max)
            {
                throw new ArgumentException();
            }

            if (double.IsNaN(min) && double.IsNaN(max))
            {
                return value;
            }

            if (double.IsNaN(min))
            {
                return value > max ? max : value;
            }

            if (double.IsNaN(max))
            {
                return value < min ? min : value;
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <inheritdoc cref="Math.Clamp(float,float,float)"/>
        internal static double Clamp_System(float value, float min, float max)
        {
            // An optimized implementation of the System.Math code. Looking at latest .NET I'm not sure the IsNan checks are needed.
            if (min > max)
            {
                throw new ArgumentException();
            }

            if (float.IsNaN(min) && float.IsNaN(max))
            {
                return value;
            }

            if (float.IsNaN(min))
            {
                return value > max ? max : value;
            }

            if (float.IsNaN(max))
            {
                return value < min ? min : value;
            }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}
