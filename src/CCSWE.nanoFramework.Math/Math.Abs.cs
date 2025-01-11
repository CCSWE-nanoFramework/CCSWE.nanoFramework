using System;

namespace CCSWE.nanoFramework
{
    internal static partial class Math
    {
        /// <summary>
        /// Returns the absolute value of a double-precision floating-point number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to <see cref="F:System.Double.MinValue" />, but less than or equal to <see cref="F:System.Double.MaxValue" />.</param>
        /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="F:System.Double.MaxValue" />.</returns>
        /// <remarks>** This is slower than <see cref="System.Math"/> **</remarks>
        [Obsolete("** This is slower than System.Math **")]
        public static double Abs(double value)
        {
            const ulong mask = 0x7FFFFFFFFFFFFFFF;
            ulong raw = BitConverter.DoubleToUInt64Bits(value);

            return BitConverter.UInt64BitsToDouble(raw & mask);
        }

        /// <summary>
        /// Returns the absolute value of a single-precision floating-point number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to <see cref="F:System.Single.MinValue" />, but less than or equal to <see cref="F:System.Single.MaxValue" />.</param>
        /// <returns>A single-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="F:System.Single.MaxValue" />.</returns>
        /// <remarks>** This is slower than <see cref="System.Math"/> **</remarks>
        [Obsolete("** This is slower than System.Math **")] // This might be improved by implementing BitConverter.SingleToUInt32Bits on the native side
        public static float Abs(float value)
        {
            const uint mask = 0x7FFFFFFF;
            uint raw = BitConverter.SingleToUInt32Bits(value);

            return BitConverter.UInt32BitsToSingle(raw & mask);
        }

        /// <summary>
        /// Returns the absolute value of a 32-bit signed integer.
        /// </summary>
        /// <param name="value">A number that is greater than <see cref="F:System.Int32.MinValue" />, but less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
        /// <returns>A 32-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int32.MaxValue" />.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        // TODO: This is a minor improvement and could be copied over to System.Math to replace the native call
        public static int Abs(int value)
        {
            if (value < 0)
            {
                value = -value;
                if (value < 0)
                {
                   throw new Exception("Overflow");
                }
            }
            return value;
        }
    }
}
