using CCSWE.nanoFramework;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;

namespace Benchmarks.CCSWE.nanoFramework.Core;

[IterationCount(100)]
public class StringEqualsBenchmarks : BenchmarkBase
{
    private const int Loops = 50;

    // Separate constants for same-value pairs to avoid reference-equality shortcuts
    private const string Lower = "hello world";
    private const string LowerSame = "hello world";
    private const string LowerDifferentLength = "goodbye world";  // different length — exits at length check
    private const string LowerDifferentContent = "hello earth";   // same length — exits in character loop

    private const string Mixed = "Hello World";
    private const string Upper = "HELLO WORLD";

    // --- Both null: both return true ---

    [Benchmark]
    public void BothNull_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(null, null);
        });
    }

    [Benchmark]
    public void BothNull_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(null, null);
        });
    }

    // --- One null: both return false ---

    [Benchmark]
    public void OneNull_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(Lower.ToUpper(), ((string?) null)?.ToUpper());
        });
    }

    [Benchmark]
    public void OneNull_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(Lower, null);
        });
    }

    // --- Different length: both return false ---

    [Benchmark]
    public void DifferentLength_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(Lower.ToUpper(), LowerDifferentLength.ToUpper());
        });
    }

    [Benchmark]
    public void DifferentLength_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(Lower, LowerDifferentLength);
        });
    }

    // --- Same length, different content: both return false ---

    [Benchmark]
    public void DifferentContent_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(Lower.ToUpper(), LowerDifferentContent.ToUpper());
        });
    }

    [Benchmark]
    public void DifferentContent_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(Lower, LowerDifferentContent);
        });
    }

    // --- Equal, same lowercase: both return true ---

    [Benchmark]
    [Baseline]
    public void EqualLowerCase_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(Lower.ToUpper(), LowerSame.ToUpper());
        });
    }

    [Benchmark]
    public void EqualLowerCase_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(Lower, LowerSame);
        });
    }

    // --- Equal, mixed case: both return true ---

    [Benchmark]
    public void EqualMixedCase_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(Lower.ToUpper(), Mixed.ToUpper());
        });
    }

    [Benchmark]
    public void EqualMixedCase_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(Lower, Mixed);
        });
    }

    // --- Equal, upper vs lower: both return true ---

    [Benchmark]
    public void EqualUpperCase_System()
    {
        RunIterations(Loops, () =>
        {
            var equals = string.Equals(Lower.ToUpper(), Upper.ToUpper());
        });
    }

    [Benchmark]
    public void EqualUpperCase_EqualsIgnoreCase()
    {
        RunIterations(Loops, () =>
        {
            var equals = Strings.EqualsIgnoreCase(Lower, Upper);
        });
    }
}
