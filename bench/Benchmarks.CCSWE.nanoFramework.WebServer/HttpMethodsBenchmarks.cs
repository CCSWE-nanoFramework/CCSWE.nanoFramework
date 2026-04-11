using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using CCSWE.nanoFramework.WebServer.Http;

namespace Benchmarks.CCSWE.nanoFramework.WebServer;

[IterationCount(250)]
public class HttpMethodsBenchmarks : BenchmarkBase
{
    // A lowercase string guaranteed not to be the same instance as HttpMethods.Get,
    // so the ReferenceEquals fast path in EqualsOptimized is bypassed.
    private const string NonCanonicalGet = "get";

    [Baseline]
    [Benchmark]
    public void GetCanonicalizedValue_Canonicalized()
    {
        // Passes the static readonly instance — hits ReferenceEquals fast path.
        HttpMethods.GetCanonicalizedValue(HttpMethods.Get);
    }

    [Benchmark]
    public void GetCanonicalizedValue_NonCanonical()
    {
        // Passes a lowercase string — falls through to string.Equals + ToUpper().
        HttpMethods.GetCanonicalizedValue(NonCanonicalGet);
    }
}