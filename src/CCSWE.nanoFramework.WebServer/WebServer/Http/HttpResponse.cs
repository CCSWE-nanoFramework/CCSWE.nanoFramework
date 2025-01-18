using System.IO;
using System.Net;

namespace CCSWE.nanoFramework.WebServer.Http
{
    /// <summary>
    /// Represents the outgoing side of an individual HTTP request.
    /// </summary>
    public abstract class HttpResponse
    {
        /// <summary>
        /// Gets the response body <see cref="Stream"/>.
        /// </summary>
        public abstract Stream Body { get; }

        /// <summary>
        /// Gets or sets the value for the <c>Content-Length</c> response header.
        /// </summary>
        public abstract long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the value for the <c>Content-Type</c> response header.
        /// </summary>
        public abstract string? ContentType { get; set; }

        /// <summary>
        /// Gets a value indicating whether response headers have been sent to the client.
        /// </summary>
        public abstract bool HasStarted { get; }

        /// <summary>
        /// Gets the response headers.
        /// </summary>
        public abstract WebHeaderCollection Headers { get; }

        /// <inheritdoc cref="HttpListenerResponse.KeepAlive"/>
        public abstract bool KeepAlive { get; set; }

        /// <inheritdoc cref="HttpListenerResponse.SendChunked"/>
        public abstract bool SendChunked { get; set; }

        /// <summary>
        /// Gets or sets the HTTP response code.
        /// </summary>
        public abstract int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a text description of the HTTP response code.
        /// </summary>
        public abstract string? StatusDescription { get; set; }

        /// <summary>
        /// Close any resources for this request.
        /// </summary>
        public abstract void Close();

        internal static HttpResponse Create(HttpListenerResponse response) => new Internal.HttpResponse(response);
    }
}
