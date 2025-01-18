using System.IO;
using System.Net;
using CCSWE.nanoFramework.Net;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// A base class with helper methods for handling <see cref="HttpResponse"/>.
    /// </summary>
    public abstract class ControllerBase
    {
        /// <summary>
        /// Gets the <see cref="HttpContext"/> for the executing action.
        /// </summary>
        public HttpContext Context { get; internal set; } = null!;

        /// <summary>
        /// Gets the <see cref="HttpRequest"/> for the executing action.
        /// </summary>
        public HttpRequest Request => Context.Request;

        /// <summary>
        /// Gets the <see cref="HttpResponse"/> for the executing action.
        /// </summary>
        public HttpResponse Response => Context.Response;

        /// <summary>
        /// Sets the status code and writes <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="body">The body content.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/>.</param>
        /// <param name="contentType">The content type.</param>
        /// <remarks>
        /// If <paramref name="body"/> is an <see cref="object"/> it will be serialized as JSON.
        /// If <paramref name="body"/> is a <see cref="byte"/>[] or <see cref="Stream"/> it will be written as is.
        /// </remarks>
        protected void StatusCode(HttpStatusCode statusCode, object? body = null, string? contentType = null)
        {
/*            if (body is null)
            {
                Response.ContentLength = 0;
                Response.StatusCode(statusCode);
                Response.Body.Write([],0,0);
                Response.KeepAlive = false;

                return;
            }

*/            Response.StatusCode(statusCode);

            switch (body)
            {
                case byte[] bytes:
                {
                    Response.Write(bytes, contentType);
                    break;
                }
                case Stream stream:
                {
                    Response.Write(stream, contentType);
                    break;
                }
                default:
                {
                    Response.Write(body, contentType);
                    break;
                }
            }
        }

        /// <summary>
        /// Sets the status code to <see cref="HttpStatusCode.OK"/> and writes <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type.</param>
        /// <remarks>
        /// If <paramref name="body"/> is an <see cref="object"/> it will be serialized as JSON.
        /// If <paramref name="body"/> is a <see cref="byte"/>[] or <see cref="Stream"/> it will be written as is.
        /// </remarks>
        protected void Ok(object? body = null, string contentType = MimeType.Application.Json) => 
            StatusCode(HttpStatusCode.OK, body, contentType); // 200

        /// <summary>
        /// Sets the status code to <see cref="HttpStatusCode.BadRequest"/> and writes <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type.</param>
        /// <remarks>
        /// If <paramref name="body"/> is an <see cref="object"/> it will be serialized as JSON.
        /// If <paramref name="body"/> is a <see cref="byte"/>[] or <see cref="Stream"/> it will be written as is.
        /// </remarks>
        protected void BadRequest(object? body = null, string contentType = MimeType.Application.Json) => 
            StatusCode(HttpStatusCode.BadRequest, body, contentType); // 400

        /// <summary>
        /// Sets the status code to <see cref="HttpStatusCode.NotFound"/> and writes <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type.</param>
        /// <remarks>
        /// If <paramref name="body"/> is an <see cref="object"/> it will be serialized as JSON.
        /// If <paramref name="body"/> is a <see cref="byte"/>[] or <see cref="Stream"/> it will be written as is.
        /// </remarks>
        protected void NotFound(object? body = null, string contentType = MimeType.Application.Json) => 
            StatusCode(HttpStatusCode.NotFound, body, contentType); // 404

        /// <summary>
        /// Sets the status code to <see cref="HttpStatusCode.InternalServerError"/> and writes <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type.</param>
        /// <remarks>
        /// If <paramref name="body"/> is an <see cref="object"/> it will be serialized as JSON.
        /// If <paramref name="body"/> is a <see cref="byte"/>[] or <see cref="Stream"/> it will be  written as is.
        /// </remarks>
        protected void InternalServerError(object? body = null, string contentType = MimeType.Application.Json) => 
            StatusCode(HttpStatusCode.InternalServerError, body, contentType); // 500
    }
}
