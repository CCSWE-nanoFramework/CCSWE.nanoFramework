using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class MinBenchmarks_Float: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Min_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Min(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1);
            var result2 = FastMath.Min(BenchmarkData.FloatPositive2, BenchmarkData.FloatPositive1);
            var result3 = FastMath.Min(float.NaN, BenchmarkData.FloatPositive2);
        });
    }

    [Benchmark]
    public void System_Min_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Min(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1);
            var result2 = Math.Min(BenchmarkData.FloatPositive2, BenchmarkData.FloatPositive1);
            var result3 = Math.Min(float.NaN, BenchmarkData.FloatPositive2);
        });
    }
}