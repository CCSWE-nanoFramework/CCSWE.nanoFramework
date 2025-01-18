using CCSWE.nanoFramework.WebServer.Http.Headers;
using CCSWE.nanoFramework.WebServer.Middleware;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Middleware
{
    [TestClass]
    public class CorsMiddlewareTests
    {
        [TestMethod]
        public void Invoke_should_add_cors_headers_when_response_has_not_started()
        {
            var context = new HttpContextMock { ResponseHasStarted = false };
            var next = new RequestDelegateMock();

            var sut = new CorsMiddleware();

            sut.Invoke(context, next.Invoke);

            Assert.AreEqual(CorsConstants.AnyHeader, context.Response.Headers[HeaderNames.AccessControlAllowHeaders]);
            Assert.AreEqual(CorsConstants.AnyMethod, context.Response.Headers[HeaderNames.AccessControlAllowMethods]);
            Assert.AreEqual(CorsConstants.AnyOrigin, context.Response.Headers[HeaderNames.AccessControlAllowOrigin]);

            Assert.IsTrue(next.Invoked);
        }

        [TestMethod]
        public void Invoke_should_not_add_cors_headers_when_response_has_started()
        {
            var context = new HttpContextMock { ResponseHasStarted = true };
            var next = new RequestDelegateMock();

            var sut = new CorsMiddleware();

            sut.Invoke(context, next.Invoke);

            Assert.IsNull(context.Response.Headers[HeaderNames.AccessControlAllowHeaders]);
            Assert.IsNull(context.Response.Headers[HeaderNames.AccessControlAllowMethods]);
            Assert.IsNull(context.Response.Headers[HeaderNames.AccessControlAllowOrigin]);

            Assert.IsTrue(next.Invoked);
        }
    }
}