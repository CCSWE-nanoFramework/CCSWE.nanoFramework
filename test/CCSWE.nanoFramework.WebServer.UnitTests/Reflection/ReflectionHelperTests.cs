using System;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Authorization;
using CCSWE.nanoFramework.WebServer.Diagnostics;
using CCSWE.nanoFramework.WebServer.Middleware;
using CCSWE.nanoFramework.WebServer.Reflection;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Controllers;
using nanoFramework.TestFramework;

// ReSharper disable RedundantCast
namespace CCSWE.nanoFramework.WebServer.UnitTests.Reflection
{
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void IsAllowAnonymousAttribute_returns_false()
        {
            Assert.IsFalse(ReflectionHelper.IsAllowAnonymousAttribute((object?) null));
            Assert.IsFalse(ReflectionHelper.IsAllowAnonymousAttribute(new object()));

            Assert.IsFalse(ReflectionHelper.IsAllowAnonymousAttribute((Type?) null));
            Assert.IsFalse(ReflectionHelper.IsAllowAnonymousAttribute(typeof(object)));
        }

        [TestMethod]
        public void IsAllowAnonymousAttribute_returns_true()
        {
            Assert.IsTrue(ReflectionHelper.IsAllowAnonymousAttribute(new AllowAnonymousAttribute()));
            Assert.IsTrue(ReflectionHelper.IsAllowAnonymousAttribute(typeof(AllowAnonymousAttribute)));
        }

        [TestMethod]
        public void IsAuthenticationHandler_returns_false()
        {
            Assert.IsFalse(ReflectionHelper.IsAuthenticationHandler((Type?)null));
            Assert.IsFalse(ReflectionHelper.IsAuthenticationHandler(typeof(object)));
        }

        [TestMethod]
        public void IsAuthenticationHandler_returns_true()
        {
            Assert.IsTrue(ReflectionHelper.IsAuthenticationHandler(typeof(AuthenticationHandler)));
            Assert.IsTrue(ReflectionHelper.IsAuthenticationHandler(typeof(SuccessAuthenticationHandler)));
        }

        [TestMethod]
        public void IsControllerBase_returns_false()
        {
            Assert.IsFalse(ReflectionHelper.IsControllerBase((Type?)null));
            Assert.IsFalse(ReflectionHelper.IsControllerBase(typeof(object)));
        }

        [TestMethod]
        public void IsControllerBase_returns_true()
        {
            Assert.IsTrue(ReflectionHelper.IsControllerBase(typeof(ControllerBase)));
            Assert.IsTrue(ReflectionHelper.IsControllerBase(typeof(ControllerMock)));
        }

        [TestMethod]
        public void IsHttpMethodProvider_returns_false()
        {
            Assert.IsFalse(ReflectionHelper.IsHttpMethodProvider((object?)null));
            Assert.IsFalse(ReflectionHelper.IsHttpMethodProvider(new object()));

            Assert.IsFalse(ReflectionHelper.IsHttpMethodProvider((Type?)null));
            Assert.IsFalse(ReflectionHelper.IsHttpMethodProvider(typeof(object)));
        }

        [TestMethod]
        public void IsHttpMethodProvider_returns_true()
        {
            Assert.IsTrue(ReflectionHelper.IsHttpMethodProvider(new HttpDeleteAttribute()));
            Assert.IsTrue(ReflectionHelper.IsHttpMethodProvider(typeof(HttpDeleteAttribute)));
        }

        [TestMethod]
        public void IsMiddleware_returns_false()
        {
            Assert.IsFalse(ReflectionHelper.IsMiddleware((Type?)null));
            Assert.IsFalse(ReflectionHelper.IsMiddleware(typeof(object)));
        }

        [TestMethod]
        public void IsMiddleware_returns_true()
        {
            // TODO: Add all middleware here
            Assert.IsTrue(ReflectionHelper.IsMiddleware(typeof(AuthenticationMiddleware)));
            Assert.IsTrue(ReflectionHelper.IsMiddleware(typeof(CorsMiddleware)));
            Assert.IsTrue(ReflectionHelper.IsMiddleware(typeof(ExceptionHandlerMiddleware)));
            Assert.IsTrue(ReflectionHelper.IsMiddleware(typeof(RoutingMiddleware)));
        }

        [TestMethod]
        public void IsRouteTemplateProvider_returns_false()
        {
            Assert.IsFalse(ReflectionHelper.IsRouteTemplateProvider((object?)null));
            Assert.IsFalse(ReflectionHelper.IsRouteTemplateProvider(new object()));

            Assert.IsFalse(ReflectionHelper.IsRouteTemplateProvider((Type?)null));
            Assert.IsFalse(ReflectionHelper.IsRouteTemplateProvider(typeof(object)));
        }

        [TestMethod]
        public void IsRouteTemplateProvider_returns_true()
        {
            Assert.IsTrue(ReflectionHelper.IsRouteTemplateProvider(new RouteAttribute("RouteTemplate")));
            Assert.IsTrue(ReflectionHelper.IsRouteTemplateProvider(typeof(RouteAttribute)));
        }
    }
}
