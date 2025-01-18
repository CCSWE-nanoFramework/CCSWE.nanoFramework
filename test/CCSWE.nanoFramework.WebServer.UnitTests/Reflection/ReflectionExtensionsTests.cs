using CCSWE.nanoFramework.WebServer.Reflection;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Reflection
{
    [TestClass]
    public class ReflectionExtensionsTests
    {
        [TestMethod]
        public void IsImplementationOf_Returns_False_If_Type_Does_Not_Implement_Interface()
        {
            Assert.IsFalse(typeof(AbstractClassDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsFalse(typeof(ClassDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsFalse(typeof(DerivedClassDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsFalse(typeof(IInterfaceDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
        }

        [TestMethod]
        public void IsImplementationOf_Returns_True_If_Type_Implements_Interface()
        {
            Assert.IsTrue(typeof(AbstractClassImplementsInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsTrue(typeof(ClassImplementsInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsTrue(typeof(DerivedClassImplementsInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsTrue(typeof(IInterfaceImplementsInterface).IsImplementationOf(typeof(ITestInterface)));
        }


        private interface ITestInterface;

        private abstract class AbstractClassDoesNotImplementInterface;
        private class ClassDoesNotImplementInterface;
        private class DerivedClassDoesNotImplementInterface: AbstractClassDoesNotImplementInterface;
        private interface IInterfaceDoesNotImplementInterface;

        private abstract class AbstractClassImplementsInterface : ITestInterface;
        private class ClassImplementsInterface : ITestInterface;
        private class DerivedClassImplementsInterface : AbstractClassImplementsInterface;
        private interface IInterfaceImplementsInterface : ITestInterface;
    }
}
