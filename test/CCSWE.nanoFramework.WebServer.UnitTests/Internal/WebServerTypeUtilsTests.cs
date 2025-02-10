using System;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Authorization;
using CCSWE.nanoFramework.WebServer.Cors;
using CCSWE.nanoFramework.WebServer.Diagnostics;
using CCSWE.nanoFramework.WebServer.Internal;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.StaticFiles;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Controllers;
using nanoFramework.TestFramework;

// ReSharper disable RedundantCast
namespace CCSWE.nanoFramework.WebServer.UnitTests.Internal
{
    [TestClass]
    public class WebServerTypeUtilsTests
    {
        [TestMethod]
        public void IsAllowAnonymousAttribute_returns_false()
        {
            Assert.IsFalse(WebServerTypeUtils.IsAllowAnonymousAttribute((object?)null));
            Assert.IsFalse(WebServerTypeUtils.IsAllowAnonymousAttribute(new object()));

            Assert.IsFalse(WebServerTypeUtils.IsAllowAnonymousAttribute(null));
            Assert.IsFalse(WebServerTypeUtils.IsAllowAnonymousAttribute(typeof(object)));
        }

        [TestMethod]
        public void IsAllowAnonymousAttribute_returns_true()
        {
            Assert.IsTrue(WebServerTypeUtils.IsAllowAnonymousAttribute(new AllowAnonymousAttribute()));
            Assert.IsTrue(WebServerTypeUtils.IsAllowAnonymousAttribute(typeof(AllowAnonymousAttribute)));
        }

        [TestMethod]
        public void IsAuthenticationHandler_returns_false()
        {
            Assert.IsFalse(WebServerTypeUtils.IsAuthenticationHandler(null));
            Assert.IsFalse(WebServerTypeUtils.IsAuthenticationHandler(typeof(object)));
        }

        [TestMethod]
        public void IsAuthenticationHandler_returns_true()
        {
            Assert.IsTrue(WebServerTypeUtils.IsAuthenticationHandler(typeof(SuccessAuthenticationHandler)));
        }

        [TestMethod]
        public void IsControllerBase_returns_false()
        {
            Assert.IsFalse(WebServerTypeUtils.IsControllerBase(null));
            Assert.IsFalse(WebServerTypeUtils.IsControllerBase(typeof(object)));
        }

        [TestMethod]
        public void IsControllerBase_returns_true()
        {
            Assert.IsTrue(WebServerTypeUtils.IsControllerBase(typeof(ControllerMock)));
        }

        [TestMethod]
        public void IsHttpMethodProvider_returns_false()
        {
            Assert.IsFalse(WebServerTypeUtils.IsHttpMethodProvider((object?)null));
            Assert.IsFalse(WebServerTypeUtils.IsHttpMethodProvider(new object()));

            Assert.IsFalse(WebServerTypeUtils.IsHttpMethodProvider(null));
            Assert.IsFalse(WebServerTypeUtils.IsHttpMethodProvider(typeof(object)));
        }

        [TestMethod]
        public void IsHttpMethodProvider_returns_true()
        {
            Assert.IsTrue(WebServerTypeUtils.IsHttpMethodProvider(new HttpDeleteAttribute()));
            Assert.IsTrue(WebServerTypeUtils.IsHttpMethodProvider(typeof(HttpDeleteAttribute)));
        }

        [TestMethod]
        public void IsMiddleware_returns_false()
        {
            Assert.IsFalse(WebServerTypeUtils.IsMiddleware(null));
            Assert.IsFalse(WebServerTypeUtils.IsMiddleware(typeof(object)));
        }

        [TestMethod]
        public void IsMiddleware_returns_true()
        {
            // TODO: Add all middleware here
            Assert.IsTrue(WebServerTypeUtils.IsMiddleware(typeof(AuthenticationMiddleware)));
            Assert.IsTrue(WebServerTypeUtils.IsMiddleware(typeof(CorsMiddleware)));
            Assert.IsTrue(WebServerTypeUtils.IsMiddleware(typeof(ExceptionHandlerMiddleware)));
            Assert.IsTrue(WebServerTypeUtils.IsMiddleware(typeof(RoutingMiddleware)));
            Assert.IsTrue(WebServerTypeUtils.IsMiddleware(typeof(StaticFileMiddleware)));
        }

        [TestMethod]
        public void IsRouteTemplateProvider_returns_false()
        {
            Assert.IsFalse(WebServerTypeUtils.IsRouteTemplateProvider((object?)null));
            Assert.IsFalse(WebServerTypeUtils.IsRouteTemplateProvider(new object()));

            Assert.IsFalse(WebServerTypeUtils.IsRouteTemplateProvider(null));
            Assert.IsFalse(WebServerTypeUtils.IsRouteTemplateProvider(typeof(object)));
        }

        [TestMethod]
        public void IsRouteTemplateProvider_returns_true()
        {
            Assert.IsTrue(WebServerTypeUtils.IsRouteTemplateProvider(new RouteAttribute("RouteTemplate")));
            Assert.IsTrue(WebServerTypeUtils.IsRouteTemplateProvider(typeof(RouteAttribute)));
        }

        [TestMethod]
        public void RequireAuthenticationHandler_does_not_throw()
        {
            WebServerTypeUtils.RequireAuthenticationHandler(typeof(SuccessAuthenticationHandler));
        }

        [TestMethod]
        public void RequireAuthenticationHandler_throws_argument_exception()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => WebServerTypeUtils.RequireAuthenticationHandler(null!));
            Assert.ThrowsException(typeof(ArgumentException), () => WebServerTypeUtils.RequireAuthenticationHandler(typeof(object)));
        }

        [TestMethod]
        public void RequireControllerBase_does_not_throw()
        {
            WebServerTypeUtils.RequireControllerBase(typeof(ControllerMock));
        }

        [TestMethod]
        public void RequireControllerBase_throws_argument_exception()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => WebServerTypeUtils.RequireControllerBase(null!));
            Assert.ThrowsException(typeof(ArgumentException), () => WebServerTypeUtils.RequireControllerBase(typeof(object)));
        }

        [TestMethod]
        public void RequireMiddleware_does_not_throw()
        {
            // TODO: Add all middleware here
            WebServerTypeUtils.RequireMiddleware(typeof(AuthenticationMiddleware));
            WebServerTypeUtils.RequireMiddleware(typeof(CorsMiddleware));
            WebServerTypeUtils.RequireMiddleware(typeof(ExceptionHandlerMiddleware));
            WebServerTypeUtils.RequireMiddleware(typeof(RoutingMiddleware));
            WebServerTypeUtils.RequireMiddleware(typeof(StaticFileMiddleware));
        }

        [TestMethod]
        public void RequireMiddleware_throws_argument_exception()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => WebServerTypeUtils.RequireMiddleware(null!));
            Assert.ThrowsException(typeof(ArgumentException), () => WebServerTypeUtils.RequireMiddleware(typeof(object)));
        }
    }
}
