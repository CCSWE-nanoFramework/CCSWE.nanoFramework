using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Cors;
using CCSWE.nanoFramework.WebServer.StaticFiles;
using System;

namespace CCSWE.nanoFramework.WebServer.Middleware
{
    internal static class ServiceProviderMiddlewareExtensions
    {
        public static bool IsAuthenticationMiddlewareEnabled(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(IAuthenticationService)) is IAuthenticationService;
        }

        public static bool IsCorsMiddlewareEnabled(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(CorsPolicy)) is CorsPolicy;
        }

        public static bool IsStaticFileMiddlewareEnabled(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(typeof(IContentTypeProvider)) is IContentTypeProvider && serviceProvider.GetService(typeof(IFileProvider)) is IFileProvider;
        }
    }
}
