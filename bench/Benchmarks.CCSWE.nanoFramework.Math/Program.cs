namespace Benchmarks.CCSWE.nanoFramework;

public class Program
{
    public static void Main()
    {
        BenchmarkProgram.AddBenchmark(typeof(AbsBenchmarks_Double));
        BenchmarkProgram.AddBenchmark(typeof(AbsBenchmarks_Float));
        BenchmarkProgram.AddBenchmark(typeof(AbsBenchmarks_Int));
        
        BenchmarkProgram.AddBenchmark(typeof(ClampBenchmarks_Double));
        BenchmarkProgram.AddBenchmark(typeof(ClampBenchmarks_Float));
        BenchmarkProgram.AddBenchmark(typeof(ClampBenchmarks_Int));
        BenchmarkProgram.AddBenchmark(typeof(ClampBenchmarks_Long));
        BenchmarkProgram.AddBenchmark(typeof(ClampBenchmarks_ULong));

        BenchmarkProgram.AddBenchmark(typeof(MaxBenchmarks_Double));
        BenchmarkProgram.AddBenchmark(typeof(MaxBenchmarks_Float));
        BenchmarkProgram.AddBenchmark(typeof(MaxBenchmarks_Int));

        BenchmarkProgram.AddBenchmark(typeof(MinBenchmarks_Double));
        BenchmarkProgram.AddBenchmark(typeof(MinBenchmarks_Float));
        BenchmarkProgram.AddBenchmark(typeof(MinBenchmarks_Int));

        BenchmarkProgram.RunBenchmarks();
    }
}