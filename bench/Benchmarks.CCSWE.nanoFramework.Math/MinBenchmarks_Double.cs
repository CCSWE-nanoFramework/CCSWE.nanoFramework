using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class MinBenchmarks_Double: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Min_Double()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Min(BenchmarkData.DoublePositive1, BenchmarkData.DoubleNegative1);
            var result2 = FastMath.Min(BenchmarkData.DoublePositive2, BenchmarkData.DoublePositive1);
            var result3 = FastMath.Min(double.NaN, BenchmarkData.DoublePositive2);
        });
    }

    [Benchmark]
    public void System_Min_Double()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Min(BenchmarkData.DoublePositive1, BenchmarkData.DoubleNegative1);
            var result2 = Math.Min(BenchmarkData.DoublePositive2, BenchmarkData.DoublePositive1);
            var result3 = Math.Min(double.NaN, BenchmarkData.DoublePositive2);
        });
    }
}