using System;
using System.Diagnostics.CodeAnalysis;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Authorization;
using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer.Internal
{
    internal static class WebServerTypeUtils
    {
        private static readonly Type AllowAnonymousAttributeType = typeof(AllowAnonymousAttribute);
        private static readonly Type AuthenticationHandlerType = typeof(IAuthenticationHandler);
        private static readonly Type ControllerBaseType = typeof(ControllerBase);
        private static readonly Type HttpMethodProviderType = typeof(IHttpMethodProvider);
        private static readonly Type MiddlewareType = typeof(IMiddleware);
        private static readonly Type RouteTemplateProviderType = typeof(IRouteTemplateProvider);

        public static bool IsAllowAnonymousAttribute(object? value)
        {
            if (value is null)
            {
                return false;
            }

            return value is AllowAnonymousAttribute || IsAllowAnonymousAttribute(value.GetType());
        }

        public static bool IsAllowAnonymousAttribute(Type? type)
        {
            if (type is null)
            {
                return false;
            }

            return (AllowAnonymousAttributeType == type || type.IsSubclassOf(AllowAnonymousAttributeType)) && !type.IsAbstract;
        }

        public static bool IsAuthenticationHandler([NotNullWhen(true)] Type? type)
        {
            return type is not null && type.IsImplementationOf(AuthenticationHandlerType) && !type.IsAbstract;
        }

        public static bool IsControllerBase([NotNullWhen(true)] Type? type)
        {
            return type is not null && type.IsSubclassOf(ControllerBaseType) && !type.IsAbstract;
        }

        public static bool IsHttpMethodProvider([NotNullWhen(true)] object? value)
        {
            return value is not null && IsHttpMethodProvider(value.GetType());
        }

        public static bool IsHttpMethodProvider([NotNullWhen(true)] Type? type)
        {
            return type is not null && type.IsImplementationOf(HttpMethodProviderType) && !type.IsAbstract;
        }

        public static bool IsMiddleware([NotNullWhen(true)] Type? type)
        {
            return type is not null && type.IsImplementationOf(MiddlewareType) && !type.IsAbstract;
        }

        public static bool IsRouteTemplateProvider([NotNullWhen(true)]object? value)
        {
            return value is not null && IsRouteTemplateProvider(value.GetType());
        }

        public static bool IsRouteTemplateProvider([NotNullWhen(true)] Type? type)
        {
            return type is not null && type.IsImplementationOf(RouteTemplateProviderType) && !type.IsAbstract;
        }

        public static void RequireAuthenticationHandler(Type eventType)
        {
            if (!IsAuthenticationHandler(eventType))
            {
                throw new ArgumentException($"{eventType.Name} does not implement {nameof(IAuthenticationHandler)}");
            }
        }

        public static void RequireControllerBase(Type eventType)
        {
            if (!IsControllerBase(eventType))
            {
                throw new ArgumentException($"{eventType.Name} does not extend {nameof(ControllerBase)}");
            }
        }

        public static void RequireMiddleware(Type eventType)
        {
            if (!IsMiddleware(eventType))
            {
                throw new ArgumentException($"{eventType.Name} does not implement {nameof(IMiddleware)}");
            }
        }
    }
}
