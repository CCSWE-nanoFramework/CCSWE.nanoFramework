namespace Benchmarks.CCSWE.nanoFramework.WebServer;

public class Program
{
    public static void Main()
    {
        BenchmarkProgram.AddBenchmark(typeof(HttpMethodsBenchmarks));
        BenchmarkProgram.AddBenchmark(typeof(HttpResponseExtensionsBenchmarks));
        
        BenchmarkProgram.RunBenchmarks();
    }
}