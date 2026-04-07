#if DEBUG
using System.Diagnostics;
#endif
using System;
using System.Threading;
using nanoFramework.Benchmark;

namespace CCSWE.nanoFramework.Graphics.Benchmarks;

public class Program
{
    public static void Main()
    {
#if DEBUG
        Console.WriteLine("Benchmarks should be run in a release build.");
        Debugger.Break();
        return;
#endif
        Console.WriteLine("Running benchmarks...");

        //BenchmarkRunner.RunClass(typeof(ColorConverterBenchmarks));
        BenchmarkRunner.RunClass(typeof(ScaleBrightnessBenchmarks));

        Console.WriteLine("Benchmarks completed.");

        Thread.Sleep(Timeout.Infinite);
    }
}
