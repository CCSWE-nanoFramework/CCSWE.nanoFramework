using System;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Authorization;
using CCSWE.nanoFramework.WebServer.Routing;

namespace CCSWE.nanoFramework.WebServer.Reflection
{
    internal static class ReflectionHelper
    {
        internal static readonly Type AllowAnonymousAttributeType = typeof(AllowAnonymousAttribute);
        internal static readonly Type AuthenticationHandlerType = typeof(IAuthenticationHandler);
        internal static readonly Type ControllerBaseType = typeof(ControllerBase);
        internal static readonly Type HttpMethodProviderType = typeof(IHttpMethodProvider);
        internal static readonly Type MiddlewareType = typeof(IMiddleware);
        internal static readonly Type RouteTemplateProviderType = typeof(IRouteTemplateProvider);

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

            return AllowAnonymousAttributeType == type || type.IsSubclassOf(AllowAnonymousAttributeType);
        }

        public static bool IsAuthenticationHandler(Type? type)
        {
            return type is not null && type.IsImplementationOf(AuthenticationHandlerType);
        }

        public static bool IsControllerBase(Type? type)
        {
            if (type is null)
            {
                return false;
            }

            return ControllerBaseType == type || type.IsSubclassOf(ControllerBaseType);
        }

        public static bool IsHttpMethodProvider(object? value)
        {
            return value is not null && IsHttpMethodProvider(value.GetType());
        }

        public static bool IsHttpMethodProvider(Type? type)
        {
            return type is not null && type.IsImplementationOf(HttpMethodProviderType);
        }

        public static bool IsMiddleware(Type? type)
        {
            return type is not null && type.IsImplementationOf(MiddlewareType);
        }

        public static bool IsRouteTemplateProvider(object? value)
        {
            return value is not null && IsRouteTemplateProvider(value.GetType());
        }

        public static bool IsRouteTemplateProvider(Type? type)
        {
            return type is not null && type.IsImplementationOf(RouteTemplateProviderType);
        }
    }
}
