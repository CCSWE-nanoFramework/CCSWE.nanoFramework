using nanoFramework.TestFramework;
#pragma warning disable CS0618 // Type or member is obsolete

namespace CCSWE.nanoFramework.UnitTests
{
    [TestClass]
    public class Math_Abs_Tests
    {
        [TestMethod]
        public void Abs_Double_handles_positive_value()
        {
            Assert.AreEqual(double.MaxValue, Math.Abs(double.MaxValue));
        }

        [TestMethod]
        public void Abs_Double_handles_negative_value()
        {
            Assert.AreEqual(double.MaxValue, Math.Abs(double.MinValue));
        }

        [TestMethod]
        public void Abs_Float_handles_positive_value()
        {
            Assert.AreEqual(float.MaxValue, Math.Abs(float.MaxValue));
        }

        [TestMethod]
        public void Abs_Float_handles_negative_value()
        {
            Assert.AreEqual(float.MaxValue, Math.Abs(float.MinValue));
        }

        [TestMethod]
        public void Abs_Int_handles_positive_value()
        {
            Assert.AreEqual(1234567890, Math.Abs(1234567890));
        }

        [TestMethod]
        public void Abs_Int_handles_negative_value()
        {
            Assert.AreEqual(1234567890, Math.Abs(-1234567890));
        }
    }
}
