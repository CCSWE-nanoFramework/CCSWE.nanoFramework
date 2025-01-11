using System;
using System.Threading;
using CCSWE.nanoFramework.Threading.TestFramework;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Threading.UnitTests
{
    [TestClass]
    public class ConsumerThreadPoolTests
    {
        [TestMethod]
        public void Enqueue_should_throw_ArgumentNullException_if_item_is_null()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var completed = false;
                using var sut = new ConsumerThreadPool(1, _ => { completed = true; });

                Assert.ThrowsException(typeof(ArgumentNullException), () => sut.Enqueue(null!));
                Thread.Sleep(0);

                Assert.IsFalse(completed);
            });
        }

        [TestMethod]
        public void Enqueue_should_throw_ObjectDisposedException_if_disposed()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var sut = new ConsumerThreadPool(1, _ => { } );
                sut.Dispose();

                Assert.ThrowsException(typeof(ObjectDisposedException), () => sut.Enqueue(new object()));
            });
        }

        [TestMethod]
        public void It_should_execute_callback_for_all_items()
        {
            ThreadPoolTestHelper.ExecuteAndReset(() =>
            {
                var completedEvent = new ManualResetEvent(false);
                var expected = 16;
                var processed = 0;

                using var sut = new ConsumerThreadPool(4, item =>
                {
                    var current = Interlocked.Increment(ref processed);

                    if (current == expected)
                    {
                        completedEvent.Set();
                    }
                });

                for (var i = 0; i < expected; i++)
                {
                    sut.Enqueue(i);
                }

                var completed = completedEvent.WaitOne(10_000, false);

                Assert.AreEqual(expected, processed);
                Assert.IsTrue(completed, "Completed");
            });
        }
    }
}
