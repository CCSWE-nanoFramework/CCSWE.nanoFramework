using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.Benchmarks
{
    [IterationCount(Iterations)]
    public class MaxBenchmarks: BenchmarksBase
    {
        [Benchmark]
        public void Max_Double_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Max(DoublePositive1, DoubleNegative1);
                var result2 = FastMath.Max(DoublePositive2, DoublePositive1);
                var result3 = FastMath.Max(double.NaN, DoublePositive2);
            });
        }

        [Benchmark]
        public void Max_Double_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Max(DoublePositive1, DoubleNegative1);
                var result2 = Math.Max(DoublePositive2, DoublePositive1);
                var result3 = Math.Max(double.NaN, DoublePositive2);
            });
        }

        [Benchmark]
        public void Max_Double_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Max(DoublePositive1, DoubleNegative1);
                var result2 = System.Math.Max(DoublePositive2, DoublePositive1);
                var result3 = System.Math.Max(double.NaN, DoublePositive2);
            });
        }

        [Benchmark]
        public void Max_Float_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Max(FloatPositive1, FloatNegative1);
                var result2 = FastMath.Max(FloatPositive2, FloatPositive1);
                var result3 = FastMath.Max(float.NaN, FloatPositive2);
            });
        }

        [Benchmark]
        public void Max_Float_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Max(FloatPositive1, FloatNegative1);
                var result2 = Math.Max(FloatPositive2, FloatPositive1);
                var result3 = Math.Max(float.NaN, FloatPositive2);
            });
        }

        // TODO: This is much slower than System.Math.Max(double,double) and I'm assuming it's because the implementation is a cast to the native double method
        [Benchmark]
        public void Max_Float_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Max(FloatPositive1, FloatNegative1);
                var result2 = System.Math.Max(FloatPositive2, FloatPositive1);
                var result3 = System.Math.Max(float.NaN, FloatPositive2);
            });
        }

        [Benchmark]
        public void Max_Int_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Max(IntPositive1, IntNegative1);
                var result2 = FastMath.Max(IntPositive2, IntPositive1);
                var result3 = FastMath.Max(IntNegative1, IntPositive2);
            });
        }

        [Benchmark]
        public void Max_Int_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Max(IntPositive1, IntNegative1);
                var result2 = Math.Max(IntPositive2, IntPositive1);
                var result3 = Math.Max(IntNegative1, IntPositive2);
            });
        }

        [Benchmark]
        public void Max_Int_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Max(IntPositive1, IntNegative1);
                var result2 = System.Math.Max(IntPositive2, IntPositive1);
                var result3 = System.Math.Max(IntNegative1, IntPositive2);
            });
        }
    }
}
