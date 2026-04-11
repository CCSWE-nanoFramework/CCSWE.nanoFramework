using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class MaxBenchmarks_Int: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Max_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Max(BenchmarkData.IntPositive1, BenchmarkData.IntNegative1);
            var result2 = FastMath.Max(BenchmarkData.IntPositive2, BenchmarkData.IntPositive1);
            var result3 = FastMath.Max(BenchmarkData.IntNegative1, BenchmarkData.IntPositive2);
        });
    }


    [Benchmark]
    public void System_Max_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Max(BenchmarkData.IntPositive1, BenchmarkData.IntNegative1);
            var result2 = Math.Max(BenchmarkData.IntPositive2, BenchmarkData.IntPositive1);
            var result3 = Math.Max(BenchmarkData.IntNegative1, BenchmarkData.IntPositive2);
        });
    }
}