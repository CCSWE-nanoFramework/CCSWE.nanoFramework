using CCSWE.nanoFramework.Graphics;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework.Graphics;

[IterationCount(100)]
public class ColorConverterBenchmarks : BenchmarkBase
{
    private const float Brightness = 0.5f;
    private const int Iterations = 100;

    [Benchmark]
    public void ScaleBrightness()
    {
        RunIterations(Iterations, () =>
        {
            var scaledColor = ColorConverter.ScaleBrightness(BenchmarkData.Color, Brightness);
        });
    }

    [Benchmark]
    public void ToColor_From_HsbColor()
    {
        RunIterations(Iterations, () =>
        {
            var color = ColorConverter.ToColor(BenchmarkData.HsbColor);
        });
    }

    [Benchmark]
    public void ToColor_From_HslColor()
    {
        RunIterations(Iterations, () =>
        {
            var color = ColorConverter.ToColor(BenchmarkData.HslColor);
        });
    }

    [Benchmark]
    public void ToHsbColor()
    {
        RunIterations(Iterations, () =>
        {
            var color = ColorConverter.ToHsbColor(BenchmarkData.Color);
        });
    }

    [Benchmark]
    public void ToHslColor()
    {
        RunIterations(Iterations, () =>
        {
            var color = ColorConverter.ToHslColor(BenchmarkData.Color);
        });
    }
}
