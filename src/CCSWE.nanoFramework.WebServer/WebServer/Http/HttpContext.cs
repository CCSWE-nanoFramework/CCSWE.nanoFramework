using System;
using System.Net;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer.Http
{
    /// <summary>
    /// Encapsulates all HTTP-specific information about an individual HTTP request.
    /// </summary>
    public abstract class HttpContext
    {
        /// <summary>
        /// Gets the <see cref="AuthenticateResult"/> for the request.
        /// </summary>
        public AuthenticateResult AuthenticateResult { get; internal set; } = AuthenticateResult.NoResult();

        internal EndpointHandler? Endpoint { get; set; }

        /// <summary>
        /// Gets the <see cref="HttpRequest"/> for the request.
        /// </summary>
        public abstract HttpRequest Request { get; }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> that provides access to the request's service container.
        /// </summary>
        public abstract IServiceProvider RequestServices { get; }

        /// <summary>
        /// Gets the <see cref="HttpResponse"/> for the request.
        /// </summary>
        public abstract HttpResponse Response { get; }

        /// <summary>
        /// Close any resources for this request.
        /// </summary>
        public abstract void Close();

        internal static HttpContext Create(HttpListenerContext context, IServiceProvider requestServices) => new Internal.HttpContext(context, requestServices);
    }
}
