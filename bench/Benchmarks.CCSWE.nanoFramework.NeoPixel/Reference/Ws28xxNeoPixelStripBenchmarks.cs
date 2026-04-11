using nanoFramework.Benchmark.Attributes;
using nanoFramework.Benchmark;

// ReSharper disable InconsistentNaming
namespace Benchmarks.CCSWE.nanoFramework.NeoPixel.Reference;

[IterationCount(BenchmarkData.IterationCount)]
public class Ws28xxNeoPixelStripBenchmarks : BenchmarkBase
{
    private Ws28xxNeoPixelStrip _sut = null!;

    [Setup]
    public void Setup()
    {
        _sut = new Ws28xxNeoPixelStrip(StripParameters.Pin, StripParameters.Count);
    }

    [Benchmark]
    public void Fill()
    {
        _sut.Fill(BenchmarkData.Color);
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
}