using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class AbsBenchmarks_Int: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Abs_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Abs(BenchmarkData.IntNegative1);
            var result2 = FastMath.Abs(BenchmarkData.IntPositive1);
        });
    }


    [Benchmark]
    [Baseline]
    public void System_Abs_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = System.Math.Abs(BenchmarkData.IntNegative1);
            var result2 = System.Math.Abs(BenchmarkData.IntPositive1);
        });
    }
}