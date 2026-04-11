using System;
using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class MinBenchmarks_Int: BenchmarkBase
{
    [Benchmark]
    public void FastMath_Min_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Min(BenchmarkData.IntPositive1, BenchmarkData.IntNegative1);
            var result2 = FastMath.Min(BenchmarkData.IntPositive2, BenchmarkData.IntPositive1);
            var result3 = FastMath.Min(BenchmarkData.IntNegative1, BenchmarkData.IntPositive2);
        });
    }


    [Benchmark]
    public void System_Min_Int()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = Math.Min(BenchmarkData.IntPositive1, BenchmarkData.IntNegative1);
            var result2 = Math.Min(BenchmarkData.IntPositive2, BenchmarkData.IntPositive1);
            var result3 = Math.Min(BenchmarkData.IntNegative1, BenchmarkData.IntPositive2);
        });
    }
}