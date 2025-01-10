using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace CCSWE.nanoFramework.Core.Benchmarks
{
    [IterationCount(250)]
    public class EnsureBenchmarks: BenchmarkBase
    {
        private const string StringValue = nameof(EnsureBenchmarks);

        [Benchmark]
        public void IsInRange_Double()
        {
            Ensure.IsInRange(0d, 0, 1, nameof(IsInRange_Double));
        }

        [Benchmark]
        public void IsInRange_Expression()
        {
            const double value = 0d;
            const double min = 0d;
            const double max = 1d;

            #pragma warning disable CS8794 // The input always matches the provided pattern.
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Ensure.IsInRange(value is >= min and <= max, nameof(IsInRange_Expression));
            #pragma warning restore CS8794 // The input always matches the provided pattern.
        }

        [Benchmark]
        public void IsInRange_Float()
        {
            Ensure.IsInRange(0f, 0, 1);
        }

        [Benchmark]
        public void IsInRange_Int()
        {
            Ensure.IsInRange(0, 0, 1);
        }

        [Benchmark]
        public void IsInRange_Long()
        {
            Ensure.IsInRange(0L, 0, 1);
        }

        [Benchmark]
        public void IsNotNull()
        {
            Ensure.IsNotNull(StringValue);
        }

        [Benchmark]
        public void IsNotNullOrEmpty()
        {
            Ensure.IsNotNullOrEmpty(StringValue);
        }

        [Benchmark]
        public void IsValid()
        {
            const double value = 0d;
            const double min = 0d;
            const double max = 1d;

            #pragma warning disable CS8794 // The input always matches the provided pattern.
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Ensure.IsValid(value is >= min and <= max);
            #pragma warning restore CS8794 // The input always matches the provided pattern.
        }
    }
}
