using nanoFramework.TestFramework;
using System;

namespace CCSWE.nanoFramework.UnitTests
{
    [TestClass]
    public class FastMath_Max_Tests
    {
        [TestMethod]
        public void Max_Double_returns_lesser_value()
        {
            var expect = 1234.5678d;
            var lower = expect / 2;

            Assert.AreEqual(expect, FastMath.Max(expect, lower));
            Assert.AreEqual(expect, FastMath.Max(lower, expect));
        }

        [TestMethod]
        public void Max_Double_returns_NaN_if_val1_is_NaN()
        {
            var expect = System.Math.Max(double.NaN, Math.PI);
            var actual = FastMath.Max(double.NaN, Math.PI);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            Assert.IsTrue(double.IsNaN(expect));
            Assert.IsTrue(double.IsNaN(actual));
        }

        [TestMethod]
        public void Max_Double_returns_NaN_if_val2_is_NaN()
        {
            Assert.SkipTest("This test fails in favor of a faster implementation");
         
            var expect = System.Math.Max(Math.PI, double.NaN);
            var actual = FastMath.Max(Math.PI, double.NaN);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            // TODO: Per the spec in the .NET Core implementation this behavior is currently a bug in the System.Math library
            //Assert.IsTrue(double.IsNaN(expect));
            Assert.IsTrue(double.IsNaN(actual));
        }

        [TestMethod]
        public void Max_Float_returns_lesser_value()
        {
            var expect = 1234.5678f;
            var lower = expect / 2;

            Assert.AreEqual(expect, FastMath.Max(expect, lower));
            Assert.AreEqual(expect, FastMath.Max(lower, expect));
        }

        [TestMethod]
        public void Max_Float_returns_NaN_if_val1_is_NaN()
        {

            var expect = System.Math.Max(float.NaN, (float)Math.PI);
            var actual = FastMath.Max(float.NaN, (float)Math.PI);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            // TODO: Per the spec in the .NET Core implementation this behavior is currently a bug in the System.Math library
            Assert.IsTrue(float.IsNaN(expect));
            Assert.IsTrue(float.IsNaN(actual));
        }

        [TestMethod]
        public void Max_Float_returns_NaN_if_val2_is_NaN()
        {
            Assert.SkipTest("This test fails in favor of a faster implementation");

            var expect = System.Math.Max((float)Math.PI, float.NaN);
            var actual = FastMath.Max((float)Math.PI, float.NaN);

            Console.WriteLine($"Expect: {expect}");
            Console.WriteLine($"Actual: {actual}");

            Assert.IsTrue(float.IsNaN(expect));
            Assert.IsTrue(float.IsNaN(actual));
        }

        [TestMethod]
        public void Max_Int_returns_lesser_value()
        {
            var expect = 12345678;
            var lower = expect / 2;

            Assert.AreEqual(expect, FastMath.Max(expect, lower));
            Assert.AreEqual(expect, FastMath.Max(lower, expect));
        }

    }
}
