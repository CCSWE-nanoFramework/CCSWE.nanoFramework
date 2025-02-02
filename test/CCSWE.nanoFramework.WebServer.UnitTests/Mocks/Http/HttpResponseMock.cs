using System.IO;
using System.Net;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http
{
    internal class HttpResponseMock : HttpResponse
    {
        public HttpResponseMock(bool hasStarted = false)
        {
            HasStarted = hasStarted;
        }

        public override Stream Body { get; } = new MemoryStream();
        public override long ContentLength { get; set; }
        public override string? ContentType { get; set; }
        public override bool HasStarted { get; }
        public override WebHeaderCollection Headers { get; } = new();
        public override bool KeepAlive { get; set; }
        public override bool SendChunked { get; set; }
        public override int StatusCode { get; set; }
        public override string? StatusDescription { get; set; }

        public override void Close()
        {
            // Nothing to do here ...
        }
    }
}
