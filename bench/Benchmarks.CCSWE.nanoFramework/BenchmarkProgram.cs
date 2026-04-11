using System;
using System.Collections;
#if DEBUG
using System.Diagnostics;
#endif
using System.Threading;
using nanoFramework.Benchmark;

namespace Benchmarks.CCSWE.nanoFramework;

public static class BenchmarkProgram
{
    private static readonly ArrayList Benchmarks = new();

    public static void AddBenchmark(Type benchmark)
    {
        Benchmarks.Add(benchmark);
    }
    
    public static void RunBenchmarks()
    {
#if DEBUG
        Console.WriteLine("Benchmarks should be run in a release build.");
        Debugger.Break();
        return;
#endif

        if (Benchmarks.Count <= 0)
        {
            Console.WriteLine("No benchmarks found to run.");
            return;
        }

        foreach (var benchmark in Benchmarks)
        {
            if (benchmark is not Type benchmarkType)
            {
                continue;
            }
        
            Console.WriteLine($"Running {benchmarkType.Name}...");
            BenchmarkRunner.RunClass(benchmarkType);
        }

        Console.WriteLine("Completed benchmarks...");

        Thread.Sleep(Timeout.Infinite);
    }
}