using nanoFramework.TestFramework;
using System;
using System.Collections;
using System.Threading;
using CCSWE.nanoFramework.Mediator.UnitTests.Mocks;
using CCSWE.nanoFramework.Threading.TestFramework;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable AccessToDisposedClosure
namespace CCSWE.nanoFramework.Mediator.UnitTests
{
    [TestClass]
    public class AsyncMediatorTests
    {
        public static TimeSpan PublishDelay = TimeSpan.FromMilliseconds(500);

        [TestMethod]
        public void Publish_should_throw_exception_for_null_event()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceProviderMock());

                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Publish(null!));
            });
        }

        [TestMethod]
        public void Subscribe_should_add_singleton_subscriber()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var mediatorEvent = new MediatorEventMock();
                var mediatorSubscriber = new MediatorEventHandlerMock();
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddSingleton(typeof(IMediatorEventHandlerMock), mediatorSubscriber);

                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), serviceCollection.BuildServiceProvider());

                sut.Subscribe(typeof(MediatorEventMock), typeof(IMediatorEventHandlerMock));
                sut.Publish(mediatorEvent);

                WaitForPublisherThread();

                Assert.AreEqual(1, mediatorSubscriber.EventsReceived);
                Assert.AreEqual(mediatorEvent, mediatorSubscriber.LastEvent);
            });
        }

        [TestMethod]
        public void Subscribe_should_add_transient_subscriber()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var mediatorEvent = new MediatorEventMock();
                var mediatorSubscriber = new MediatorEventHandlerMock();

                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceCollection().BuildServiceProvider());

                sut.Subscribe(typeof(MediatorEventMock), mediatorSubscriber);
                sut.Publish(mediatorEvent);

                WaitForPublisherThread();

                Assert.AreEqual(1, mediatorSubscriber.EventsReceived);
                Assert.AreEqual(mediatorEvent, mediatorSubscriber.LastEvent);
            });
        }

        [TestMethod]
        public void Subscribe_should_throw_exception_for_invalid_event()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceProviderMock());

                Assert.ThrowsException(typeof(ArgumentException), () => sut.Subscribe(typeof(ArrayList), typeof(MediatorEventHandlerMock)));
                Assert.ThrowsException(typeof(ArgumentException), () => sut.Subscribe(typeof(ArrayList), new MediatorEventHandlerMock()));
            });
        }

        [TestMethod]
        public void Subscribe_should_throw_exception_for_null_event()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceProviderMock());

                Assert.ThrowsException(typeof(ArgumentNullException),() => sut.Subscribe(null!, typeof(MediatorEventHandlerMock)));
                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Subscribe(null!, new MediatorEventHandlerMock()));
            });
        }

        [TestMethod]
        public void Subscribe_should_throw_exception_for_null_subscriber()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceProviderMock());

                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Subscribe(typeof(MediatorEventMock), (Type)null!));
                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Subscribe(typeof(MediatorEventMock), (IMediatorEventHandler)null!));
            });
        }

        [TestMethod]
        public void Unsubscribe_should_remove_singleton_subscriber()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var mediatorEvent = new MediatorEventMock();
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddSingleton(typeof(IMediatorEventHandlerMock), typeof(MediatorEventHandlerMock));
                var serviceProvider = serviceCollection.BuildServiceProvider();
                var mediatorSubscriber = (MediatorEventHandlerMock)serviceProvider.GetRequiredService(typeof(IMediatorEventHandlerMock));

                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), serviceCollection.BuildServiceProvider());

                sut.Subscribe(typeof(MediatorEventMock), typeof(IMediatorEventHandlerMock));
                sut.Unsubscribe(typeof(MediatorEventMock), typeof(IMediatorEventHandlerMock));
                sut.Publish(mediatorEvent);

                WaitForPublisherThread();

                Assert.AreEqual(0, mediatorSubscriber.EventsReceived);
                Assert.IsNull(mediatorSubscriber.LastEvent);
            });
        }

        [TestMethod]
        public void Unsubscribe_should_remove_transient_subscriber()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var mediatorEvent = new MediatorEventMock();
                var mediatorSubscriber = new MediatorEventHandlerMock();

                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceCollection().BuildServiceProvider());

                sut.Subscribe(typeof(MediatorEventMock), mediatorSubscriber);
                sut.Unsubscribe(typeof(MediatorEventMock), mediatorSubscriber);
                sut.Publish(mediatorEvent);

                WaitForPublisherThread();

                Assert.AreEqual(0, mediatorSubscriber.EventsReceived);
                Assert.IsNull(mediatorSubscriber.LastEvent);
            });
        }

        [TestMethod]
        public void Unsubscribe_should_throw_exception_for_null_event()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceProviderMock());

                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Unsubscribe(null!, typeof(MediatorEventHandlerMock)));
                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Unsubscribe(null!, new MediatorEventHandlerMock()));
            });
        }

        [TestMethod]
        public void Unsubscribe_should_throw_exception_for_null_subscriber()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                using var sut = new AsyncMediator(new AsyncMediatorOptions(), new LoggerMock(), new ServiceProviderMock());

                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Unsubscribe(typeof(MediatorEventMock), (Type)null!));
                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Unsubscribe(typeof(MediatorEventMock), (IMediatorEventHandler)null!));
            });
        }

        private static void WaitForPublisherThread()
        {
            Thread.Sleep(PublishDelay);
        }
    }
}
