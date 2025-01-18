using CCSWE.nanoFramework.WebServer.Http;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Controllers;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using nanoFramework.TestFramework;

// ReSharper disable StringLiteralTypo
namespace CCSWE.nanoFramework.WebServer.UnitTests.Routing
{
    [TestClass]
    public class EndpointHandlerTests
    {
        [TestMethod]
        public void HandleRequest_Invokes_Method()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), Types.EmptyTypes);
            const string routeTemplate = "/api/ControllerMock/GET/";

            Assert.IsNotNull(methodInfo);

            var descriptor = new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true);
            var parameters = new object[0];

            var sut = new EndpointHandler(descriptor, parameters);

            Assert.IsTrue(sut.RequireAuthentication);

            sut.Invoke(new HttpContextMock(HttpMethods.Get, routeTemplate));

            Assert.IsTrue(HttpMethods.IsGet(ControllerMock.HttpMethod));
            Assert.IsNull(ControllerMock.StringParameter);
        }

        [TestMethod]
        public void HandleRequest_Invokes_Method_With_Parameters()
        {
            var methodInfo = typeof(ControllerMock).GetMethod(nameof(ControllerMock.HandleGet), [typeof(string)]);
            const string routeTemplate = "/api/ControllerMock/GET/{StringParameter}";
            const string stringParameter = "A string parameter";

            Assert.IsNotNull(methodInfo);

            var descriptor = new Endpoint(HttpMethods.Get, methodInfo, routeTemplate, true);
            var parameters = new object[] { stringParameter };

            var sut = new EndpointHandler(descriptor, parameters);

            Assert.IsTrue(sut.RequireAuthentication);

            sut.Invoke(new HttpContextMock(HttpMethods.Get, routeTemplate));

            Assert.IsTrue(HttpMethods.IsGet(ControllerMock.HttpMethod));
            Assert.AreEqual(stringParameter, ControllerMock.StringParameter);
        }
    }
}
