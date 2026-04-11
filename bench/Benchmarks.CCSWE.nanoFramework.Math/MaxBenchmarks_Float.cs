using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class MaxBenchmarks_Float: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Max_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Max(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1);
            var result2 = FastMath.Max(BenchmarkData.FloatPositive2, BenchmarkData.FloatPositive1);
            var result3 = FastMath.Max(float.NaN, BenchmarkData.FloatPositive2);
        });
    }

    [Benchmark]
    public void System_Max_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Max(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1);
            var result2 = Math.Max(BenchmarkData.FloatPositive2, BenchmarkData.FloatPositive1);
            var result3 = Math.Max(float.NaN, BenchmarkData.FloatPositive2);
        });
    }
}