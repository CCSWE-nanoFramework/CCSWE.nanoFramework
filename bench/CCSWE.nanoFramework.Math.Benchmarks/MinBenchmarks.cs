using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.Benchmarks
{
    [IterationCount(Iterations)]
    public class MinBenchmarks: BenchmarksBase
    {
        [Benchmark]
        public void Min_Double_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Min(DoublePositive1, DoubleNegative1);
                var result2 = FastMath.Min(DoublePositive2, DoublePositive1);
                var result3 = FastMath.Min(double.NaN, DoublePositive2);
            });
        }

        [Benchmark]
        public void Min_Double_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Min(DoublePositive1, DoubleNegative1);
                var result2 = Math.Min(DoublePositive2, DoublePositive1);
                var result3 = Math.Min(double.NaN, DoublePositive2);
            });
        }

        [Benchmark]
        public void Min_Double_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Min(DoublePositive1, DoubleNegative1);
                var result2 = System.Math.Min(DoublePositive2, DoublePositive1);
                var result3 = System.Math.Min(double.NaN, DoublePositive2);
            });
        }

        [Benchmark]
        public void Min_Float_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Min(FloatPositive1, FloatNegative1);
                var result2 = FastMath.Min(FloatPositive2, FloatPositive1);
                var result3 = FastMath.Min(float.NaN, FloatPositive2);
            });
        }

        [Benchmark]
        public void Min_Float_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Min(FloatPositive1, FloatNegative1);
                var result2 = Math.Min(FloatPositive2, FloatPositive1);
                var result3 = Math.Min(float.NaN, FloatPositive2);
            });
        }

        // TODO: This is much slower than System.Math.Min(double,double) and I'm assuming it's because the implementation is a cast to the native double method
        [Benchmark]
        public void Min_Float_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Min(FloatPositive1, FloatNegative1);
                var result2 = System.Math.Min(FloatPositive2, FloatPositive1);
                var result3 = System.Math.Min(float.NaN, FloatPositive2);
            });
        }

        [Benchmark]
        public void Min_Int_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Min(IntPositive1, IntNegative1);
                var result2 = FastMath.Min(IntPositive2, IntPositive1);
                var result3 = FastMath.Min(IntNegative1, IntPositive2);
            });
        }

        [Benchmark]
        public void Min_Int_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Min(IntPositive1, IntNegative1);
                var result2 = Math.Min(IntPositive2, IntPositive1);
                var result3 = Math.Min(IntNegative1, IntPositive2);
            });
        }

        [Benchmark]
        public void Min_Int_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Min(IntPositive1, IntNegative1);
                var result2 = System.Math.Min(IntPositive2, IntPositive1);
                var result3 = System.Math.Min(IntNegative1, IntPositive2);
            });
        }
    }
}
