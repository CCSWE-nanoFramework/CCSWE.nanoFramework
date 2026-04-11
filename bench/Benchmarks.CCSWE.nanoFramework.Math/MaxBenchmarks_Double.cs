using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class MaxBenchmarks_Double: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Max_Double()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Max(BenchmarkData.DoublePositive1, BenchmarkData.DoubleNegative1);
            var result2 = FastMath.Max(BenchmarkData.DoublePositive2, BenchmarkData.DoublePositive1);
            var result3 = FastMath.Max(double.NaN, BenchmarkData.DoublePositive2);
        });
    }

    [Benchmark]
    public void System_Max_Double()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Max(BenchmarkData.DoublePositive1, BenchmarkData.DoubleNegative1);
            var result2 = Math.Max(BenchmarkData.DoublePositive2, BenchmarkData.DoublePositive1);
            var result3 = Math.Max(double.NaN, BenchmarkData.DoublePositive2);
        });
    }
}