using System.Threading;
using CCSWE.nanoFramework.Threading;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework.Threading;

[IterationCount(64)]
public class ThreadPoolBenchmarks: BenchmarkBase
{
    private static readonly ManualResetEvent CompletedEvent = new(false);
    
    [Setup]
    public void Setup()
    {
        // Spin the threads up
        ThreadPool.SetMinThreads(ThreadPool.Workers);

        while (ThreadPool.ThreadCount < ThreadPool.Workers)
        {
            Thread.Sleep(10);
        }
    }

    [Benchmark]
    public void QueueUserWorkItem()
    {
        CompletedEvent.Reset();
        ThreadPool.QueueUserWorkItem(_ => { CompletedEvent.Set(); });
        CompletedEvent.WaitOne();
    }

    [Baseline]
    [Benchmark]
    public void SimpleThread()
    {
        CompletedEvent.Reset();
        var thread = new Thread(() => { CompletedEvent.Set(); });
        thread.Start();
        CompletedEvent.WaitOne();
    }
}