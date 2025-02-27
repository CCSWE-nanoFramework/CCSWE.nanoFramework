using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks.Reference
{
    [IterationCount(IterationCount)]
    public class SampleNeoPixelStripBenchmarks : NeoPixelStripBenchmarkBase
    {
        private SampleNeoPixelStrip _sut = null!;

        [Setup]
        public override void Setup()
        {
            _sut = new SampleNeoPixelStrip(StripParameters.Pin, StripParameters.Count);
        }

        [Benchmark]
        public override void Fill()
        {
            _sut.Fill(BenchmarkData.Color);
            _sut.Update();
        }
    }
}
