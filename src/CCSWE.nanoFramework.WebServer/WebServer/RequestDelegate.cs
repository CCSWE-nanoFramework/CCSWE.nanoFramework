using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>A function that can process an HTTP request.</summary>
    /// <param name="context">The <see cref="HttpContext" /> for the request.</param>
    public delegate void RequestDelegate(HttpContext context);
}
