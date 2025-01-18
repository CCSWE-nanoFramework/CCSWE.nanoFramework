using System.Net;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.Routing
{
    // TODO: Add unit tests
    internal class EndpointMiddleware: IMiddleware
    {
        public void Invoke(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.Endpoint;

            if (endpoint is null)
            {
                next(context);
                return;
            }

            if (endpoint.RequireAuthentication)
            {
                // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
                var authenticateResult = context.AuthenticateResult ?? AuthenticateResult.NoResult();
                if (authenticateResult.None || !authenticateResult.Succeeded)
                {
                    context.Response.StatusCode(HttpStatusCode.Unauthorized, authenticateResult.Failure);
                    return;
                }
            }

            endpoint.Invoke(context);
        }
    }
}
