using System;
using System.Diagnostics;
using System.Threading;
using nanoFramework.Benchmark;

namespace CCSWE.nanoFramework.Threading.Benchmarks
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

            BenchmarkRunner.RunClass(typeof(ThreadPoolBenchmarks));

            Console.WriteLine("Completed benchmarks...");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
