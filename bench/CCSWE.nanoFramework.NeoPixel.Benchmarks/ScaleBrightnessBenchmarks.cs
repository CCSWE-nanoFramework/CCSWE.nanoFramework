using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using CCSWE.nanoFramework.NeoPixel.Benchmarks.Reference;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    /// <summary>
    /// Compares the original <see cref="ColorConverter.ScaleBrightness"/> implementation
    /// (RGB -> HSB -> RGB round-trip) against the replacement channel-multiply approach.
    /// </summary>
    [IterationCount(100)]
    public class ScaleBrightnessBenchmarks : BenchmarkBase
    {
        private const float Brightness = 0.5f;
        private const int Iterations = 100;

        /// <summary>
        /// New implementation: multiply each channel by the brightness scale factor.
        /// Avoids floating-point color space conversions entirely.
        /// </summary>
        [Benchmark]
        public void ScaleBrightness_ChannelMultiply()
        {
            RunIterations(Iterations, () =>
            {
                var _ = ColorConverter.ScaleBrightness(BenchmarkData.Color, Brightness);
            });
        }

        /// <summary>
        /// Old implementation: convert to HSB, override brightness, convert back to RGB.
        /// Preserves hue/saturation fidelity at the cost of two full color space conversions.
        /// </summary>
        [Benchmark]
        public void ScaleBrightness_HsbRoundTrip()
        {
            RunIterations(Iterations, () =>
            {
                var _ = ScaleBrightnessReference.ScaleBrightness(BenchmarkData.Color, Brightness);
            });
        }
    }
}
