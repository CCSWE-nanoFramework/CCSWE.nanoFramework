using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    [IterationCount(IterationCount)]
    public class NeoPixelStripBenchmarks : NeoPixelStripBenchmarkBase
    {
        private const float Brightness = 0.1f;

        private NeoPixelStrip _sut = null!;

        [Setup]
        public override void Setup()
        {
            _sut = new NeoPixelStrip(StripParameters.Pin, StripParameters.Count, StripParameters.Driver);
        }

        [Benchmark]
        public override void Fill()
        {
            _sut.Fill(BenchmarkData.Color);
            _sut.Update();
        }

        [Benchmark]
        public void Fill_Brightness()
        {
            _sut.Fill(BenchmarkData.Color, Brightness);
            _sut.Update();
        }

        [Benchmark]
        public void SetPixel()
        {
            for (var i = 0; i < _sut.Count; i++)
            {
                _sut.SetLed(i, BenchmarkData.Color);
            }

            _sut.Update();
        }

        [Benchmark]
        public void SetPixel_Brightness()
        {
            for (var i = 0; i < _sut.Count; i++)
            {
                _sut.SetLed(i, BenchmarkData.Color, Brightness);
            }

            _sut.Update();
        }

        [Benchmark]
        public void SetPixel_Brightness_Scaled()
        {
            var color = ColorConverter.ScaleBrightness(BenchmarkData.Color, Brightness);

            for (var i = 0; i < _sut.Count; i++)
            {
                _sut.SetLed(i, color);
            }

            _sut.Update();
        }
    }
}

