using System;

namespace CCSWE.nanoFramework
{
    internal static partial class Math
    {
        /// <summary>
        /// Returns the smaller of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two double-precision floating-point numbers to compare.</param>
        /// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("** This is slower than System.Math **")]
        public static double Min(double val1, double val2)
        {
            // TODO: The last part of this spec is not currently possible to implement in managed code
            // This matches the IEEE 754:2019 `minimum` function
            //
            // It propagates NaN inputs back to the caller and
            // otherwise returns the lesser of the inputs. It
            // treats +0 as lesser than -0 as per the specification.

            if (val1 != val2)
            {
                if (!double.IsNaN(val1))
                {
                    return val1 < val2 ? val1 : val2;
                }

                return val1;
            }

            // TODO: Implement double.IsNegative
            //return double.IsNegative(val1) ? val1 : val2;

            return val1;
        }

        /// <summary>
        /// Returns the smaller of two single-precision floating-point numbers.
        /// </summary>
        /// <param name="val1">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="val2">The second of two single-precision floating-point numbers to compare.</param>
        /// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obsolete("** This is slower than System.Math **")]
        public static float Min(float val1, float val2)
        {
            // TODO: The last part of this spec is not currently possible to implement in managed code
            // This matches the IEEE 754:2019 `minimum` function
            //
            // It propagates NaN inputs back to the caller and
            // otherwise returns the lesser of the inputs. It
            // treats +0 as lesser than -0 as per the specification.

            if (val1 != val2)
            {
                if (!float.IsNaN(val1))
                {
                    return val1 < val2 ? val1 : val2;
                }

                return val1;
            }

            // TODO: Implement double.IsNegative
            //return float.IsNegative(val1) ? val1 : val2;

            return val1;
        }

        /// <summary>Returns the smaller of two 32-bit signed integers.</summary>
        /// <param name="val1">The first of two 32-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 32-bit signed integers to compare.</param>
        /// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
        // TODO: This is a minor improvement and could be copied over to System.Math to replace the native call
        [Obsolete("** This is slower than System.Math **")]
        public static int Min(int val1, int val2)
        {
            return (val1 <= val2) ? val1 : val2;
        }
    }
}
