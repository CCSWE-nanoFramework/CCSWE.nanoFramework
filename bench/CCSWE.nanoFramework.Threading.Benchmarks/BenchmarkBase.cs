using System;

namespace CCSWE.nanoFramework.Threading.Benchmarks
{
    public abstract class BenchmarkBase
    {
        public void RunIterations(Action action, int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                action();
            }
        }
    }
}
