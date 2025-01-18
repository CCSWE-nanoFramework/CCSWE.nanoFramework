using CCSWE.nanoFramework.WebServer.Authentication;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Authentication
{
    [TestClass]
    public class AuthenticateResultTests
    {
        [TestMethod]
        public void Fail_with_message()
        {
            var result = AuthenticateResult.Fail("Custom failure message");

            Assert.IsFalse(result.Succeeded);
            Assert.IsFalse(result.None);
            Assert.AreEqual("Custom failure message", result.Failure);
        }

        [TestMethod]
        public void Fail_without_message()
        {
            var result = AuthenticateResult.Fail(null);

            Assert.IsFalse(result.Succeeded);
            Assert.IsFalse(result.None);
            Assert.AreEqual("Authentication failed.", result.Failure);
        }

        [TestMethod]
        public void NoResult_returns_no_result()
        {
            var result = AuthenticateResult.NoResult();

            Assert.IsFalse(result.Succeeded);
            Assert.IsTrue(result.None);
            Assert.IsNull(result.Failure);
        }

        [TestMethod]
        public void Success_returns_success()
        {
            var result = AuthenticateResult.Success();

            Assert.IsTrue(result.Succeeded);
            Assert.IsFalse(result.None);
            Assert.IsNull(result.Failure);
        }
    }
}