using System;
using System.Collections;
using System.Reflection;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Internal;
using Microsoft.Extensions.Logging;

namespace CCSWE.nanoFramework.WebServer.Routing
{
    internal interface IEndpointProvider
    {
        EndpointHandler? GetEndpoint(HttpContext context);
    }

    internal class EndpointProvider : IEndpointProvider
    {
        private readonly Endpoint[] _endpoints;
        private readonly ILogger _logger;

        public EndpointProvider(ControllerDescriptor[] descriptors, ILogger logger)
        {
            _logger = logger;

            var controllers = new ArrayList();

            foreach (var descriptor in descriptors)
            {
                controllers.Add(descriptor.ImplementationType);
            }

            _endpoints = CreateEndpoints((Type[])controllers.ToArray(typeof(Type)));
        }

        /// <summary>
        /// Get <see cref="Endpoint"/> from <see cref="MethodInfo"/>.
        /// </summary>
        /// <returns>An array of <see cref="Endpoint"/>.</returns>
        private Endpoint[] CreateEndpoints(MethodInfo[] methodInfos, bool controllerRequiresAuthentication, string? controllerRouteTemplate)
        {
            var descriptors = new ArrayList();

            foreach (var methodInfo in methodInfos)
            {
                var attributes = methodInfo.GetCustomAttributes(true);
                var httpMethodProviders = new ArrayList();
                var requireAuthentication = controllerRequiresAuthentication;

                foreach (var attribute in attributes)
                {
                    if (WebServerTypeUtils.IsAllowAnonymousAttribute(attribute))
                    {
                        requireAuthentication = false;
                    }
                    else if (WebServerTypeUtils.IsHttpMethodProvider(attribute))
                    {
                        httpMethodProviders.Add(attribute);
                    }
                }

                // TODO: Need to confirm method parameter count matches template parameter count
                foreach (var attribute in httpMethodProviders)
                {
                    if (attribute is not IHttpMethodProvider httpMethodProvider)
                    {
                        continue;
                    }

                    var template = controllerRouteTemplate ?? string.Empty;

                    if (WebServerTypeUtils.IsRouteTemplateProvider(httpMethodProvider))
                    {
                        var routeTemplate = ((IRouteTemplateProvider)httpMethodProvider).Template;

                        if (!string.IsNullOrEmpty(routeTemplate))
                        {
                            template = routeTemplate.StartsWith("/") ? routeTemplate : UrlHelper.CombinePathSegments(template, routeTemplate);
                        }
                    }

                    if (string.IsNullOrEmpty(template))
                    {
                        _logger.LogWarning($"No route template for method: [{Strings.Join(", ", httpMethodProvider.HttpMethods)}] {methodInfo.ToDisplayName()}");

                        continue;
                    }

                    foreach (var httpMethod in httpMethodProvider.HttpMethods)
                    {
                        var descriptor = new Endpoint(httpMethod, methodInfo, template, requireAuthentication);
                        
                        descriptors.Add(descriptor);
                        
                        _logger.LogTrace($"Added controller route: {descriptor}");
                    }
                }
            }

            return descriptors.Count > 0 ? (Endpoint[])descriptors.ToArray(typeof(Endpoint)) : Endpoint.EmptyArray;
        }

        /// <summary>
        /// Get <see cref="Endpoint"/>> from <see cref="ControllerBase"/> types.
        /// </summary>
        /// <param name="controllers">An array of types that implement <see cref="ControllerBase"/>.</param>
        /// <returns>An array of <see cref="Endpoint"/>.</returns>
        private Endpoint[] CreateEndpoints(Type[] controllers)
        {
            var descriptors = new ArrayList();
            
            foreach (var controller in controllers)
            {
                if (!WebServerTypeUtils.IsControllerBase(controller))
                {
                    continue;
                }

                var requireAuthentication = true;
                var routeTemplate = string.Empty;

                var attributes = controller.GetCustomAttributes(true);

                foreach (var attribute in attributes)
                {
                    if (WebServerTypeUtils.IsAllowAnonymousAttribute(attribute))
                    {
                        requireAuthentication = false;
                    } 
                    else if (WebServerTypeUtils.IsRouteTemplateProvider(attribute))
                    {
                        routeTemplate = ((IRouteTemplateProvider)attribute).Template;
                    }
                }

                descriptors.AddRange(CreateEndpoints(controller.GetMethods(BindingFlags.Instance | BindingFlags.Public), requireAuthentication, routeTemplate));
            }

            return descriptors.Count > 0 ? (Endpoint[])descriptors.ToArray(typeof(Endpoint)) : Endpoint.EmptyArray;
        }

        public EndpointHandler? GetEndpoint(HttpContext context)
        {
            EndpointHandler? requestHandler = null;

            var maxSegmentsMatched = -1;
            var request = context.Request;

            foreach (var descriptor in _endpoints)
            {
                if (!HttpMethods.Equals(descriptor.HttpMethod, context.Request.Method))
                {
                    continue;
                }

                if (request.PathSegments.Length != descriptor.RouteSegments.Length)
                {
                    continue;
                }

                var isMatch = true;
                var routeParameters = new object[descriptor.ParameterIndexes.Length];
                var routeParameterIndex = 0;
                var segmentsMatched = 0;

                for (var i = 0; i < descriptor.RouteSegments.Length; i++)
                {
                    if (descriptor.ParameterIndexes.Length > 0 && i == descriptor.ParameterIndexes[routeParameterIndex])
                    {
                        routeParameters[routeParameterIndex] = request.PathSegments[i];
                        routeParameterIndex++;

                        continue;
                    }

                    if (descriptor.RouteSegments[i] == request.PathSegments[i])
                    {
                        segmentsMatched++;

                        continue;
                    }

                    isMatch = false;
                    break;
                }

                if (isMatch)
                {
                    if (segmentsMatched == maxSegmentsMatched)
                    {
                        throw new InvalidOperationException("Multiple routes match");
                    }

                    if (segmentsMatched > maxSegmentsMatched)
                    {
                        maxSegmentsMatched = segmentsMatched;
                        requestHandler = new EndpointHandler(descriptor, routeParameters);
                    }
                }
            }

            return requestHandler;
        }
    }
}
