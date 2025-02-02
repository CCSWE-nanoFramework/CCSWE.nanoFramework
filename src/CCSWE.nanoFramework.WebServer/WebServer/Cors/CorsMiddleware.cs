using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Http.Headers;

namespace CCSWE.nanoFramework.WebServer.Cors
{
    /// <summary>
    /// A middleware for handling CORS.
    /// </summary>
    internal class CorsMiddleware : IMiddleware
    {
        private readonly CorsPolicy _policy;

        public CorsMiddleware(CorsPolicy policy)
        {
            _policy = policy;
        }

        /// <inheritdoc />
        public void Invoke(HttpContext context, RequestDelegate next)
        {
            var response = context.Response;

            if (!response.HasStarted)
            {
                response.Headers.Add(HeaderNames.AccessControlAllowHeaders, _policy.AccessControlAllowHeaders);
                response.Headers.Add(HeaderNames.AccessControlAllowMethods, _policy.AccessControlAllowMethods);
                response.Headers.Add(HeaderNames.AccessControlAllowOrigin, _policy.AccessControlAllowOrigin);
            }

            next.Invoke(context);
        }
    }
}
