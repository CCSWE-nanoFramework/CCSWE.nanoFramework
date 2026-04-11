using System.IO;
using nanoFramework.Benchmark;
using nanoFramework.Benchmark.Attributes;
using Benchmarks.CCSWE.nanoFramework.WebServer.Mocks.Http;
using CCSWE.nanoFramework.WebServer.Http;

namespace Benchmarks.CCSWE.nanoFramework.WebServer;

[IterationCount(250)]
public class HttpResponseExtensionsBenchmarks : BenchmarkBase
{
    private static readonly byte[] SmallPayload = new byte[64];
    private static readonly MemoryStream SmallMemoryStream = new(SmallPayload);
    private static readonly byte[] LargePayload = new byte[8192];
    private static readonly MemoryStream LargeMemoryStream = new(SmallPayload);

    private static readonly object TestObject = new BenchmarkTestClass();
    private static readonly string TestString = "\"hello\": \"world\"";
    
    private HttpResponseMock _response = new();

    [Setup]
    public void Setup()
    {
        _response = new HttpResponseMock();
    }

    [Baseline]
    [Benchmark]
    public void Write_ByteArray_Large()
    {
        _response.Write(LargePayload);
    }

    [Benchmark]
    public void Write_ByteArray_Small()
    {
        _response.Write(SmallPayload);
    }

    [Benchmark]
    public void Write_Stream_Large()
    {
        LargeMemoryStream.Seek(0, SeekOrigin.Begin);
        _response.Write(LargeMemoryStream);
    }

    [Benchmark]
    public void Write_Stream_Small()
    {
        SmallMemoryStream.Seek(0, SeekOrigin.Begin);
        _response.Write(SmallMemoryStream);
    }

    [Benchmark]
    public void Write_Object()
    {
        _response.Write(TestObject);
    }

    [Benchmark]
    public void Write_String()
    {
        _response.Write(TestString);
    }
    
    private class BenchmarkTestClass
    {
        public string Hello { get; } = "World";
    }
}