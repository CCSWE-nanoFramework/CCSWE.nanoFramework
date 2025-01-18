using System;
using System.Net;
using CCSWE.nanoFramework.WebServer.Diagnostics;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using Microsoft.Extensions.Logging;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Diagnostics
{
    [TestClass]
    public class ExceptionHandlerMiddlewareTests
    {
        [TestMethod]
        public void Invoke_exception_thrown()
        {
            var logger = new LoggerMock();
            var context = new HttpContextMock();

            var sut = new ExceptionHandlerMiddleware(logger);

            sut.Invoke(context, (ctx) => throw new InvalidOperationException("Test exception"));

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            Assert.AreEqual("Test exception", context.Response.StatusDescription);
            Assert.IsNotNull(logger.LastLoggedException);
            Assert.AreEqual("Test exception", logger.LastLoggedException.Message);
            Assert.AreEqual("Unhandled exception while processing request.", logger.LastLoggedMessage);
            Assert.AreEqual(LogLevel.Error, logger.LastLoggedLogLevel);
        }

        [TestMethod]
        public void Invoke_exception_thrown_response_already_started()
        {
            var logger = new LoggerMock();
            var context = new HttpContextMock { ResponseHasStarted = true };

            var sut = new ExceptionHandlerMiddleware(logger);

            sut.Invoke(context, (ctx) =>
            {
                ctx.Response.StatusCode = 420;
                throw new InvalidOperationException("Test exception");
            });

            Assert.AreEqual(420, context.Response.StatusCode); // Status code should not be changed
            Assert.IsNotNull(logger.LastLoggedException);
            Assert.AreEqual("Test exception", logger.LastLoggedException.Message);
            Assert.AreEqual("Unhandled exception while processing request.", logger.LastLoggedMessage);
            Assert.AreEqual(LogLevel.Error, logger.LastLoggedLogLevel);
        }

        [TestMethod]
        public void Invoke_no_exception()
        {
            var logger = new LoggerMock();
            var context = new HttpContextMock();

            var sut = new ExceptionHandlerMiddleware(logger);

            sut.Invoke(context, ctx => { ctx.Response.StatusCode = 420; });

            Assert.AreEqual(420, context.Response.StatusCode);
            Assert.IsNull(logger.LastLoggedException);
        }
    }
}




