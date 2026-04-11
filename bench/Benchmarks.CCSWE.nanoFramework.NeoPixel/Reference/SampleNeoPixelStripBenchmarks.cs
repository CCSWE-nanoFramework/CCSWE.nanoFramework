using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework.NeoPixel.Reference;

[IterationCount(BenchmarkData.IterationCount)]
public class SampleNeoPixelStripBenchmarks : BenchmarkBase
{
    private SampleNeoPixelStrip _sut = null!;

    [Setup]
    public void Setup()
    {
        _sut = new SampleNeoPixelStrip(StripParameters.Pin, StripParameters.Count);
    }

    [Benchmark]
    public void Fill()
    {
        _sut.Fill(BenchmarkData.Color);
        _sut.Update();
    }
}