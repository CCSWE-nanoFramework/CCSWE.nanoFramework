using System;
using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Controllers;
using nanoFramework.TestFramework;

// ReSharper disable StringLiteralTypo
namespace CCSWE.nanoFramework.WebServer.UnitTests.Routing
{
    [TestClass]
    public class EndpointTests
    {
        [TestMethod]
        public void Constructor_Sets_HttpMethod()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);
            const string routeTemplate = "/api/ControllerMock/GET/";

            Assert.IsNotNull(methodInfo);
            Assert.IsTrue(HttpMethods.IsGet(new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true).HttpMethod));
        }

        [TestMethod]
        public void Constructor_Sets_ParameterIndexes()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);
            const string routeTemplate = "/api/ControllerMock/GET/";

            Assert.IsNotNull(methodInfo);

            var descriptor = new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true);

            Assert.AreEqual(0, descriptor.ParameterIndexes.Length);
        }

        [TestMethod]
        public void Constructor_Sets_ParameterIndexes_With_Parameters()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), [typeof(string)]);
            const string routeTemplate = "/api/ControllerMock/GET/{StringParameter}";

            Assert.IsNotNull(methodInfo);

            var descriptor = new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true);

            Assert.AreEqual(1, descriptor.ParameterIndexes.Length);
            Assert.AreEqual(3, descriptor.ParameterIndexes[0]);
        }

        [TestMethod]
        public void Constructor_Sets_RequireAuthentication()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);
            const string routeTemplate = "/api/ControllerMock/GET/";

            Assert.IsNotNull(methodInfo);
            Assert.IsFalse(new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, false).RequireAuthentication);
            Assert.IsTrue(new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true).RequireAuthentication);
        }

        [TestMethod]
        public void Constructor_Sets_RouteSegments()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);
            const string routeTemplate = "/api/ControllerMock/GET/";
            var routeSegments = new[] { "api", "controllermock", "get" };

            Assert.IsNotNull(methodInfo);

            var descriptor = new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true);

            Assert.AreEqual(routeSegments.Length, descriptor.RouteSegments.Length);

            for (var i = 0; i < routeSegments.Length; i++)
            {
                Assert.AreEqual(routeSegments[i], descriptor.RouteSegments[i]);
            }
        }

        [TestMethod]
        public void Constructor_Sets_RouteSegments_With_Parameters()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), [typeof(string)]);
            const string routeTemplate = "/api/ControllerMock/GET/{StringParameter}";
            var routeSegments = new[] { "api", "controllermock", "get", "{stringparameter}" };

            Assert.IsNotNull(methodInfo);

            var descriptor = new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true);

            Assert.AreEqual(routeSegments.Length, descriptor.RouteSegments.Length);

            for (var i = 0; i < routeSegments.Length; i++)
            {
                Assert.AreEqual(routeSegments[i], descriptor.RouteSegments[i]);
            }
        }

        [TestMethod]
        public void Constructor_Sets_RouteTemplate()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);
            const string routeTemplate = "/api/ControllerMock/GET/";

            Assert.IsNotNull(methodInfo);
            Assert.AreEqual(routeTemplate, new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, false).RouteTemplate);
        }

        [TestMethod]
        public void Constructor_Throws_If_HttpMethod_Is_Null_Or_Empty()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);

            Assert.IsNotNull(methodInfo);

            Assert.ThrowsException(typeof(ArgumentNullException), () =>
            {
                _ = new Endpoint(null!, methodInfo, "RouteTemple", true);
            });

            Assert.ThrowsException(typeof(ArgumentException), () =>
            {
                _ = new Endpoint(string.Empty, methodInfo, "RouteTemple", true);
            });

        }

        [TestMethod]
        public void Constructor_Throws_If_MethodInfo_Is_Null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () =>
            {
                _ = new Endpoint("get", null!, "RouteTemple", true);
            });
        }

        [TestMethod]
        public void Constructor_Throws_If_Template_Is_Null_Or_Empty()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);

            Assert.IsNotNull(methodInfo);

            Assert.ThrowsException(typeof(ArgumentNullException), () =>
            {
                _ = new Endpoint("get", methodInfo, null!, true);
            });

            Assert.ThrowsException(typeof(ArgumentException), () =>
            {
                _ = new Endpoint("get", methodInfo, string.Empty, true);
            });
        }
    }
}
