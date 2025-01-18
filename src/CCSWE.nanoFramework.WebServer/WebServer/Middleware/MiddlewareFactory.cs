using System;
using CCSWE.nanoFramework.WebServer.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer.Middleware;

internal interface IMiddlewareFactory
{
    RequestDelegate CreateMiddleware(RequestDelegate next);
}

/// <summary>
/// This factory creates middleware instances from <see cref="HttpContext.RequestServices"/>.
/// </summary>
internal class MiddlewareFactory : IMiddlewareFactory
{
    internal readonly Type MiddlewareType;

    public MiddlewareFactory(Type middlewareType)
    {
        MiddlewareType = middlewareType;
    }

    public RequestDelegate CreateMiddleware(RequestDelegate next)
    {
        return context =>
        {
            var middleware = ActivatorUtilities.GetServiceOrCreateInstance(context.RequestServices, MiddlewareType) as IMiddleware;
            if (middleware is null)
            {
                // The factory returned null, it's a broken implementation
                throw new InvalidOperationException($"Failed to create {MiddlewareType}");
            }

            middleware.Invoke(context, next);
        };
    }
}