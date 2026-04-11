using System;
using CCSWE.nanoFramework.WebServer;
using nanoFramework.TestFramework;

namespace UnitTests.CCSWE.nanoFramework.WebServer;
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
