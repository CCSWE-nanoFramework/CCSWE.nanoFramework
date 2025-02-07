using System;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Cors;
using CCSWE.nanoFramework.WebServer.Internal;
using CCSWE.nanoFramework.WebServer.Middleware;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.StaticFiles;
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
            ArgumentNullException.ThrowIfNull(implementationType);

            if (!WebServerTypeUtils.IsAuthenticationHandler(implementationType) || implementationType.IsAbstract)
            {
                throw new InvalidOperationException();
            }

            services.AddAuthenticationCore();
            services.AddSingleton(typeof(AuthenticationHandlerDescriptor), new AuthenticationHandlerDescriptor(implementationType));

            return services;
        }

        private static IServiceCollection AddAuthenticationCore(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(IAuthenticationService), typeof(AuthenticationService));

            return services;
        }

        /// <summary>
        /// Registers a new <see cref="ControllerBase"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="implementationType">The <see cref="Type"/> the implements <see cref="ControllerBase"/>.</param>
        public static IServiceCollection AddController(this IServiceCollection services, Type implementationType)
        {
            ArgumentNullException.ThrowIfNull(implementationType);

            if (!WebServerTypeUtils.IsControllerBase(implementationType) || implementationType.IsAbstract)
            {
                throw new InvalidOperationException();
            }

            services.AddSingleton(typeof(ControllerDescriptor), new ControllerDescriptor(implementationType));

            return services;
        }

        /// <summary>
        /// Registers CORS middleware.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddCors(this IServiceCollection services)
        {
            var descriptor = new ServiceDescriptor(typeof(CorsPolicy), CorsPolicy.AllowAny);
            services.TryAdd(descriptor);

            return services;
        }

        /// <summary>
        /// Registers a new <see cref="IMiddleware"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="implementationType">The <see cref="Type"/> the implements <see cref="IMiddleware"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddMiddleware(this IServiceCollection services, Type implementationType)
        {
            ArgumentNullException.ThrowIfNull(implementationType);

            if (!WebServerTypeUtils.IsMiddleware(implementationType) || implementationType.IsAbstract)
            {
                throw new InvalidOperationException();
            }

            var count = services.Count;
            for (var index = 0; index < count; ++index)
            {
                var service = services[index];

                if (service.ServiceType != WebServerTypeUtils.MiddlewareType)
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
        /// Add static files to the web server.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="fileProvider">The type that implements <see cref="IFileProvider"/>.</param>
        /// <param name="contentTypeProvider">The type that implements <see cref="IContentTypeProvider"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        // TODO: Add a default implementation for IFileProvider in another library
        public static IServiceCollection AddStaticFiles(this IServiceCollection services, Type fileProvider, Type? contentTypeProvider = null)
        {
            services.TryAddSingleton(typeof(IFileProvider), fileProvider);
            services.TryAddSingleton(typeof(IContentTypeProvider), contentTypeProvider ?? typeof(ContentTypeProvider));

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

            services.AddSingleton(typeof(IEndpointProvider), typeof(EndpointProvider));
            services.AddScoped(typeof(IRequestPipeline), typeof(RequestPipeline));

            return services;
        }
    }
}
