using System;

namespace CCSWE.nanoFramework.Benchmarks
{
    public abstract class BenchmarksBase
    {
        protected const int Iterations = 250;
        protected const int Loops = 100;

        protected const double DoublePositive1 = Math.PI;
        protected const double DoublePositive2 = DoublePositive1 * 2;
        protected const double DoubleNegative1 = DoublePositive1 * -1;

        protected const float FloatPositive1 = (float) Math.PI;
        protected const float FloatPositive2 = FloatPositive1 * 2;
        protected const float FloatNegative1 = FloatPositive1 * -1;

        protected const int IntPositive1 = 123456789;
        protected const int IntPositive2 = IntPositive1 * 2;
        protected const int IntNegative1 = IntPositive1 * -1;

        protected const long LongPositive1 = 12345678901234567;
        protected const long LongPositive2 = LongPositive1 * 2;
        protected const long LongNegative1 = LongPositive1 * -1;

        protected const ulong ULongLow = 123456789012345678;
        protected const ulong ULongMid = 987654321098765432;
        protected const ulong ULongHigh = ULongMid * 2;

        public void RunIterations(int iterations, Action action)
        {
            for (var i = 0; i < iterations; i++)
            {
                action();
            }
        }
    }
}
