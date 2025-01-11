using System;
using System.Collections;
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
            // Arrange
            var sut = new AsyncMediatorOptions();

            // Act
            sut.AddSubscriber(typeof(MediatorEventMock), typeof(IMediatorEventHandlerMock));

            // Assert
            Assert.AreEqual(1, sut.Subscribers.Count);
        }

        [TestMethod]
        public void AddSubscription_should_throw_exception_for_invalid_event_type()
        {
            // Arrange
            var sut = new AsyncMediatorOptions();

            // Act
            Assert.ThrowsException(typeof(ArgumentException), () => sut.AddSubscriber(typeof(ArrayList), typeof(MediatorEventHandlerMock)));

            // Assert
            Assert.AreEqual(0, sut.Subscribers.Count);
        }

        [TestMethod]
        public void AddSubscription_should_throw_exception_for_null_event_type()
        {
            // Arrange
            var sut = new AsyncMediatorOptions();

            // Act
            Assert.ThrowsException(typeof(ArgumentNullException), () => sut.AddSubscriber(null!, typeof(MediatorEventHandlerMock)));

            // Assert
            Assert.AreEqual(0, sut.Subscribers.Count);
        }

        [TestMethod]
        public void AddSubscription_should_throw_exception_for_null_subscriber_type()
        {
            // Arrange
            var sut = new AsyncMediatorOptions();

            // Act
            Assert.ThrowsException(typeof(ArgumentNullException), () => sut.AddSubscriber(typeof(ArrayList), null!));

            // Assert
            Assert.AreEqual(0, sut.Subscribers.Count);
        }
    }
}
