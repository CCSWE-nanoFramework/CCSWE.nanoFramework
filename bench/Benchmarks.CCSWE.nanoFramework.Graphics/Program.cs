namespace Benchmarks.CCSWE.nanoFramework.Graphics;

public class Program
{
    public static void Main()
    {
        BenchmarkProgram.AddBenchmark(typeof(ColorConverterBenchmarks));
        BenchmarkProgram.AddBenchmark(typeof(ScaleBrightnessBenchmarks));

        BenchmarkProgram.RunBenchmarks();
    }
}
