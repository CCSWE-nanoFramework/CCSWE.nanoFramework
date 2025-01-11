using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.Benchmarks
{
    // TODO: These do not test enough cases (ie: NaN variations) to be representative of actual use
    [IterationCount(Iterations)]
    public class ClampBenchmarks: BenchmarksBase
    {
/*        [Benchmark]
        public void Clamp_Double_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Clamp(DoublePositive1, DoubleNegative1, DoublePositive2);
                var result2 = FastMath.Clamp(double.NaN, DoubleNegative1, DoublePositive2);
                var result3 = FastMath.Clamp(double.NaN, double.NaN, DoublePositive2);
                var result4 = FastMath.Clamp(double.NaN, double.NaN, double.NaN);
                var result5 = FastMath.Clamp(DoublePositive1, double.NaN, double.NaN);
                var result6 = FastMath.Clamp(DoublePositive1, DoubleNegative1, double.NaN);
            });
        }
*/
        [Benchmark]
        public void Clamp_Double_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Clamp(DoublePositive1, DoubleNegative1, DoublePositive2);
                var result2 = Math.Clamp(double.NaN, DoubleNegative1, DoublePositive2);
                var result3 = Math.Clamp(double.NaN, double.NaN, DoublePositive2);
                var result4 = Math.Clamp(double.NaN, double.NaN, double.NaN);
                var result5 = Math.Clamp(DoublePositive1, double.NaN, double.NaN);
                var result6 = Math.Clamp(DoublePositive1, DoubleNegative1, double.NaN);
            });
        }

        [Benchmark]
        public void Clamp_System_Double_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Clamp_System(DoublePositive1, DoubleNegative1, DoublePositive2);
                var result2 = Math.Clamp_System(double.NaN, DoubleNegative1, DoublePositive2);
                var result3 = Math.Clamp_System(double.NaN, double.NaN, DoublePositive2);
                var result4 = Math.Clamp_System(double.NaN, double.NaN, double.NaN);
                var result5 = Math.Clamp_System(DoublePositive1, double.NaN, double.NaN);
                var result6 = Math.Clamp_System(DoublePositive1, DoubleNegative1, double.NaN);
            });
        }

        [Benchmark]
        public void Clamp_Double_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Clamp(DoublePositive1, DoubleNegative1, DoublePositive2);
                var result2 = System.Math.Clamp(double.NaN, DoubleNegative1, DoublePositive2);
                var result3 = System.Math.Clamp(double.NaN, double.NaN, DoublePositive2);
                var result4 = System.Math.Clamp(double.NaN, double.NaN, double.NaN);
                var result5 = System.Math.Clamp(DoublePositive1, double.NaN, double.NaN);
                var result6 = System.Math.Clamp(DoublePositive1, DoubleNegative1, double.NaN);
            });
        }

/*        [Benchmark]
        public void Clamp_Float_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Clamp(FloatPositive1, FloatNegative1, FloatPositive2);
                var result2 = FastMath.Clamp(float.NaN, FloatNegative1, FloatPositive2);
                var result3 = FastMath.Clamp(float.NaN, float.NaN, FloatPositive2);
                var result4 = FastMath.Clamp(float.NaN, float.NaN, float.NaN);
                var result5 = FastMath.Clamp(FloatPositive1, float.NaN, float.NaN);
                var result6 = FastMath.Clamp(FloatPositive1, FloatNegative1, float.NaN);
            });
        }
*/
        [Benchmark]
        public void Clamp_Float_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Clamp(FloatPositive1, FloatNegative1, FloatPositive2);
                var result2 = Math.Clamp(float.NaN, FloatNegative1, FloatPositive2);
                var result3 = Math.Clamp(float.NaN, float.NaN, FloatPositive2);
                var result4 = Math.Clamp(float.NaN, float.NaN, float.NaN);
                var result5 = Math.Clamp(FloatPositive1, float.NaN, float.NaN);
                var result6 = Math.Clamp(FloatPositive1, FloatNegative1, float.NaN);
            });
        }

        [Benchmark]
        public void Clamp_System_Float_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Clamp_System(FloatPositive1, FloatNegative1, FloatPositive2);
                var result2 = Math.Clamp_System(float.NaN, FloatNegative1, FloatPositive2);
                var result3 = Math.Clamp_System(float.NaN, float.NaN, FloatPositive2);
                var result4 = Math.Clamp_System(float.NaN, float.NaN, float.NaN);
                var result5 = Math.Clamp_System(FloatPositive1, float.NaN, float.NaN);
                var result6 = Math.Clamp_System(FloatPositive1, FloatNegative1, float.NaN);
            });
        }

        [Benchmark]
        public void Clamp_Float_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Clamp(FloatPositive1, FloatNegative1, FloatPositive2);
                var result2 = System.Math.Clamp(float.NaN, FloatNegative1, FloatPositive2);
                var result3 = System.Math.Clamp(float.NaN, float.NaN, FloatPositive2);
                var result4 = System.Math.Clamp(float.NaN, float.NaN, float.NaN);
                var result5 = System.Math.Clamp(FloatPositive1, float.NaN, float.NaN);
                var result6 = System.Math.Clamp(FloatPositive1, FloatNegative1, float.NaN);
            });
        }

        [Benchmark]
        public void Clamp_Long_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result = FastMath.Clamp(LongPositive1, LongNegative1, LongPositive2);
            });
        }

        [Benchmark]
        public void Clamp_Long_Math()
        {
            RunIterations(Loops, () =>
            {
                var result = Math.Clamp(LongPositive1, LongNegative1, LongPositive2);
            });
        }

        [Benchmark]
        public void Clamp_Long_System()
        {
            RunIterations(Loops, () =>
            {
                var result = System.Math.Clamp(LongPositive1, LongNegative1, LongPositive2);
            });
        }

        [Benchmark]
        public void Clamp_ULong_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result = FastMath.Clamp(ULongLow, ULongMid, ULongHigh);
            });
        }

        [Benchmark]
        public void Clamp_ULong_Math()
        {
            RunIterations(Loops, () =>
            {
                var result = Math.Clamp(ULongLow, ULongMid, ULongHigh);
            });
        }

        [Benchmark]
        public void Clamp_ULong_System()
        {
            RunIterations(Loops, () =>
            {
                var result = System.Math.Clamp(ULongLow, ULongMid, ULongHigh);
            });
        }
    }
}
