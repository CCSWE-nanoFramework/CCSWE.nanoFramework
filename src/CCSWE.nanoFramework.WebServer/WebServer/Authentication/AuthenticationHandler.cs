using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.Authentication
{
    /// <summary>
    /// An opinionated abstraction for implementing <see cref="IAuthenticationHandler"/>.
    /// </summary>
    public abstract class AuthenticationHandler: IAuthenticationHandler
    {
        /// <summary>
        /// Gets or sets the <see cref="HttpContext"/>.
        /// </summary>
        protected HttpContext Context { get; private set; } = null!;

        /// <summary>
        /// Gets the <see cref="HttpRequest"/> associated with the current request.
        /// </summary>
        protected HttpRequest Request => Context.Request;

        /// <summary>
        /// Gets the <see cref="HttpResponse" /> associated with the current request.
        /// </summary>
        protected HttpResponse Response => Context.Response;

        /// <inheritdoc />
        public abstract AuthenticateResult Authenticate();

        /// <inheritdoc />
        public void Initialize(HttpContext context)
        {
            Context = context;
        }
    }
}
