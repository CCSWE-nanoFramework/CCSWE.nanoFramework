using Iot.Device.Ws28xx.Esp32;
using nanoFramework.Benchmark.Attributes;
using nanoFramework.Benchmark;
using System.Drawing;

// ReSharper disable InconsistentNaming
namespace CCSWE.nanoFramework.NeoPixel.Benchmarks.Reference
{
    [IterationCount(IterationCount)]
    public class Ws28xxNeoPixelStripBenchmarks : NeoPixelStripBenchmarkBase
    {
        private Ws28xxNeoPixelStrip _sut;

        [Setup]
        public override void Setup()
        {
            _sut = new Ws28xxNeoPixelStrip(StripParameters.Pin, StripParameters.Count);
        }

        [Benchmark]
        public override void Fill()
        {
            _sut.Fill(TestData.Color);
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
    }

    internal class Ws28xxNeoPixelStrip
    {
        private readonly ushort _count;
        private readonly Ws2812c _ws28xx;

        public Ws28xxNeoPixelStrip(byte pin, ushort count)
        {
            _count = count;

            _ws28xx = new Ws2812c(pin, count);
        }

        public ushort Count => _count;

        public void Fill(Color color)
        {
            _ws28xx.Image.Clear(color);
        }

        public void SetLed(int index, Color color)
        {
            for (var i = 0; i < _count; i++)
            {
                _ws28xx.Image.SetPixel(i, 0, color);
            }
        }

        public void Update()
        {
            _ws28xx.Update();
        }
    }
}
