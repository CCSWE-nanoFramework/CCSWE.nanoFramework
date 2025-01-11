using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
#pragma warning disable CS0618 // Type or member is obsolete

namespace CCSWE.nanoFramework.Benchmarks
{
    [IterationCount(Iterations)]
    public class AbsBenchmarks: BenchmarksBase
    {
        [Benchmark]
        public void Abs_Double_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Abs(DoubleNegative1);
                var result2 = FastMath.Abs(DoublePositive1);
            });
        }

        [Benchmark]
        public void Abs_Double_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Abs(DoubleNegative1);
                var result2 = Math.Abs(DoublePositive1);
            });
        }

        [Benchmark]
        public void Abs_Double_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Abs(DoubleNegative1);
                var result2 = System.Math.Abs(DoublePositive1);
            });
        }

        [Benchmark]
        public void Abs_Float_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Abs(FloatNegative1);
                var result2 = FastMath.Abs(FloatPositive1);
            });
        }

        [Benchmark]
        public void Abs_Float_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Abs(FloatNegative1);
                var result2 = Math.Abs(FloatPositive1);
            });
        }

        [Benchmark]
        public void Abs_Float_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Abs(FloatNegative1);
                var result2 = System.Math.Abs(FloatPositive1);
            });
        }

        [Benchmark]
        public void Abs_Int_Fast()
        {
            RunIterations(Loops, () =>
            {
                var result1 = FastMath.Abs(IntNegative1);
                var result2 = FastMath.Abs(IntPositive1);
            });
        }

        [Benchmark]
        public void Abs_Int_Math()
        {
            RunIterations(Loops, () =>
            {
                var result1 = Math.Abs(IntNegative1);
                var result2 = Math.Abs(IntPositive1);
            });
        }

        [Benchmark]
        public void Abs_Int_System()
        {
            RunIterations(Loops, () =>
            {
                var result1 = System.Math.Abs(IntNegative1);
                var result2 = System.Math.Abs(IntPositive1);
            });
        }
    }
}
