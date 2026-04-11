using System;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework;

public abstract class BenchmarkBase
{
    /// <summary>
    /// Invokes the specified action a given number of times in sequence.
    /// </summary>
    /// <param name="iterations">The number of times to execute the action. Must be greater than or equal to zero.</param>
    /// <param name="action">The action to execute on each iteration. Cannot be null.</param>
    /// <remarks>
    /// I use this in conjunction with the <see cref="IterationCountAttribute"/> attribute so that enough time is spent in a single
    /// iteration to get meaningful timings.
    /// </remarks>
    public void RunIterations(int iterations, Action action)
    {
        for (var i = 0; i < iterations; i++)
        {
            action();
        }
    }
}