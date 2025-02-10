using System;
using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.Cors;
using CCSWE.nanoFramework.WebServer.Middleware;
using CCSWE.nanoFramework.WebServer.Routing;
using CCSWE.nanoFramework.WebServer.StaticFiles;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Controllers;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Middleware;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void AddAuthentication_registers_AuthenticationHandlerDescriptor()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddAuthentication(typeof(SuccessAuthenticationHandler));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var descriptors = (AuthenticationHandlerDescriptor[]) serviceProvider.GetServices(typeof(AuthenticationHandlerDescriptor)).ToArray(typeof(AuthenticationHandlerDescriptor));

            Assert.AreEqual(1, descriptors.Length);
            Assert.AreEqual(typeof(SuccessAuthenticationHandler), descriptors[0].ImplementationType);
        }

        [TestMethod]
        public void AddAuthentication_registers_AuthenticationService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddAuthentication(typeof(SuccessAuthenticationHandler));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var authenticationService = serviceProvider.GetService(typeof(IAuthenticationService)) as IAuthenticationService;

            Assert.IsInstanceOfType(authenticationService, typeof(AuthenticationService));
        }

        [TestMethod]
        public void AddAuthentication_throws_when_implementationType_does_not_implement_IAuthenticationHandler()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => new ServiceCollection().AddAuthentication(typeof(object)));
        }

        [TestMethod]
        public void AddAuthentication_throws_when_implementationType_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new ServiceCollection().AddAuthentication(null!));
        }

        [TestMethod]
        public void AddController_registers_ControllerDescriptor()
        {
            var serviceCollection = new ServiceCollection();

            // Confirms that the controller is only registered a single time
            serviceCollection.AddController(typeof(ControllerMock));
            serviceCollection.AddController(typeof(ControllerMock));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var descriptors = (ControllerDescriptor[])serviceProvider.GetServices(typeof(ControllerDescriptor)).ToArray(typeof(ControllerDescriptor));

            Assert.AreEqual(1, descriptors.Length);
            Assert.AreEqual(typeof(ControllerMock), descriptors[0].ImplementationType);
        }

        [TestMethod]
        public void AddController_throws_when_implementationType_does_not_implement_ControllerBase()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => new ServiceCollection().AddController(typeof(object)));
        }

        [TestMethod]
        public void AddController_throws_when_implementationType_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new ServiceCollection().AddController(null!));
        }

        [TestMethod]
        public void AddControllers_registers_ControllerDescriptors()
        {
            var serviceCollection = new ServiceCollection();

            // Confirms that the controller is only registered a single time
            serviceCollection.AddControllers(typeof(BootstrapperTests).Assembly);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var descriptors = (ControllerDescriptor[])serviceProvider.GetServices(typeof(ControllerDescriptor)).ToArray(typeof(ControllerDescriptor));

            Assert.AreEqual(1, descriptors.Length);
            Assert.AreEqual(typeof(ControllerMock), descriptors[0].ImplementationType);
        }

        [TestMethod]
        public void AddCors_registers_CorsPolicy()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddCors();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var corsPolicy = serviceProvider.GetService(typeof(CorsPolicy));

            Assert.AreSame(CorsPolicy.AllowAny, corsPolicy);
        }

        [TestMethod]
        public void AddMiddleware_registers_MiddlewareFactory()
        {
            var serviceCollection = new ServiceCollection();

            // Confirms that the middleware is only registered a single time
            serviceCollection.AddMiddleware(typeof(MiddlewareMock));
            serviceCollection.AddMiddleware(typeof(MiddlewareMock));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var factories = (IMiddlewareFactory[])serviceProvider.GetServices(typeof(IMiddlewareFactory)).ToArray(typeof(IMiddlewareFactory));

            Assert.AreEqual(1, factories.Length);
            Assert.AreEqual(typeof(MiddlewareMock), factories[0].ImplementationType);
        }

        [TestMethod]
        public void AddMiddleware_throws_when_implementationType_does_not_implement_MiddlewareBase()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => new ServiceCollection().AddMiddleware(typeof(object)));
        }

        [TestMethod]
        public void AddMiddleware_throws_when_implementationType_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new ServiceCollection().AddMiddleware(null!));
        }

        [TestMethod]
        public void AddStaticFiles_registers_ContentTypeProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddStaticFiles(typeof(FileProviderMock));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var contentTypeProvider = serviceProvider.GetService(typeof(IContentTypeProvider));
            var fileProvider = serviceProvider.GetService(typeof(IFileProvider));

            Assert.IsInstanceOfType(contentTypeProvider, typeof(ContentTypeProvider));
            Assert.IsInstanceOfType(fileProvider, typeof(FileProviderMock));
        }

        [TestMethod]
        public void AddStaticFiles_registers_FileProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddStaticFiles(typeof(FileProviderMock), typeof(ContentTypeProviderMock));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var contentTypeProvider = serviceProvider.GetService(typeof(IContentTypeProvider));
            var fileProvider = serviceProvider.GetService(typeof(IFileProvider));

            Assert.IsInstanceOfType(contentTypeProvider, typeof(ContentTypeProviderMock));
            Assert.IsInstanceOfType(fileProvider, typeof(FileProviderMock));
        }

        [TestMethod]
        public void AddWebServer_configures_WebServerOptions()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddWebServer(options => options.Port = 420);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var webServerOptions = serviceProvider.GetService(typeof(WebServerOptions)) as WebServerOptions;

            Assert.IsNotNull(webServerOptions);
            Assert.AreEqual((ushort) 420, webServerOptions.Port);
        }

        [TestMethod]
        public void AddWebServer_registers_required_services()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddWebServer();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            Assert.IsInstanceOfType(serviceProvider.GetService(typeof(IEndpointProvider)), typeof(EndpointProvider));
            Assert.IsInstanceOfType(serviceProvider.GetService(typeof(WebServerOptions)), typeof(WebServerOptions));

            using var serviceProviderScope = serviceProvider.CreateScope();

            Assert.IsInstanceOfType(serviceProviderScope.ServiceProvider.GetService(typeof(IRequestPipeline)), typeof(RequestPipeline));

            using var webServer = serviceProvider.GetService(typeof(IWebServer)) as WebServer;

            Assert.IsNotNull(webServer);
        }
    }
}
