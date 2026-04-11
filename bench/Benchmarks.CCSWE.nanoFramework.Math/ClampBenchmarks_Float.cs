using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

[IterationCount(BenchmarkData.Iterations)]
// ReSharper disable once InconsistentNaming
public class ClampBenchmarks_Float: BenchmarkBase
{
    [Benchmark]
    public void FastMast_Clamp_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = FastMath.Clamp(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
            var result2 = FastMath.Clamp(double.NaN, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
            var result3 = FastMath.Clamp(double.NaN, double.NaN, BenchmarkData.FloatPositive2);
            var result4 = FastMath.Clamp(double.NaN, double.NaN, double.NaN);
            var result5 = FastMath.Clamp(BenchmarkData.FloatPositive1, double.NaN, double.NaN);
            var result6 = FastMath.Clamp(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1, double.NaN);
        });
    }

    [Benchmark]
    public void System_Clamp_Float()
    {
        RunIterations(BenchmarkData.Loops, () =>
        {
            var result1 = System.Math.Clamp(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
            var result2 = System.Math.Clamp(double.NaN, BenchmarkData.FloatNegative1, BenchmarkData.FloatPositive2);
            var result3 = System.Math.Clamp(double.NaN, double.NaN, BenchmarkData.FloatPositive2);
            var result4 = System.Math.Clamp(double.NaN, double.NaN, double.NaN);
            var result5 = System.Math.Clamp(BenchmarkData.FloatPositive1, double.NaN, double.NaN);
            var result6 = System.Math.Clamp(BenchmarkData.FloatPositive1, BenchmarkData.FloatNegative1, double.NaN);
        });
    }
}