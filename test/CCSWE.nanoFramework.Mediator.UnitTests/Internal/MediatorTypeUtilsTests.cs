using System;
using System.Collections;
using CCSWE.nanoFramework.Mediator.Internal;
using CCSWE.nanoFramework.Mediator.UnitTests.Mocks;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Mediator.UnitTests.Internal
{
    [TestClass]
    public class MediatorTypeUtilsTests
    {
        [TestMethod]
        public void IsMediatorEvent_should_return_false_for_invalid_type()
        {
            // Arrange
            var type = typeof(ArrayList);

            // Act
            var actual = MediatorTypeUtils.IsMediatorEvent(type);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsMediatorEvent_should_return_true_for_valid_type()
        {
            // Arrange
            var type = typeof(MediatorEventMock);

            // Act
            var actual = MediatorTypeUtils.IsMediatorEvent(type);

            // Assert
            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void RequireMediatorEvent_should_succeed_when_type_is_valid()
        {
            // Arrange
            var type = typeof(MediatorEventMock);

            // Act
            MediatorTypeUtils.RequireMediatorEvent(type);
        }

        [TestMethod]
        public void RequireMediatorEvent_should_throw_when_type_is_invalid()
        {
            // Arrange
            var type = typeof(ArrayList);

            // Act
            Assert.ThrowsException(typeof(ArgumentException), () => MediatorTypeUtils.RequireMediatorEvent(type));
        }
    }
}
