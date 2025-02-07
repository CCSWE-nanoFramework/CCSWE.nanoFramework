using System;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Core.UnitTests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void IsImplementationOf_returns_false_if_type_does_not_implement_interface()
        {
            Assert.IsFalse(typeof(AbstractClassDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsFalse(typeof(ClassDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsFalse(typeof(DerivedClassDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
            Assert.IsFalse(typeof(IInterfaceDoesNotImplementInterface).IsImplementationOf(typeof(ITestInterface)));
        }

        [TestMethod]
        public void IsImplementationOf_returns_true_if_type_implements_interface()
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
