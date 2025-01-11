using System;

namespace CCSWE.nanoFramework
{
    public static partial class FastMath
    {
        /// <inheritdoc cref="Math.Abs(double)"/>
        public static double Abs(double value) => value > 0.0d ? value : value * -1.0d;

        /// <inheritdoc cref="Math.Abs(float)"/>
        public static float Abs(float value) => value > 0.0f ? value : value * -1.0f;

        /// <inheritdoc cref="Math.Abs(int)"/>
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
