using System;
using System.Collections;
using System.Net;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Cors;
using CCSWE.nanoFramework.WebServer.Diagnostics;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Middleware;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer
{
    internal interface IRequestPipeline
    {
        /// <summary>
        /// Produces a <see cref="RequestDelegate"/> that executes added middlewares.
        /// </summary>
        /// <returns>The <see cref="RequestDelegate"/>.</returns>
        RequestDelegate Build();
    }

    // TODO: Add unit tests
    internal class RequestPipeline : IRequestPipeline
    {
        private readonly CreateMiddlewareDelegate[] _middleware;

        public RequestPipeline(IServiceProvider serviceProvider)
        {
            /* Middleware order:
             * 1. ExceptionHandlerMiddleware
             * 2. RequestLoggingMiddleware (optional) [TODO]
             * 3. RoutingMiddleware
             * 4. StaticFilesMiddleware [TODO]
             * 5. CorsMiddleware
             * 6. AuthenticationMiddleware
             * X. User defined middleware
             * 8. EndpointMiddleware
             */

            var middleware = new ArrayList
            {
                BindMiddleware(typeof(ExceptionHandlerMiddleware)),
                // TODO: Add optional RequestLoggingMiddleware
                BindMiddleware(typeof(RoutingMiddleware)),
            };

            if (IsStaticFileMiddlewareEnabled(serviceProvider))
            {
                middleware.Add(BindMiddleware(typeof(StaticFileMiddleware)));
            }

            if (IsCorsMiddlewareEnabled(serviceProvider))
            {
                middleware.Add(BindMiddleware(typeof(CorsMiddleware)));
            }

            if (IsAuthenticationMiddlewareEnabled(serviceProvider))
            {
                middleware.Add(BindMiddleware(typeof(AuthenticationMiddleware)));
            }

            // Add the user defined middleware
            var middlewareFactories = (MiddlewareFactory[]) serviceProvider.GetServices(typeof(MiddlewareFactory)).ToArray(typeof(MiddlewareFactory));
            foreach (var middlewareFactory in middlewareFactories)
            {
                middleware.Add(BindMiddleware(middlewareFactory));
            }

            // EndpointMiddleware should always be last
            middleware.Add(BindMiddleware(typeof(EndpointMiddleware)));

            _middleware = (CreateMiddlewareDelegate[])middleware.ToArray(typeof(CreateMiddlewareDelegate));
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                /* TODO: Implement something like this
                // If we reach the end of the pipeline, but we have an endpoint, then something unexpected has happened.
                // This could happen if user code sets an endpoint, but they forgot to add the UseEndpoint middleware.
                var endpoint = context.GetEndpoint();
                var endpointRequestDelegate = endpoint?.RequestDelegate;
                if (endpointRequestDelegate != null)
                {
                    var message =
                        $"The request reached the end of the pipeline without executing the endpoint: '{endpoint!.DisplayName}'. " +
                        $"Please register the EndpointMiddleware using '{nameof(IApplicationBuilder)}.UseEndpoints(...)' if using " +
                        $"routing.";
                    throw new InvalidOperationException(message);
                }
                */

                // Flushing the response and calling through to the next middleware in the pipeline is
                // a user error, but don't attempt to set the status code if this happens. It leads to a confusing
                // behavior where the client response looks fine, but the server side logic results in an exception.

                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode(HttpStatusCode.NotFound);
                }

                context.Close();
            };

            // Loop through the middleware and have it recursively call the next...
            for (var c = _middleware.Length - 1; c >= 0; c--)
            {
                app = _middleware[c](app);
            }

            return app;
        }

        private static CreateMiddlewareDelegate BindMiddleware(IMiddlewareFactory middlewareFactory)
        {
            return middlewareFactory.CreateMiddleware;
        }

        private static CreateMiddlewareDelegate BindMiddleware(Type middlewareType)
        {
            return new MiddlewareFactory(middlewareType).CreateMiddleware;
        }

        public static void Execute(HttpContext context)
        {
            if (context.RequestServices.GetService(typeof(IRequestPipeline)) is not IRequestPipeline requestPipeline)
            {
                throw new InvalidOperationException($"Failed to create {nameof(IRequestPipeline)}");
            }

            requestPipeline.Build().Invoke(context);
            context.Close();
        }

        private static bool IsAuthenticationMiddlewareEnabled(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(IAuthenticationService)) is IAuthenticationService;
        }

        private static bool IsCorsMiddlewareEnabled(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(CorsPolicy)) is CorsPolicy;
        }

        private static bool IsStaticFileMiddlewareEnabled(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(IContentTypeProvider)) is IContentTypeProvider && serviceProvider.GetService(typeof(IFileProvider)) is IFileProvider;
        }
    }
}
