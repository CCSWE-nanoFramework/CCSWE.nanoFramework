using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class ClampBenchmarks_Int: BenchmarkBase
{
    [Benchmark]
    public void FastMast_Clamp_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Clamp(BenchmarkData.IntNegative1, BenchmarkData.IntPositive1, BenchmarkData.IntPositive2);
            var result2 = FastMath.Clamp(BenchmarkData.IntPositive1, BenchmarkData.IntNegative1, BenchmarkData.IntPositive2);
        });
    }

    [Benchmark]
    public void System_Clamp_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Clamp(BenchmarkData.IntNegative1, BenchmarkData.IntPositive1, BenchmarkData.IntPositive2);
            var result2 = Math.Clamp(BenchmarkData.IntPositive1, BenchmarkData.IntNegative1, BenchmarkData.IntPositive2);
        });
    }
}