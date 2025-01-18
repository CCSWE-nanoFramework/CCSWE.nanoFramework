using System;
using System.Collections;
using CCSWE.nanoFramework.WebServer.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer.Authentication
{
    internal interface IAuthenticationService
    {
        AuthenticateResult Authenticate(HttpContext context);
    }

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly Type[] _authenticationHandlers;
        private readonly IServiceProvider _serviceProvider;

        public AuthenticationService(AuthenticationHandlerDescriptor[] descriptors, IServiceProvider serviceProvider)
        {
            var authenticationHandlers = new ArrayList();

            foreach (var descriptor in descriptors)
            {
                authenticationHandlers.Add(descriptor.ImplementationType);
            }

            _authenticationHandlers = (Type[]) authenticationHandlers.ToArray(typeof(Type));
            _serviceProvider = serviceProvider;
        }

        // TODO: Since we have no support for schemes yet multiple IAuthenticationHandler won't really work well
        public AuthenticateResult Authenticate(HttpContext context)
        {
            foreach (var authenticationHandlerType in _authenticationHandlers)
            {
                var authenticationHandler = (IAuthenticationHandler)ActivatorUtilities.CreateInstance(_serviceProvider, authenticationHandlerType);

                authenticationHandler.Initialize(context);

                var authenticateResult = authenticationHandler.Authenticate();

                if (authenticateResult.None)
                {
                    continue;
                }

                return authenticateResult;
            }

            return AuthenticateResult.NoResult();
        }
    }
}
