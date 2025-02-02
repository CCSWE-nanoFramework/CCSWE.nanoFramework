using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.Routing
{
    // TODO: Add unit tests
    internal sealed class RoutingMiddleware: IMiddleware
    {
        private readonly IEndpointProvider _endpointProvider;

        public RoutingMiddleware(IEndpointProvider endpointProvider)
        {
            _endpointProvider = endpointProvider;
        }

        /// <inheritdoc />
        public void Invoke(HttpContext context, RequestDelegate next)
        {
            context.Endpoint = _endpointProvider.GetEndpoint(context);

            next.Invoke(context);
        }
    }
}
