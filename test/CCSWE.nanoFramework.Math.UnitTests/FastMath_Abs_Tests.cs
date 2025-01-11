using nanoFramework.TestFramework;
#pragma warning disable CS0618 // Type or member is obsolete

namespace CCSWE.nanoFramework.UnitTests
{
    [TestClass]
    public class FastMath_Abs_Tests
    {
        [TestMethod]
        public void Abs_Double_handles_positive_value()
        {
            Assert.AreEqual(double.MaxValue, FastMath.Abs(double.MaxValue));
        }

        [TestMethod]
        public void Abs_Double_handles_negative_value()
        {
            Assert.AreEqual(double.MaxValue, FastMath.Abs(double.MinValue));
        }

        [TestMethod]
        public void Abs_Float_handles_positive_value()
        {
            Assert.AreEqual(float.MaxValue, FastMath.Abs(float.MaxValue));
        }

        [TestMethod]
        public void Abs_Float_handles_negative_value()
        {
            Assert.AreEqual(float.MaxValue, FastMath.Abs(float.MinValue));
        }

        [TestMethod]
        public void Abs_Int_handles_positive_value()
        {
            Assert.AreEqual(1234567890, FastMath.Abs(1234567890));
        }

        [TestMethod]
        public void Abs_Int_handles_negative_value()
        {
            Assert.AreEqual(1234567890, FastMath.Abs(-1234567890));
        }
    }
}
