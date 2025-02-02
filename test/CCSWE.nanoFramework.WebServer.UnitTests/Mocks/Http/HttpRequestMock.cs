using System.IO;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http
{
    internal class HttpRequestMock : HttpRequest
    {
        private Stream _body = new MemoryStream();
        private string? _contentType;

        public HttpRequestMock(string method, string url)
        {
            Method = HttpMethods.GetCanonicalizedValue(method);

            GetPathAndQueryString(url, out var path, out var queryString);

            Path = path;
            PathSegments = GetPathSegments(path);
            QueryString = queryString;
        }

        public override Stream Body => _body;
        public override long ContentLength => Body.Length;
        public override string? ContentType => _contentType;
        public override string Method { get; }
        public override string Path { get; }
        internal override string[] PathSegments { get; }
        public override string? QueryString { get; }

        public override void Close()
        {
            // Nothing to do here...
        }

        public void SetBody(Stream body)
        {
            _body = body;
        }

        public void SetContentType(string? contentType)
        {
            _contentType = contentType;
        }
    }
}
