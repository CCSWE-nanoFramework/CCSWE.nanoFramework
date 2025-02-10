using System;
using CCSWE.nanoFramework.Mediator.UnitTests.Mocks;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Mediator.UnitTests
{
    [TestClass]
    public class AsyncMediatorOptionsTests
    {
        [TestMethod]
        public void AddSubscription_should_add_subscriber()
        {
            var sut = new AsyncMediatorOptions();

            sut.AddSubscriber(typeof(MediatorEventMock), typeof(MediatorEventHandlerMock));

            Assert.AreEqual(1, sut.Subscribers.Count);
        }

        [TestMethod]
        public void AddSubscription_should_throw_exception_for_invalid_event_type()
        {
            var sut = new AsyncMediatorOptions();

            Assert.ThrowsException(typeof(ArgumentException), () => sut.AddSubscriber(typeof(object), typeof(MediatorEventHandlerMock)));
            Assert.AreEqual(0, sut.Subscribers.Count);
        }

        [TestMethod]
        public void AddSubscription_should_throw_exception_for_null_event_type()
        {
            var sut = new AsyncMediatorOptions();

            Assert.ThrowsException(typeof(ArgumentNullException), () => sut.AddSubscriber(null!, typeof(MediatorEventHandlerMock)));
            Assert.AreEqual(0, sut.Subscribers.Count);
        }

        [TestMethod]
        public void AddSubscription_should_throw_exception_for_null_subscriber_type()
        {
            var sut = new AsyncMediatorOptions();

            Assert.ThrowsException(typeof(ArgumentNullException), () => sut.AddSubscriber(typeof(MediatorEventMock), null!));
            Assert.AreEqual(0, sut.Subscribers.Count);
        }
    }
}
