using CCSWE.nanoFramework.WebServer.Cors;
using CCSWE.nanoFramework.WebServer.Http.Headers;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Cors
{
    [TestClass]
    public class CorsMiddlewareTests
    {
        [TestMethod]
        public void Invoke_should_add_cors_headers_when_response_has_not_started()
        {
            var context = new HttpContextMock { ResponseHasStarted = false };
            var next = new RequestDelegateMock();
            var policy = CorsPolicy.AllowAny;

            var sut = new CorsMiddleware(policy);

            sut.Invoke(context, next.Invoke);

            Assert.AreEqual(policy.AccessControlAllowHeaders, context.Response.Headers[HeaderNames.AccessControlAllowHeaders]);
            Assert.AreEqual(policy.AccessControlAllowMethods, context.Response.Headers[HeaderNames.AccessControlAllowMethods]);
            Assert.AreEqual(policy.AccessControlAllowOrigin, context.Response.Headers[HeaderNames.AccessControlAllowOrigin]);

            Assert.IsTrue(next.Invoked);
        }

        [TestMethod]
        public void Invoke_should_not_add_cors_headers_when_response_has_started()
        {
            var context = new HttpContextMock { ResponseHasStarted = true };
            var next = new RequestDelegateMock();
            var policy = CorsPolicy.AllowAny;

            var sut = new CorsMiddleware(policy);

            sut.Invoke(context, next.Invoke);

            Assert.IsNull(context.Response.Headers[HeaderNames.AccessControlAllowHeaders]);
            Assert.IsNull(context.Response.Headers[HeaderNames.AccessControlAllowMethods]);
            Assert.IsNull(context.Response.Headers[HeaderNames.AccessControlAllowOrigin]);

            Assert.IsTrue(next.Invoked);
        }
    }
}