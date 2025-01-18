using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.Authentication
{
    /// <summary>
    /// Created per request to handle authentication.
    /// </summary>
    public interface IAuthenticationHandler
    {
        /// <summary>Authenticate the current request.</summary>
        /// <returns>The <see cref="AuthenticateResult" /> result.</returns>
        AuthenticateResult Authenticate();

        /// <summary>
        /// Initialize the handler for the current request.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        void Initialize(HttpContext context);
    }
}
