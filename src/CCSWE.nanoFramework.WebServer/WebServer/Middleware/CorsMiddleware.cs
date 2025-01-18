using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.Middleware
{
    /// <summary>
    /// A middleware for handling CORS.
    /// </summary>
    public class CorsMiddleware: IMiddleware
    {
        /// <inheritdoc />
        public void Invoke(HttpContext context, RequestDelegate next)
        {
            var response = context.Response;

            if (!response.HasStarted)
            {
                response.AddCors();
            }

            next.Invoke(context);
        }
    }
}
