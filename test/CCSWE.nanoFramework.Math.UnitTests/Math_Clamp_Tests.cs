using nanoFramework.TestFramework;
using System;

namespace CCSWE.nanoFramework.UnitTests
{
    [TestClass]
    public class Math_Clamp_Tests
    {
        #region Double
        [TestMethod]
        public void Clamp_Double_returns_max_when_value_is_greater_than_max()
        {
            Assert.AreEqual(1d, Math.Clamp(10d, -1d, 1d));
        }

        [TestMethod]
        public void Clamp_Double_returns_max_when_value_is_NaN()
        {
            Assert.AreEqual(1d, Math.Clamp(float.NaN, -1d, 1d));
        }

        [TestMethod]
        public void Clamp_Double_returns_min_when_value_is_less_than_min()
        {
            Assert.AreEqual(-1d, Math.Clamp(-10d, -1d, 1d));
        }

        [TestMethod]
        public void Clamp_Double_throws_when_max_is_less_than_min()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(10d, 1d, 0d); });
        }

        [TestMethod]
        public void Clamp_Double_throws_when_max_is_NaN()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(10d, 1d, double.NaN); });
        }

        [TestMethod]
        public void Clamp_Double_throws_when_min_is_NaN()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(double.NaN, double.NaN, double.NaN); });
        }
        #endregion

        #region Float
        [TestMethod]
        public void Clamp_Float_returns_max_when_value_is_greater_than_max()
        {
            Assert.AreEqual(1f, Math.Clamp(10f, -1f, 1f));
        }

        [TestMethod]
        public void Clamp_Float_returns_max_when_value_is_NaN()
        {
            Assert.AreEqual(1f, Math.Clamp(float.NaN, -1f, 1f));
        }

        [TestMethod]
        public void Clamp_Float_returns_min_when_value_is_less_than_min()
        {
            Assert.AreEqual(-1f, Math.Clamp(-10f, -1f, 1f));
        }

        [TestMethod]
        public void Clamp_Float_throws_when_max_is_less_than_mix()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(10f, 1f, 0d); });
        }

        [TestMethod]
        public void Clamp_Float_throws_when_max_is_NaN()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(10f, 1f, float.NaN); });
        }

        [TestMethod]
        public void Clamp_Float_throws_when_min_is_NaN()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(float.NaN, float.NaN, float.NaN); });
        }
        #endregion

        #region Long
        [TestMethod]
        public void Clamp_Long_returns_max_when_value_is_greater_than_max()
        {
            Assert.AreEqual(1L, Math.Clamp(10L, -1L, 1L));
        }

        [TestMethod]
        public void Clamp_Long_returns_min_when_value_is_less_than_min()
        {
            Assert.AreEqual(-1L, Math.Clamp(-10L, -1L, 1L));
        }

        [TestMethod]
        public void Clamp_Long_throws_when_max_is_less_than_mix()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(10L, 1L, 0L); });
        }
        #endregion

        #region ULong
        [TestMethod]
        public void Clamp_ULong_returns_max_when_value_is_greater_than_max()
        {
            Assert.AreEqual(1L, Math.Clamp(10UL, 0UL, 1UL));
        }

        [TestMethod]
        public void Clamp_ULong_returns_min_when_value_is_less_than_min()
        {
            Assert.AreEqual(5L, Math.Clamp(0L, 5L, 10L));
        }

        [TestMethod]
        public void Clamp_ULong_throws_when_max_is_less_than_mix()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => { Math.Clamp(10L, 1L, 0L); });
        }
        #endregion
    }
}
