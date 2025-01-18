using System;
using System.Collections;
using CCSWE.nanoFramework.WebServer.Authentication;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Authentication;
using CCSWE.nanoFramework.WebServer.UnitTests.Mocks.Http;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Authentication
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        [TestMethod]
        public void Authenticate_returns_first_failure()
        {
            AuthenticationHandlerMocks.Reset();

            var descriptors = GetAuthenticationHandlerDescriptors(typeof(NoResultAuthenticationHandler), typeof(FailAuthenticationHandler), typeof(SuccessAuthenticationHandler), typeof(ExceptionAuthenticationHandler));

            var sut = new AuthenticationService(descriptors, new ServiceProviderMock());
            var result = sut.Authenticate(new HttpContextMock());

            Assert.IsFalse(result.Succeeded);

            Assert.IsTrue(NoResultAuthenticationHandler.AuthenticateCalled);
            Assert.IsTrue(FailAuthenticationHandler.AuthenticateCalled);

            Assert.IsFalse(SuccessAuthenticationHandler.AuthenticateCalled);
            Assert.IsFalse(ExceptionAuthenticationHandler.AuthenticateCalled);
        }

        [TestMethod]
        public void Authenticate_returns_first_success()
        {
            AuthenticationHandlerMocks.Reset();

            var descriptors = GetAuthenticationHandlerDescriptors(typeof(NoResultAuthenticationHandler), typeof(SuccessAuthenticationHandler), typeof(FailAuthenticationHandler), typeof(ExceptionAuthenticationHandler));

            var sut = new AuthenticationService(descriptors, new ServiceProviderMock());
            var result = sut.Authenticate(new HttpContextMock());

            Assert.IsTrue(result.Succeeded);

            Assert.IsTrue(NoResultAuthenticationHandler.AuthenticateCalled);
            Assert.IsTrue(SuccessAuthenticationHandler.AuthenticateCalled);

            Assert.IsFalse(FailAuthenticationHandler.AuthenticateCalled);
            Assert.IsFalse(ExceptionAuthenticationHandler.AuthenticateCalled);
        }

        [TestMethod]
        public void Authenticate_returns_none()
        {
            AuthenticationHandlerMocks.Reset();

            var descriptors = GetAuthenticationHandlerDescriptors(typeof(NoResultAuthenticationHandler));

            var sut = new AuthenticationService(descriptors, new ServiceProviderMock());
            var result = sut.Authenticate(new HttpContextMock());

            Assert.IsTrue(result.None);

            Assert.IsTrue(NoResultAuthenticationHandler.AuthenticateCalled);
        }

        [TestMethod]
        public void Authenticate_throws_exception()
        {
            AuthenticationHandlerMocks.Reset();

            var descriptors = GetAuthenticationHandlerDescriptors(typeof(ExceptionAuthenticationHandler));

            var sut = new AuthenticationService(descriptors, new ServiceProviderMock());

            Assert.ThrowsException(typeof(Exception), () => sut.Authenticate(new HttpContextMock()));

            Assert.IsTrue(ExceptionAuthenticationHandler.AuthenticateCalled);
        }

        [TestMethod]
        public void Authenticate_with_no_handlers()
        {
            AuthenticationHandlerMocks.Reset();

            var descriptors = GetAuthenticationHandlerDescriptors();

            var sut = new AuthenticationService(descriptors, new ServiceProviderMock());
            var result = sut.Authenticate(new HttpContextMock());

            Assert.IsTrue(result.None);
        }

        private AuthenticationHandlerDescriptor[] GetAuthenticationHandlerDescriptors(params Type[] implementationTypes)
        {
            var descriptors = new ArrayList();

            foreach (var implementationType in implementationTypes)
            {
                descriptors.Add(new AuthenticationHandlerDescriptor(implementationType));
            }

            return (AuthenticationHandlerDescriptor[])descriptors.ToArray(typeof(AuthenticationHandlerDescriptor));
        }
    }
}
