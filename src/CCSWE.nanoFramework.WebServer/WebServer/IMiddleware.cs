using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Defines middleware that can be added to the application's request pipeline.
    /// </summary>
    public interface IMiddleware
    {
        /// <summary>Request handling method.</summary>
        /// <param name="context">The <see cref="HttpContext" /> for the current request.</param>
        /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
        void Invoke(HttpContext context, RequestDelegate next);
    }
}
