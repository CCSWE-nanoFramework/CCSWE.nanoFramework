using System;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Middleware
{
    internal class MiddlewareMock: IMiddleware
    {
        public void Invoke(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
