namespace CCSWE.nanoFramework
{
    /// <summary>
    /// Fast comparison functions that exist outside the contract provided by <see cref="System.Math"/>.
    /// </summary>
    public static class FloatingPoint
    {
        /// <summary>Compares two values to determine equality.</summary>
        /// <param name="left">The value to compare with <paramref name="right" />.</param>
        /// <param name="right">The value to compare with <paramref name="left" />.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <c>false</c>.</returns>
        public static bool Equals(double left, double right)
        {
            return FastMath.Abs(left - right) < double.Epsilon;
        }

        /// <summary>Compares two values to determine equality.</summary>
        /// <param name="left">The value to compare with <paramref name="right" />.</param>
        /// <param name="right">The value to compare with <paramref name="left" />.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <c>false</c>.</returns>
        public static bool Equals(float left, float right)
        {
            return FastMath.Abs(left - right) < float.Epsilon;
        }

    }
}
