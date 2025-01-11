using CCSWE.nanoFramework.NeoPixel.Drivers;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    [IterationCount(IterationCount)]
    public class NeoPixelStripBenchmarks : NeoPixelStripBenchmarkBase
    {
        private const float Brightness = 0.1f;

        private NeoPixelStrip _sut;

        [Setup]
        public override void Setup()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            _sut = new NeoPixelStrip(StripParameters.Pin, StripParameters.Count, new Ws2812B(StripParameters.ColorOrder));
        }

        [Benchmark]
        public override void Fill()
        {
            _sut.Fill(TestData.Color);
            _sut.Update();
        }

        [Benchmark]
        public void Fill_Brightness()
        {
            _sut.Fill(TestData.Color, Brightness);
            _sut.Update();
        }

        [Benchmark]
        public void SetPixel()
        {
            for (var i = 0; i < _sut.Count; i++)
            {
                _sut.SetLed(i, TestData.Color);
            }

            _sut.Update();
        }

        [Benchmark]
        public void SetPixel_Brightness()
        {
            for (var i = 0; i < _sut.Count; i++)
            {
                _sut.SetLed(i, TestData.Color, Brightness);
            }

            _sut.Update();
        }

        [Benchmark]
        public void SetPixel_Brightness_Scaled()
        {
            var color = ColorConverter.ScaleBrightness(TestData.Color, Brightness);

            for (var i = 0; i < _sut.Count; i++)
            {
                _sut.SetLed(i, color);
            }

            _sut.Update();
        }
    }
}

