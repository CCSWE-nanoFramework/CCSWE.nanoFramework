using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using System;
using CCSWE.nanoFramework;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
internal class AbsBenchmarks_Double: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Abs_Double()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Abs(BenchmarkData.DoubleNegative1);
            var result2 = FastMath.Abs(BenchmarkData.DoublePositive1);
        });
    }

    [Benchmark]
    [Baseline]
    public void System_Abs_Double()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Abs(BenchmarkData.DoubleNegative1);
            var result2 = Math.Abs(BenchmarkData.DoublePositive1);
        });
    }
}