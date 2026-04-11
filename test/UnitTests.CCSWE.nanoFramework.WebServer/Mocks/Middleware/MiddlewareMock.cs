using System;
using CCSWE.nanoFramework.WebServer;
using CCSWE.nanoFramework.WebServer.Http;

namespace UnitTests.CCSWE.nanoFramework.WebServer.Mocks.Middleware;
internal class MiddlewareMock: IMiddleware
{
    public void Invoke(HttpContext context, RequestDelegate next)
    {
        throw new NotImplementedException();
    }
}
