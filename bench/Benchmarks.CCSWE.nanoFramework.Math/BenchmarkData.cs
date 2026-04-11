using System;

namespace Benchmarks.CCSWE.nanoFramework;

internal static class BenchmarkData
{
    public const int Iterations = 250;
    public const int Loops = 100;

    public const double DoublePositive1 = Math.PI;
    public const double DoublePositive2 = DoublePositive1 * 2;
    public const double DoubleNegative1 = DoublePositive1 * -1;

    public const float FloatPositive1 = (float)Math.PI;
    public const float FloatPositive2 = FloatPositive1 * 2;
    public const float FloatNegative1 = FloatPositive1 * -1;

    public const int IntPositive1 = 123456789;
    public const int IntPositive2 = IntPositive1 * 2;
    public const int IntNegative1 = IntPositive1 * -1;

    public const long LongPositive1 = 12345678901234567;
    public const long LongPositive2 = LongPositive1 * 2;
    public const long LongNegative1 = LongPositive1 * -1;

    public const ulong ULongLow = 123456789012345678;
    public const ulong ULongMid = 987654321098765432;
    public const ulong ULongHigh = ULongMid * 2;
}