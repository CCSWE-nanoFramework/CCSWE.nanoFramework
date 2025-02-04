using System.IO;
using System.Net;
using System.Text;
using CCSWE.nanoFramework.Net;
using nanoFramework.Json;

// TODO: Add unit tests
namespace CCSWE.nanoFramework.WebServer.Http
{
    /// <summary>
    /// Extension methods for <see cref="HttpResponse"/>
    /// </summary>
    public static class HttpResponseExtensions
    {
        private const int BufferSize = 1024;

        /*
        /// <summary>
        /// Add CORS headers to the <see cref="HttpResponse"/>.
        /// </summary>
        public static void AddCors(this HttpResponse response)
        {
            response.Headers.Add(HeaderNames.AccessControlAllowHeaders, CorsConstants.AnyHeader);
            response.Headers.Add(HeaderNames.AccessControlAllowMethods, CorsConstants.AnyMethod);
            response.Headers.Add(HeaderNames.AccessControlAllowOrigin, CorsConstants.AnyOrigin);
        }
        */

        /// <summary>
        /// Sets the <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> to write to.</param>
        /// <param name="statusCode">The <see cref="HttpStatusCode"/>.</param>
        /// <param name="statusDescription">The description of the HTTP status code returned to the client.</param>
        public static void StatusCode(this HttpResponse response, HttpStatusCode statusCode, string? statusDescription = null)
        {
            response.StatusCode = (int)statusCode;

            if (!string.IsNullOrEmpty(statusDescription))
            {
                response.StatusDescription = statusDescription;
            }
        }

        /// <summary>
        /// Serializes <paramref name="body"/> to JSON and writes it to the output stream.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> to write to.</param>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type. Defaults to <see cref="MimeType.Application.Json"/>.</param>
        public static void Write(this HttpResponse response, object? body, string? contentType = null)
        {
            if (body is null)
            {
                // TODO: I don't like that this hack is necessary, I'm going to look into re-writing HttpListener as this should be handled in the Close/Dispose
                response.ContentLength = 0;
                response.KeepAlive = false;
                return;
            }

            var bytes = Encoding.UTF8.GetBytes(body as string ?? JsonConvert.SerializeObject(body));

            response.Write(bytes, contentType ?? MimeType.Application.Json);
        }

        /// <summary>
        /// Writes the <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> to write to.</param>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type. Defaults to <see cref="MimeType.Application.Json"/>.</param>
        public static void Write(this HttpResponse response, byte[] body, string? contentType = null)
        {
            //TODO: I could reuse the stream method here, but I need to test which path is optimal
            //using var stream = new MemoryStream(body, false);
            //response.Write(stream, contentType);

            response.ContentLength = body.Length;
            response.ContentType = contentType;
            response.SendChunked = response.ContentLength > BufferSize;

            for (var bytesSent = 0L; bytesSent < body.Length;)
            {
                var bytesToSend = body.Length - bytesSent;
                bytesToSend = bytesToSend < BufferSize ? bytesToSend : BufferSize;

                response.Body.Write(body, (int)bytesSent, (int)bytesToSend);
                bytesSent += bytesToSend;
            }
        }

        /// <summary>
        /// Writes the <paramref name="body"/> to the output stream.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> to write to.</param>
        /// <param name="body">The body content.</param>
        /// <param name="contentType">The content type. Defaults to <see cref="MimeType.Application.Octet"/>.</param>
        public static void Write(this HttpResponse response, Stream body, string? contentType = null)
        {
            response.ContentLength = body.Length;
            response.ContentType = contentType ?? MimeType.Application.Octet;
            response.SendChunked = response.ContentLength > BufferSize;

            var buffer = new byte[BufferSize];
            var bytesSent = 0L;
            int bytesToSend;

            // Even though I fixed the issue with MemoryStream.Read(SpanByte buffer) (https://github.com/nanoframework/Home/issues/1598)
            // Not all streams implement the Read(SpanByte buffer) method, so I'm going to have to use the Read(byte[] buffer, int offset, int count) method for now
            // while ((bytesToSend = body.Read(buffer)) > 0)
            while ((bytesToSend = body.Read(buffer, 0, buffer.Length)) > 0)
            {
                response.Body.Write(buffer, (int)bytesSent, bytesToSend);
                bytesSent += bytesToSend;
            }
        }
    }
}
