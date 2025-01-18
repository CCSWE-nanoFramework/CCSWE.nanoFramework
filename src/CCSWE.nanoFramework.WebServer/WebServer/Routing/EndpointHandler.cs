using CCSWE.nanoFramework.WebServer.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer.Routing
{
    internal class EndpointHandler
    {
        private readonly Endpoint _endpoint;
        private readonly object[] _parameters;

        public EndpointHandler(Endpoint endpoint, object[] parameters)
        {
            _endpoint = endpoint;
            _parameters = parameters;
        }

        public bool RequireAuthentication => _endpoint.RequireAuthentication;

        public bool Invoke(HttpContext context)
        {
            var controller = (ControllerBase)ActivatorUtilities.CreateInstance(context.RequestServices, _endpoint.MethodInfo.DeclaringType);
            controller.Context = context;

            _endpoint.MethodInfo.Invoke(controller, _parameters);

            return true;
        }

        public override string ToString() => $"{nameof(EndpointHandler)}: {_endpoint}";
    }
}
