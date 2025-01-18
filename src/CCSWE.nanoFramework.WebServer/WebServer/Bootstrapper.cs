using System;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Diagnostics;
using CCSWE.nanoFramework.WebServer.Middleware;
using CCSWE.nanoFramework.WebServer.Reflection;
using CCSWE.nanoFramework.WebServer.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer
{
    /// <summary>
    /// Extension methods for <see cref="WebServer"/>.
    /// </summary>
    public static class Bootstrapper
    {
        /// <summary>
        /// Registers a new <see cref="IAuthenticationHandler"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="implementationType">The <see cref="Type"/> the implements <see cref="IAuthenticationHandler"/>.</param>
        // TODO: Add support for schemes
        public static IServiceCollection AddAuthentication(this IServiceCollection services, Type implementationType)
        {
            Ensure.IsNotNull(implementationType);

            if (!ReflectionHelper.IsAuthenticationHandler(implementationType) || implementationType.IsAbstract)
            {
                throw new InvalidOperationException();
            }

            services.AddSingleton(typeof(AuthenticationHandlerDescriptor), new AuthenticationHandlerDescriptor(implementationType));

            return services;
        }

        /// <summary>
        /// Registers a new <see cref="ControllerBase"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="implementationType">The <see cref="Type"/> the implements <see cref="ControllerBase"/>.</param>
        public static IServiceCollection AddController(this IServiceCollection services, Type implementationType)
        {
            Ensure.IsNotNull(implementationType);

            if (!ReflectionHelper.IsControllerBase(implementationType) || implementationType.IsAbstract)
            {
                throw new InvalidOperationException();
            }

            services.AddSingleton(typeof(ControllerDescriptor), new ControllerDescriptor(implementationType));

            return services;
        }

        /// <summary>
        /// Registers a new <see cref="IMiddleware"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="implementationType">The <see cref="Type"/> the implements <see cref="IMiddleware"/>.</param>
        public static IServiceCollection AddMiddleware(this IServiceCollection services, Type implementationType)
        {
            Ensure.IsNotNull(implementationType);

            if (!ReflectionHelper.IsMiddleware(implementationType) || implementationType.IsAbstract)
            {
                throw new InvalidOperationException();
            }

            var count = services.Count;
            for (var index = 0; index < count; ++index)
            {
                var service = services[index];

                if (service.ServiceType != ReflectionHelper.MiddlewareType)
                {
                    continue;
                }

                if (service.ImplementationInstance is not MiddlewareFactory middlewareFactory)
                {
                    continue;
                }

                // This middleware has already been added
                if (middlewareFactory.MiddlewareType == implementationType)
                {
                    return services;
                }
            }

            services.AddSingleton(typeof(IMiddlewareFactory), new MiddlewareFactory(implementationType));
            
            return services;
        }

        /// <summary>
        /// Adds an <see cref="WebServer"/> with the specified <see cref="WebServerOptions"/>.
        /// </summary>
        public static IServiceCollection AddWebServer(this IServiceCollection services, ConfigureWebServerOptions? configureOptions = null)
        {
            services.AddSingleton(typeof(IWebServer), typeof(WebServer));
            var options = new WebServerOptions();
            configureOptions?.Invoke(options);
            services.AddSingleton(typeof(WebServerOptions), options);

            services.AddSingleton(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddSingleton(typeof(IEndpointProvider), typeof(EndpointProvider));
            services.AddScoped(typeof(IRequestPipeline), typeof(RequestPipeline));

            return services;
        }
    }
}
