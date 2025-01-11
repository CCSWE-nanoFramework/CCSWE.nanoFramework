using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using System;
using System.Device.Gpio;
using System.Text;

namespace CCSWE.nanoFramework.NeoPixel.Benchmarks
{
    [IterationCount(100)]
    public class DevelopmentBenchmarks : BenchmarkBase
    {
        private const int Iterations = 100;
        private const double R = 38.23d, G = 196.12d, B = 103.98d;

        [Benchmark]
        public void MinMax_Array_Math()
        {
            RunIterations(Iterations, () =>
            {
                MinMax_Array_Math_Implementation(out var min, out var max, R, G, B);
            });
        }

        [Benchmark]
        public void MinMax_Array_Simple()
        {
            RunIterations(Iterations, () =>
            {
                MinMax_Array_Simple_Implementation(out var min, out var max, R, G, B);
            });
        }

        [Benchmark]
        public void MinMax_Params_Simple()
        {
            RunIterations(Iterations, () =>
            {
                MinMax_Params_Simple_Implementation(out var min, out var max, R, G, B);
            });
        }

        private static void MinMax_Array_Math_Implementation(out double min, out double max, params double[] values)
        {
            if (values is null || values.Length <= 0)
            {
                throw new ArgumentNullException(nameof(values));
            }

            max = values[0];
            min = values[0];

            foreach (var value in values)
            {
#pragma warning disable CS0618 // Type or member is obsolete
                // There appears to be an issue, triggered by scaling Color.Green, with the Math.Max/Math.Min implementation. Gather more details.
                max = FastMath.Max(max, value);
                min = FastMath.Min(min, value);
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }

        private static void MinMax_Array_Simple_Implementation(out double min, out double max, params double[] values)
        {
            if (values is null || values.Length <= 0)
            {
                throw new ArgumentNullException(nameof(values));
            }

            max = values[0];
            min = values[0];

            foreach (var value in values)
            {
                max = value < max ? max : value;
                min = value > min ? min : value;
            }
        }

        private static void MinMax_Params_Simple_Implementation(out double min, out double max, double r, double g, double b)
        {
            if (r > g)
            {
                max = r;
                min = g;
            }
            else
            {
                max = g;
                min = r;
            }
            if (b > max)
            {
                max = b;
            }
            else if (b < min)
            {
                min = b;
            }
        }
    }
}
