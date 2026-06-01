using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class ClampBenchmarks_Float: BenchmarkBase
{
    [Benchmark]
    public void FastMast_Clamp_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Clamp(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
            var result2 = FastMath.Clamp(float.NaN, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
        });
    }

    [Benchmark]
    public void System_Clamp_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = System.Math.Clamp(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
            var result2 = System.Math.Clamp(float.NaN, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
        });
    }
}