using System;

namespace CCSWE.nanoFramework
{
    public static partial class FastMath
    {
        /// <inheritdoc cref="Math.Clamp(double,double,double)"/>
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

        /// <inheritdoc cref="Math.Clamp(float,float,float)"/>
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

        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
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

        /// <inheritdoc cref="Math.Clamp(long,long,long)"/>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static long Clamp(long value, long min, long max) => System.Math.Clamp(value, min, max);

        /// <inheritdoc cref="Math.Clamp(ulong,ulong,ulong)"/>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ulong Clamp(ulong value, ulong min, ulong max) => System.Math.Clamp(value, min, max);
    }
}
