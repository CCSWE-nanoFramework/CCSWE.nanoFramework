using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.Authentication
{
    internal sealed class AuthenticationMiddleware : IMiddleware
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationMiddleware(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void Invoke(HttpContext context, RequestDelegate next)
        {
            context.AuthenticateResult = _authenticationService.Authenticate(context);

            next.Invoke(context);
        }
    }
}
