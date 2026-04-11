using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class ClampBenchmarks_Long: BenchmarkBase
{
    [Benchmark]
    public void FastMast_Clamp_Long()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Clamp(BenchmarkData.LongNegative1, BenchmarkData.LongPositive1, BenchmarkData.LongPositive2);
            var result2 = FastMath.Clamp(BenchmarkData.LongPositive1, BenchmarkData.LongNegative1, BenchmarkData.LongPositive2);
        });
    }

    [Benchmark]
    public void System_Clamp_Long()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Clamp(BenchmarkData.LongNegative1, BenchmarkData.LongPositive1, BenchmarkData.LongPositive2);
            var result2 = Math.Clamp(BenchmarkData.LongPositive1, BenchmarkData.LongNegative1, BenchmarkData.LongPositive2);
        });
    }
}