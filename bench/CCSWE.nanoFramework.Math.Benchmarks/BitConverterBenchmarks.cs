using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
#pragma warning disable CS0618 // Type or member is obsolete

namespace CCSWE.nanoFramework.Benchmarks
{
    [IterationCount(Iterations)]
    public class BitConverterBenchmarks: BenchmarksBase
    {
        #region Int64
        [Benchmark]
        public void DoubleToInt64Bits_Fast()
        {
            RunIterations(Loops, () => { BitConverter.DoubleToInt64Bits(DoublePositive1); });
        }

        [Benchmark]
        public void DoubleToInt64Bits_System()
        {
            RunIterations(Loops, () => { System.BitConverter.DoubleToInt64Bits(DoublePositive1); });
        }

        [Benchmark]
        public void Int64BitsToDouble_Fast()
        {
            RunIterations(Loops, () => { BitConverter.Int64BitsToDouble(LongPositive1); });
        }

        [Benchmark]
        public void Int64BitsToDouble_System()
        {
            RunIterations(Loops, () => { System.BitConverter.Int64BitsToDouble(LongPositive1); });
        }
        #endregion

        #region UInt64
        [Benchmark]
        public void DoubleToUInt64Bits_Fast()
        {
            RunIterations(Loops, () => { BitConverter.DoubleToUInt64Bits(DoublePositive1); });
        }

        [Benchmark]
        public void UInt64BitsToDouble_Fast()
        {
            RunIterations(Loops, () => { BitConverter.UInt64BitsToDouble(ULongHigh); });
        }
        #endregion
    }
}
