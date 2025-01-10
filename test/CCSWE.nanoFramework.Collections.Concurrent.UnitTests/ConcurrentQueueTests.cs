using CCSWE.nanoFramework.Collections.Concurrent.UnitTests.Mocks;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Collections.Concurrent.UnitTests
{
    [TestClass]
    public class ConcurrentQueueTests
    {
        // TODO: Clone
        // TODO: CopyTo

        [TestMethod]
        public void Clear_should_remove_all_items()
        {
            const int create = 10;
            const int expected = 0;
            var sut = new ConcurrentQueue();

            for (var i = 0; i < create; i++)
            {
                sut.Enqueue(i);
            }

            sut.Clear();

            Assert.AreEqual(expected, sut.Count);
        }

        [TestMethod]
        public void Contains_should_return_false_for_null()
        {
            const int create = 10;
            var sut = new ConcurrentQueue();

            for (var i = 0; i < create; i++)
            {
                sut.Enqueue(i);
            }

            Assert.IsFalse(sut.Contains(null!));
        }

        [TestMethod]
        public void Contains_should_return_true()
        {
            const int create = 10;
            var sut = new ConcurrentQueue();

            for (var i = 0; i < create; i++)
            {
                sut.Enqueue(i);
            }

            for (var i = 0; i < create; i++)
            {
                Assert.IsTrue(sut.Contains(i));
            }
        }

        [TestMethod]
        public void Count_should_return_correct_value()
        {
            const int create = 10;
            var sut = new ConcurrentQueue();

            for (var i = 0; i < create; i++)
            {
                sut.Enqueue(i);
                Assert.AreEqual(i + 1, sut.Count);
            }
        }

        [TestMethod]
        public void Dequeue_should_remove_item()
        {
            var expected = new MockItem();
            var sut = new ConcurrentQueue();

            sut.Enqueue(expected);

            Assert.AreEqual(1, sut.Count);

            var actual = sut.Dequeue();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, sut.Count);
        }

        [TestMethod]
        public void Enqueue_should_add_item()
        {
            var expected = new MockItem();
            var sut = new ConcurrentQueue();

            sut.Enqueue(expected);

            Assert.AreEqual(1, sut.Count);
        }

        [TestMethod]
        public void IsSynchronized_should_be_true()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var sut = new ConcurrentQueue();

            Assert.IsTrue(sut.IsSynchronized);
        }

        [TestMethod]
        public void Peek_should_not_remove_item()
        {
            var expected = new MockItem();
            var sut = new ConcurrentQueue();
            sut.Enqueue(expected);

            Assert.AreEqual(1, sut.Count);

            var actual = sut.Peek();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(1, sut.Count);
        }

        [TestMethod]
        public void TryDequeue_should_return_false()
        {
            var expected = new MockItem();
            var sut = new ConcurrentQueue();

            var result = sut.TryDequeue(out var actual);

            Assert.IsFalse(result);
            Assert.IsNull(actual);
            Assert.AreNotEqual(expected, actual);
            Assert.AreEqual(0, sut.Count);
        }

        [TestMethod]
        public void TryDequeue_should_return_true()
        {
            var expected = new MockItem();
            var sut = new ConcurrentQueue();
            sut.Enqueue(expected);

            var result = sut.TryDequeue(out var actual);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, sut.Count);
        }
    }
}
