using System.IO;
using System.Net;

namespace CCSWE.nanoFramework.WebServer.Http
{
    /// <summary>
    /// Represents the incoming side of an individual HTTP request.
    /// </summary>
    public abstract class HttpRequest
    {
        /// <summary>
        /// Gets the request body <see cref="Stream"/>.
        /// </summary>
        /// <value>The request body <see cref="Stream"/>.</value>
        public abstract Stream Body { get; }

        /// <summary>
        /// Gets the Content-Length header.
        /// </summary>
        /// <returns>The value of the Content-Length header, if any.</returns>
        public abstract long ContentLength { get; }

        /// <summary>
        /// Gets the Content-Type header.
        /// </summary>
        /// <returns>The Content-Type header.</returns>
        public abstract string? ContentType { get; }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        /// <returns>The HTTP method.</returns>
        public abstract string Method { get; }

        /// <summary>
        /// Gets the portion of the request path that identifies the requested resource.
        /// </summary>
        /// <returns>The path for the request.</returns>
        public abstract string Path { get; }

        internal abstract string[] PathSegments { get; }

        // TODO: Implement a QueryCollection

        /// <summary>
        /// Gets the raw query string used to create the query collection in Request.Query.
        /// </summary>
        /// <returns>The raw query string.</returns>
        public abstract string? QueryString { get; }

        /// <summary>
        /// Close any resources for this request.
        /// </summary>
        public abstract void Close();

        internal static HttpRequest Create(HttpListenerRequest request) => new Internal.HttpRequest(request);

        /// <summary>
        /// Split the requested url (without the host and port) into a path and query string.
        /// </summary>
        /// <param name="url">The requested url (without the host and port).</param>
        /// <param name="path">The path portion of the requested url.</param>
        /// <param name="queryString">The query string portion of the request url.</param>
        protected static void GetPathAndQueryString(string url, out string path, out string? queryString)
        {
            Ensure.IsNotNull(url);

            var urlParts = url.Split('?');

            path = urlParts[0].Trim('/');
            queryString = urlParts.Length > 1 ? urlParts[1] : null;
        }

        /// <summary>
        /// Split the <see cref="Path"/> into segments.
        /// </summary>
        /// <param name="path">The <see cref="Path"/> string.</param>
        protected static string[] GetPathSegments(string path)
        {
            return path.ToLower().Split('/');
        }
    }
}
