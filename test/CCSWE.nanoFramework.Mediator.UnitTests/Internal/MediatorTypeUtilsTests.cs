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
            var type = typeof(ArrayList);

            var actual = MediatorTypeUtils.IsMediatorEvent(type);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsMediatorEvent_should_return_true_for_valid_type()
        {
            var type = typeof(MediatorEventMock);

            var actual = MediatorTypeUtils.IsMediatorEvent(type);

            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void RequireMediatorEvent_should_succeed_when_type_is_valid()
        {
            var type = typeof(MediatorEventMock);

            MediatorTypeUtils.RequireMediatorEvent(type);
        }

        [TestMethod]
        public void RequireMediatorEvent_should_throw_when_type_is_invalid()
        {
            var type = typeof(ArrayList);

            Assert.ThrowsException(typeof(ArgumentException), () => MediatorTypeUtils.RequireMediatorEvent(type));
        }
    }
}
