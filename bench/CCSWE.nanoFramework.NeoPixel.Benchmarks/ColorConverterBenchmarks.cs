using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    [IterationCount(100)]
    public class ColorConverterBenchmarks: BenchmarkBase
    {
        private const float Brightness = 0.5f;
        private const int Iterations = 100;

        [Benchmark]
        public void ScaleBrightness()
        {
            RunIterations(Iterations, () =>
            {
                var scaledColor = ColorConverter.ScaleBrightness(TestData.Color, Brightness);
            });
        }

        [Benchmark]
        public void ToColor_FromHsbColor()
        {
            RunIterations(Iterations, () =>
            {
                var color = ColorConverter.ToColor(TestData.HsbColor);
            });
        }

        [Benchmark]
        public void ToColor_FromHslColor()
        {
            RunIterations(Iterations, () =>
            {
                var color = ColorConverter.ToColor(TestData.HslColor);
            });
        }

        [Benchmark]
        public void ToHsbColor()
        {
            RunIterations(Iterations, () =>
            {
                var color = ColorConverter.ToHsbColor(TestData.Color);
            });
        }

        [Benchmark]
        public void ToHslColor()
        {
            RunIterations(Iterations, () =>
            {
                var color = ColorConverter.ToHslColor(TestData.Color);
            });
        }
    }
}
