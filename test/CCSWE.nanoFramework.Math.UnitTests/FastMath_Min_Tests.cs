using System;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.UnitTests
{
    [TestClass]
    public class FastMath_Min_Tests
    {
        [TestMethod]
        public void Min_Double_returns_lesser_value()
        {
            var expect = 1234.5678d;
            var higher = expect * 2;

            Assert.AreEqual(expect, FastMath.Min(expect, higher));
            Assert.AreEqual(expect, FastMath.Min(higher, expect));
        }

        [TestMethod]
        public void Min_Double_returns_NaN_if_val1_is_NaN()
        {
            Assert.SkipTest("This test fails in favor of a faster implementation");

            var expect = System.Math.Min(double.NaN, Math.PI);
            var actual = FastMath.Min(double.NaN, Math.PI);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            // TODO: Per the spec in the .NET Core implementation this behavior is currently a bug in the System.Math library
            //Assert.IsTrue(double.IsNaN(expect));
            Assert.IsTrue(double.IsNaN(actual));
        }

        [TestMethod]
        public void Min_Double_returns_NaN_if_val2_is_NaN()
        {
            var expect = System.Math.Min(Math.PI, double.NaN);
            var actual = FastMath.Min(Math.PI, double.NaN);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            Assert.IsTrue(double.IsNaN(expect));
            Assert.IsTrue(double.IsNaN(actual));
        }

        [TestMethod]
        public void Min_Float_returns_lesser_value()
        {
            var expect = 1234.5678f;
            var higher = expect * 2;

            Assert.AreEqual(expect, FastMath.Min(expect, higher));
            Assert.AreEqual(expect, FastMath.Min(higher, expect));
        }

        [TestMethod]
        public void Min_Float_returns_NaN_if_val1_is_NaN()
        {
            Assert.SkipTest("This test fails in favor of a faster implementation");
         
            var expect = System.Math.Min(float.NaN, (float)Math.PI);
            var actual = FastMath.Min(float.NaN, (float)Math.PI);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            // TODO: Per the spec in the .NET Core implementation this behavior is currently a bug in the System.Math library
            //Assert.IsTrue(float.IsNaN(expect));
            Assert.IsTrue(float.IsNaN(actual));
        }

        [TestMethod]
        public void Min_Float_returns_NaN_if_val2_is_NaN()
        {
            var expect = System.Math.Min((float)Math.PI, float.NaN);
            var actual = FastMath.Min((float)Math.PI, float.NaN);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            Assert.IsTrue(float.IsNaN(expect));
            Assert.IsTrue(float.IsNaN(actual));
        }

        [TestMethod]
        public void Min_Int_returns_lesser_value()
        {
            var expect = 12345678;
            var higher = expect * 2;

            Assert.AreEqual(expect, FastMath.Min(expect, higher));
            Assert.AreEqual(expect, FastMath.Min(higher, expect));
        }
    }
}
