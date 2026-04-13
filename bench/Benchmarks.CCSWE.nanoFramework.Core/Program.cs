namespace Benchmarks.CCSWE.nanoFramework.Core;

public class Program
{
    public static void Main()
    {
        // BenchmarkProgram.AddBenchmark(typeof(EnsureBenchmarks));
        BenchmarkProgram.AddBenchmark(typeof(StringEqualsBenchmarks));

        BenchmarkProgram.RunBenchmarks();
    }
}