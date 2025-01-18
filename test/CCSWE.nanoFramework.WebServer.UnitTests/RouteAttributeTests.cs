using System;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests
{
    [TestClass]
    public class RouteAttributeTests
    {
        [TestMethod]
        public void Constructor_Throws_Exception_When_Template_Is_Null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () =>
            {
                var attribute = new RouteAttribute(null!);
            });
        }
    }
}
