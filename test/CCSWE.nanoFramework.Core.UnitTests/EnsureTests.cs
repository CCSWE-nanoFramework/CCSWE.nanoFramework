using nanoFramework.TestFramework;
using System;

namespace CCSWE.nanoFramework.Core.UnitTests
{
    [TestClass]
    public class EnsureTests
    {
        [TestMethod]
        public void GetException_returns_ArgumentException()
        {
            Assert.IsInstanceOfType(Ensure.GetException(typeof(ArgumentException), nameof(EnsureTests)), typeof(ArgumentException));
        }

        [TestMethod]
        public void GetException_returns_ArgumentNullException()
        {
            Assert.IsInstanceOfType(Ensure.GetException(typeof(ArgumentNullException), nameof(EnsureTests)), typeof(ArgumentNullException));
        }

        [TestMethod]
        public void GetException_returns_ArgumentOutOfRangeException()
        {
            Assert.IsInstanceOfType(Ensure.GetException(typeof(ArgumentOutOfRangeException), nameof(EnsureTests)), typeof(ArgumentOutOfRangeException));
        }

        [TestMethod]
        public void GetException_returns_IndexOutOfRangeException()
        {
            Assert.IsInstanceOfType(Ensure.GetException(typeof(IndexOutOfRangeException), nameof(EnsureTests)), typeof(IndexOutOfRangeException));
        }

        [TestMethod]
        public void GetException_returns_InvalidOperationException()
        {
            Assert.IsInstanceOfType(Ensure.GetException(typeof(InvalidOperationException), nameof(EnsureTests)), typeof(InvalidOperationException));
        }

        [TestMethod]
        public void IsInRange_does_not_throw()
        {
            Ensure.IsInRange(true, nameof(EnsureTests));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsInRange_does_not_throw_for_double()
        {
            Ensure.IsInRange(1d, 0, 1, nameof(EnsureTests));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsInRange_does_not_throw_for_float()
        {
            Ensure.IsInRange(1f, 0, 1);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsInRange_does_not_throw_for_int()
        {
            Ensure.IsInRange(1, 0, 1);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsInRange_does_not_throw_for_long()
        {
            Ensure.IsInRange(1L, 0, 1);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsInRange_throws()
        {
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => Ensure.IsInRange(false, nameof(EnsureTests)));
        }

        [TestMethod]
        public void IsInRange_throws_for_double()
        {
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => Ensure.IsInRange(-1d, 0, 1, nameof(EnsureTests)));
        }

        [TestMethod]
        public void IsInRange_throws_for_float()
        {
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => Ensure.IsInRange(-1f, 0, 1));
        }

        [TestMethod]
        public void IsInRange_throws_for_int()
        {
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => Ensure.IsInRange(-1, 0, 1));
        }

        [TestMethod]
        public void IsInRange_throws_for_long()
        {
            Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => Ensure.IsInRange(-1L, 0, 1));
        }

        [TestMethod]
        public void IsNotNull_does_not_throw()
        {
            Ensure.IsNotNull(new object());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsNotNull_throws()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => Ensure.IsNotNull(null));
        }

        [TestMethod]
        public void IsNotNullOrEmpty_does_not_throw()
        {
            Ensure.IsNotNullOrEmpty(nameof(IsNotNullOrEmpty_does_not_throw));
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsNotNullOrEmpty_throws()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => Ensure.IsNotNullOrEmpty(null));
            Assert.ThrowsException(typeof(ArgumentException), () => Ensure.IsNotNullOrEmpty(string.Empty));
        }

        [TestMethod]
        public void IsValid_does_not_throws()
        {
            Ensure.IsValid(true);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void IsValid_throws()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => Ensure.IsValid(false));
        }
    }
}
