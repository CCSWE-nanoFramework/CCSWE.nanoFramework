using CCSWE.nanoFramework.Collections.Concurrent.UnitTests.Mocks;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Collections.Concurrent.UnitTests
{
    [TestClass]
    public class ConcurrentListTests
    {
        [TestMethod]
        public void Add_should_add_item()
        {
            var expect = new MockItem();
            var sut = new ConcurrentList();

            var index = sut.Add(expect);

            Assert.AreEqual(0, index);
            Assert.AreEqual(1, sut.Count);
            Assert.AreEqual(expect, sut[index]);
        }

        [TestMethod]
        public void Add_should_return_correct_index()
        {
            const int create = 10;
            var sut = new ConcurrentList();

            for (var i = 0; i < create; i++)
            {
                var index = sut.Add(i);
                Assert.AreEqual(i, index);
            }
        }

        [TestMethod]
        public void Clear_should_remove_all_items()
        {
            const int create = 10;
            const int expected = 0;
            var sut = new ConcurrentList();

            for (var i = 0; i < create; i++)
            {
                sut.Add(i);
            }

            sut.Clear();

            Assert.AreEqual(expected, sut.Count);
        }

        [TestMethod]
        public void Contains_should_return_false()
        {
            const int create = 10;
            var sut = new ConcurrentList();

            for (var i = 0; i < create; i++)
            {
                sut.Add(i);
            }

            Assert.IsFalse(sut.Contains(null!));
            Assert.IsFalse(sut.Contains(create + 1));
        }

        [TestMethod]
        public void Contains_should_return_true()
        {
            const int create = 10;
            var sut = new ConcurrentList();

            for (var i = 0; i < create; i++)
            {
                sut.Add(i);
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
            var sut = new ConcurrentList();

            for (var i = 0; i < create; i++)
            {
                sut.Add(i);
                Assert.AreEqual(i + 1, sut.Count);
            }
        }

        [TestMethod]
        public void IndexOf_should_return_correct_index()
        {
            var first = new MockItem();
            var second = new MockItem();
            var sut = new ConcurrentList();

            Assert.AreEqual(-1, sut.IndexOf(first));
            Assert.AreEqual(-1, sut.IndexOf(second));

            var firstIndex = sut.Add(first);

            Assert.AreEqual(firstIndex, sut.IndexOf(first));
            Assert.AreEqual(-1, sut.IndexOf(second));

            var secondIndex = sut.Add(second);

            Assert.AreEqual(firstIndex, sut.IndexOf(first));
            Assert.AreEqual(secondIndex, sut.IndexOf(second));
        }

        [TestMethod]
        public void Insert_should_insert_item()
        {
            var first = new MockItem();
            var second = new MockItem();
            var third = new MockItem();
            var sut = new ConcurrentList();

            var firstIndex = sut.Add(first);
            var secondIndex = sut.Add(second);

            Assert.AreEqual(firstIndex, sut.IndexOf(first));
            Assert.AreEqual(secondIndex, sut.IndexOf(second));

            sut.Insert(secondIndex, third);

            Assert.AreEqual(firstIndex, sut.IndexOf(first));
            Assert.AreEqual(secondIndex + 1, sut.IndexOf(second));
            Assert.AreEqual(secondIndex, sut.IndexOf(third));
        }

        [TestMethod]
        public void Remove_should_remove_item()
        {
            var item = new MockItem();
            var sut = new ConcurrentList { item };

            Assert.IsTrue(sut.Contains(item));
            Assert.AreEqual(1, sut.Count);

            sut.Remove(item);

            Assert.IsFalse(sut.Contains(item));
            Assert.AreEqual(0, sut.Count);
        }

        [TestMethod]
        public void RemoveAt_should_remove_item()
        {
            var first = new MockItem();
            var second = new MockItem();
            var third = new MockItem();
            
            var sut = new ConcurrentList
            {
                first,
                second,
                third
            };

            Assert.AreEqual(3, sut.Count);

            sut.RemoveAt(1);

            Assert.IsTrue(sut.Contains(first));
            Assert.IsFalse(sut.Contains(second));
            Assert.IsTrue(sut.Contains(third));

            Assert.AreEqual(2, sut.Count);
            Assert.AreEqual(0, sut.IndexOf(first));
            Assert.AreEqual(-1, sut.IndexOf(second));
            Assert.AreEqual(1, sut.IndexOf(third));
        }
    }
}
