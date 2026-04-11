namespace Benchmarks.CCSWE.nanoFramework.Threading;

public class Program
{
    public static void Main()
    {
        BenchmarkProgram.AddBenchmark(typeof(ThreadPoolBenchmarks));

        BenchmarkProgram.RunBenchmarks();
    }
}