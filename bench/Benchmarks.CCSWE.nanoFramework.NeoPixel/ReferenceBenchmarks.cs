using Benchmarks.CCSWE.nanoFramework.NeoPixel.Reference;
using CCSWE.nanoFramework.NeoPixel;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework.NeoPixel;

[IterationCount(BenchmarkData.IterationCount)]
// ReSharper disable InconsistentNaming
public class ReferenceBenchmarks: BenchmarkBase
{
    private NeoPixelStrip _neoPixelStrip = null!;
    private SampleNeoPixelStrip _sampleNeoPixelStrip = null!;
    private Ws28xxNeoPixelStrip _ws28xxNeoPixelStrip = null!;

    [Setup]
    public void Setup()
    {
        _neoPixelStrip = new NeoPixelStrip(StripParameters.Pin, StripParameters.Count, StripParameters.Driver);
        _sampleNeoPixelStrip = new SampleNeoPixelStrip(StripParameters.Pin, StripParameters.Count);
        _ws28xxNeoPixelStrip = new Ws28xxNeoPixelStrip(StripParameters.Pin, StripParameters.Count);
    }

    [Benchmark]
    public void Fill()
    {
        _neoPixelStrip.Fill(BenchmarkData.Color);
        _neoPixelStrip.Update();
    }

    [Benchmark]
    [Baseline]
    public void Fill_Sample()
    {
        _sampleNeoPixelStrip.Fill(BenchmarkData.Color);
        _sampleNeoPixelStrip.Update();
    }

    [Benchmark]
    public void Fill_Ws28xx()
    {
        _ws28xxNeoPixelStrip.Fill(BenchmarkData.Color);
        _ws28xxNeoPixelStrip.Update();
    }
}