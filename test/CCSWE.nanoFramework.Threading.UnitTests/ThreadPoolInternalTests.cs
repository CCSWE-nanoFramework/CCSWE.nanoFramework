using System;
using nanoFramework.TestFramework;
using System.Threading;
using CCSWE.nanoFramework.Threading.Internal;

namespace CCSWE.nanoFramework.Threading.UnitTests
{
    [TestClass]
    public class ThreadPoolInternalTests
    {
        private const int WaitTimeoutMilliseconds = 10_000;

        [TestMethod]
        public void QueueUserWorkItem_returns_false_when_full()
        {
            // Arrange
            var completedEvent = new ManualResetEvent(false);
            var completedThreads = 0;
            var startEvent = new ManualResetEvent(false);
            var startedThreads = 0;

            using var sut = new ThreadPoolInternal(2, 2);

            var expected = sut.WorkItems + sut.Workers;

            for (var i = 0; i < expected; i++)
            {
                sut.QueueUserWorkItem(_ =>
                {
                    Interlocked.Increment(ref startedThreads);
                    startEvent.WaitOne();
                    var completed = Interlocked.Increment(ref completedThreads);

                    if (completed == expected)
                    {
                        completedEvent.Set();
                    }
                });
            }

            // Act
            var workItemQueued = sut.QueueUserWorkItem(_ => { });

            startEvent.Set();

            var completed = completedEvent.WaitOne(WaitTimeoutMilliseconds, false);

            // Assert
            Assert.IsFalse(workItemQueued, "Work item queued");
            Assert.IsTrue(completed, "Completed");
        }

        [TestMethod]
        public void QueueUserWorkItem_returns_true_when_item_posted()
        {
            // Arrange
            var completedEvent = new ManualResetEvent(false);
            var startEvent = new ManualResetEvent(false);

            using var sut = new ThreadPoolInternal(1, 1);

            // Act
            var workItemQueued = sut.QueueUserWorkItem(_ =>
            {
                startEvent.WaitOne();
                completedEvent.Set();
            });

            // Assert
            Assert.AreEqual(0, sut.PendingWorkItemCount, "Work item was not queued");
            Assert.IsTrue(workItemQueued, "Work item posted");

            startEvent.Set();

            var completed = completedEvent.WaitOne(WaitTimeoutMilliseconds, false);

            Assert.IsTrue(completed, "Work item completed");
            Assert.AreEqual(0, sut.PendingWorkItemCount, "Pending work item count");
        }

        [TestMethod]
        public void QueueUserWorkItem_returns_true_when_item_queued()
        {
            // Arrange
            var completedEvent = new ManualResetEvent(false);
            var startEvent = new ManualResetEvent(false);
            var workItemsProcessed = 0;
            var workItemsStarted = 0;

            using var sut = new ThreadPoolInternal(1, 1);

            var expected = sut.WorkItems + sut.Workers;

            for (var i = 0; i < sut.Workers; i++)
            {
                sut.QueueUserWorkItem(_ =>
                {
                    Interlocked.Increment(ref workItemsStarted);
                    startEvent.WaitOne();

                    var processed = Interlocked.Increment(ref workItemsProcessed);
                    if (processed == expected)
                    {
                        completedEvent.Set();
                    }
                });
            }

            // Act
            var workItemQueued = sut.QueueUserWorkItem(_ =>
            {
                Interlocked.Increment(ref workItemsStarted);
                startEvent.WaitOne();

                var processed = Interlocked.Increment(ref workItemsProcessed);
                if (processed == expected)
                {
                    completedEvent.Set();
                }
            });

            // Assert
            Assert.AreEqual(1, sut.PendingWorkItemCount, "Work item was not queued");
            Assert.IsTrue(workItemQueued, "Work item queued");

            startEvent.Set();

            var completed = completedEvent.WaitOne(WaitTimeoutMilliseconds, false);

            Assert.IsTrue(completed, "Work item completed");
            Assert.AreEqual(0, sut.PendingWorkItemCount, "Pending work item count");
            Assert.AreEqual(expected, workItemsProcessed);
        }

        [TestMethod]
        public void QueueUserWorkItem_throws_if_callback_is_null()
        {
            using var sut = new ThreadPoolInternal(1, 1);

            Assert.ThrowsException(typeof(ArgumentNullException), () => { sut.QueueUserWorkItem(null!); });
        }

        [TestMethod]
        public void QueueUserWorkItem_throws_if_disposed()
        {
            var sut = new ThreadPoolInternal(1, 1);
            sut.Dispose();

            Assert.ThrowsException(typeof(ObjectDisposedException), () => { sut.QueueUserWorkItem(_ => { }); });
        }

        [TestMethod]
        public void SetMinThreads_returns_false_if_workerThreads_is_out_of_range()
        {
            using var sut = new ThreadPoolInternal(1, 1);

            Assert.IsFalse(sut.SetMinThreads(0), "0");
            Assert.IsFalse(sut.SetMinThreads(-1), "-1");
            Assert.IsFalse(sut.SetMinThreads(sut.Workers + 1), "Greater than Workers");
        }

        [TestMethod]
        public void SetMinThreads_returns_true()
        {
            const int workerCount = 16;
            using var sut = new ThreadPoolInternal(workerCount, workerCount);

            Console.WriteLine("1");
            Assert.IsTrue(sut.SetMinThreads(workerCount, true), "First pass");
            Assert.AreEqual(workerCount, sut.ThreadCount, "First pass thread count");

            Console.WriteLine("2");
            Assert.IsTrue(sut.SetMinThreads(workerCount, true), "Second pass");
            Assert.AreEqual(workerCount, sut.ThreadCount, "Second pass thread count");
        }
  
        [TestMethod]
        public void SetMinThreads_throws_if_disposed()
        {
            var sut = new ThreadPoolInternal(1, 1);
            sut.Dispose();

            Assert.ThrowsException(typeof(ObjectDisposedException), () => { sut.SetMinThreads(1); });
        }
    }
}
