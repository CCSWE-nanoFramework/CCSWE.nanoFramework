using System.Net;
using CCSWE.nanoFramework.WebServer.Http;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks
{
    internal class RequestDelegateMock
    {
        private readonly HttpStatusCode _statusCode;

        public RequestDelegateMock(HttpStatusCode statusCode = 0)
        {
            _statusCode = statusCode;
        }

        public bool Invoked { get; private set; }

        public void Invoke(HttpContext context)
        {
            if (!context.Response.HasStarted && _statusCode > 0)
            {
                context.Response.StatusCode(_statusCode);
            }

            Invoked = true;
        }
    }
}
