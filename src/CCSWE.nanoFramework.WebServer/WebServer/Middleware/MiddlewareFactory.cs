using System;
using CCSWE.nanoFramework.WebServer.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CCSWE.nanoFramework.WebServer.Middleware;

internal interface IMiddlewareFactory
{
    Type ImplementationType { get; }

    RequestDelegate CreateMiddleware(RequestDelegate next);
}

/// <summary>
/// This factory creates middleware instances from <see cref="HttpContext.RequestServices"/>.
/// </summary>
internal class MiddlewareFactory : IMiddlewareFactory
{
    public MiddlewareFactory(Type implementationType)
    {
        ImplementationType = implementationType;
    }

    public Type ImplementationType { get; }

    public RequestDelegate CreateMiddleware(RequestDelegate next)
    {
        return context =>
        {
            var middleware = ActivatorUtilities.GetServiceOrCreateInstance(context.RequestServices, ImplementationType) as IMiddleware;
            if (middleware is null)
            {
                // The factory returned null, it's a broken implementation
                throw new InvalidOperationException($"Failed to create {ImplementationType}");
            }

            middleware.Invoke(context, next);
        };
    }
}