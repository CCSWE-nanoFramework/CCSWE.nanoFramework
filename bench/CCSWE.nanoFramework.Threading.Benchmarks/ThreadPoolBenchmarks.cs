using System.Threading;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.Threading.Benchmarks
{
    [IterationCount(64)]
    public class ThreadPoolBenchmarks: BenchmarkBase
    {
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
            var completedEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(_ => { completedEvent.Set(); });
            completedEvent.WaitOne();
        }

        [Baseline]
        [Benchmark]
        public void SimpleThread()
        {
            var completedEvent = new ManualResetEvent(false);
            var thread = new Thread(() => { completedEvent.Set(); });
            thread.Start();
            completedEvent.WaitOne();
        }
    }
}
