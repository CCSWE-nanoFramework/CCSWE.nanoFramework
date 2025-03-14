using System;
using System.Diagnostics;
using nanoFramework.Benchmark;

namespace CCSWE.nanoFramework.Benchmarks
{
    public class Program
    {
        public static void Main()
        {
#if DEBUG
            Console.WriteLine("Benchmarks should be run in a release build.");
            Debugger.Break();
#endif
            Console.WriteLine("Running benchmarks...");

            BenchmarkRunner.RunClass(typeof(AbsBenchmarks));
            BenchmarkRunner.RunClass(typeof(ClampBenchmarks));
            BenchmarkRunner.RunClass(typeof(MaxBenchmarks));
            BenchmarkRunner.RunClass(typeof(MinBenchmarks));

            BenchmarkRunner.RunClass(typeof(BitConverterBenchmarks));
        }
    }
}
