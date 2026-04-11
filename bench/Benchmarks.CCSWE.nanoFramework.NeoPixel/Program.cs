using Benchmarks.CCSWE.nanoFramework.NeoPixel.Reference;

namespace Benchmarks.CCSWE.nanoFramework.NeoPixel;

public class Program
{
    public static void Main()
    {
        StripParameters.Initialize(DeviceType.LilyGoT4);
        
        BenchmarkProgram.AddBenchmark(typeof(ReferenceBenchmarks));
            
        /*
        BenchmarkProgram.AddBenchmark(typeof(NeoPixelStripBenchmarks));
        BenchmarkProgram.AddBenchmark(typeof(SampleNeoPixelStripBenchmarks));
        BenchmarkProgram.AddBenchmark(typeof(Ws28xxNeoPixelStripBenchmarks));
        */

        BenchmarkProgram.RunBenchmarks();
    }
}