using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class ClampBenchmarks_ULong: BenchmarkBase
{
    [Benchmark]
    public void FastMast_Clamp_ULong()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Clamp(BenchmarkData.ULongLow, BenchmarkData.ULongMid, BenchmarkData.ULongHigh);
            var result2 = FastMath.Clamp(BenchmarkData.ULongMid, BenchmarkData.ULongLow, BenchmarkData.ULongHigh);
        });
    }

    [Benchmark]
    public void System_Clamp_ULong()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Clamp(BenchmarkData.ULongLow, BenchmarkData.ULongMid, BenchmarkData.ULongHigh);
            var result2 = Math.Clamp(BenchmarkData.ULongMid, BenchmarkData.ULongLow, BenchmarkData.ULongHigh);
        });
    }
}