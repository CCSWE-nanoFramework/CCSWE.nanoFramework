using System;

namespace CCSWE.nanoFramework
{
    // TODO: Still deciding if I want/need to expose this (or even use it at all).

    /// <summary>
    /// Converts base data types to an array of bytes, and an array of bytes to base data types.
    /// </summary>
    internal class BitConverter
    {
        /// <summary>
        /// Indicates the byte order ("endianess") in which data is stored in this computer architecture.
        /// </summary>
        public static readonly bool IsLittleEndian = System.BitConverter.IsLittleEndian;

        /// <summary>
        /// Converts the specified double-precision floating point number to a 64-bit signed integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 64-bit signed integer whose bits are identical to <paramref name="value"/>.</returns>
        /// <remarks>** This is slower than <see cref="System.BitConverter"/> **</remarks>
        [Obsolete("** This is slower than System.BitConverter **")]
        public static unsafe long DoubleToInt64Bits(double value) => *((long*)&value);

        /// <summary>
        /// Converts the specified 64-bit signed integer to a double-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A double-precision floating point number whose bits are identical to <paramref name="value"/>.</returns>
        /// <remarks>** This is slower than <see cref="System.BitConverter"/> **</remarks>
        [Obsolete("** This is slower than System.BitConverter **")]
        public static unsafe double Int64BitsToDouble(long value) => *((double*)&value);

        /// <summary>
        /// Converts the specified double-precision floating point number to a 64-bit unsigned integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 64-bit unsigned integer whose bits are identical to <paramref name="value"/>.</returns>
        [CLSCompliant(false)]
        public static unsafe ulong DoubleToUInt64Bits(double value) => *((ulong*)&value);

        /// <summary>
        /// Converts the specified 64-bit unsigned integer to a double-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A double-precision floating point number whose bits are identical to <paramref name="value"/>.</returns>
        [CLSCompliant(false)]
        public static unsafe double UInt64BitsToDouble(ulong value) => *((double*)&value);

        /// <summary>
        /// Converts the specified single-precision floating point number to a 32-bit unsigned integer.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A 32-bit unsigned integer whose bits are identical to <paramref name="value"/>.</returns>
        [CLSCompliant(false)]
        public static unsafe uint SingleToUInt32Bits(float value) => *((uint*)&value);

        /// <summary>
        /// Converts the specified 32-bit unsigned integer to a single-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A single-precision floating point number whose bits are identical to <paramref name="value"/>.</returns>
        [CLSCompliant(false)]
        public static unsafe float UInt32BitsToSingle(uint value) => *((float*)&value);
    }
}
