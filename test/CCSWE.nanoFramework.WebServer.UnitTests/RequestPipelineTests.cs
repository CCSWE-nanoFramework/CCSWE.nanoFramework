using CCSWE.nanoFramework.Logging;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using Microsoft.Extensions.DependencyInjection;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests
{
    [TestClass]
    public class RequestPipelineTests
    {
        [TestMethod]
        public void Build_creates_RequestDelegate()
        {
            var serviceProvider = new ServiceProviderMock();
            var requestPipeline = new RequestPipeline(serviceProvider);

            var requestDelegate = requestPipeline.Build();
            
            Assert.IsNotNull(requestDelegate);
        }

        [TestMethod]
        public void Execute_retrieve_RequestPipeline_from_HttpContext()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging();
            serviceCollection.AddWebServer();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var serviceScope = serviceProvider.CreateScope();

            var context = new HttpContextMock(serviceScope.ServiceProvider);

            RequestPipeline.Execute(context);
        }
    }
}
