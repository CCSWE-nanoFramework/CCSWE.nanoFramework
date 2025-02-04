using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;

namespace CCSWE.nanoFramework.WebServer.Http.Internal
{
    // TODO: Add unit tests
    internal class HttpRequest : Http.HttpRequest
    {
        private Stream? _body;
        private string? _method;
        private string? _path;
        private string[]? _pathSegments;
        private string? _queryString;
        private readonly HttpListenerRequest _request;

        public HttpRequest(HttpListenerRequest request)
        {
            _request = request;
        }

        public override Stream Body => _body ??= _request.InputStream ?? new MemoryStream();

        public override long ContentLength => _request.ContentLength64;

        public override string? ContentType => _request.ContentType;

        public override string Method => _method ??= HttpMethods.GetCanonicalizedValue(_request.HttpMethod);

        /// <summary>
        /// Gets the portion of the request path that identifies the requested resource.
        /// </summary>
        /// <returns>The path for the request.</returns>
        public override string Path
        {
            get
            {
                if (_path is null)
                {
                    InitializePath();
                }

                return _path;
            }
        }

        internal override string[] PathSegments
        {
            get
            {
                if (_pathSegments is null)
                {
                    InitializePathSegments();
                }

                return _pathSegments;
            }
        }

        /// <summary>
        /// Gets the raw query string used to create the query collection in Request.Query.
        /// </summary>
        /// <returns>The raw query string.</returns>
        public override string? QueryString
        {
            get
            {
                if (_path is null)
                {
                    InitializePath();
                }

                return _queryString;
            }
        }

        public override void Close()
        {
            // Nothing to do here...
        }

        [MemberNotNull(nameof(_path))]
        private void InitializePath()
        {
            GetPathAndQueryString(_request.RawUrl, out var path, out var queryString);

            _path = path;
            _queryString = queryString;
        }

        [MemberNotNull(nameof(_pathSegments))]
        private void InitializePathSegments()
        {
            _pathSegments = GetPathSegments(Path);
        }
    }
}
