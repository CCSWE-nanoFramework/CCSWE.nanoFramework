using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http
{
    internal class HttpRequestMock : HttpRequest
    {
        public HttpRequestMock(string method, string url)
        {
            Method = HttpMethods.GetCanonicalizedValue(method);

            GetPathAndQueryString(url, out var path, out var queryString);

            Path = path;
            PathSegments = GetPathSegments(path);
            QueryString = queryString;
        }

        public override string Method { get; }
        public override string Path { get; }
        internal override string[] PathSegments { get; }
        public override string? QueryString { get; }

        public override void Close()
        {
            // Nothing to do here...
        }
    }
}
