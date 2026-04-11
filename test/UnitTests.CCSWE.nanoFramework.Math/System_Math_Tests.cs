using System;
using nanoFramework.TestFramework;

namespace UnitTests.CCSWE.nanoFramework;
[TestClass]
public class System_Math_Tests
{
    [TestMethod]
    public void Testing()
    {
        Assert.SkipTest("This should not be checked in");

        var expect = 1234.5678d;
        var lower = expect / 2;

        var actual1 = Math.Max(expect, lower);
        var actual2 = Math.Max(lower, expect);

        Console.WriteLine($"Expect: {expect} - {(float)expect}");
        Console.WriteLine($"Actual 1: {actual1}");
        Console.WriteLine($"Actual 2: {actual1}");

        Console.WriteLine($"Test: {Math.Abs(expect - actual1)}");
        Console.WriteLine($"Double: {double.Epsilon} - {Math.Abs(expect - actual1) < double.Epsilon}");
        Console.WriteLine($"Float: {float.Epsilon} - {Math.Abs(expect - actual1) < float.Epsilon}");

        Assert.IsTrue(Math.Abs(expect - actual1) < double.Epsilon);
        Assert.IsTrue(Math.Abs(expect - actual2) < double.Epsilon);
    }

    [TestMethod]
    public void Max_Double_returns_greater_value()
    {
        // TODO: Maybe I'm missing something here but the change in precision here seems like a defect as my expectation is that the same value sent in is returned
        //Assert.SkipTest("Skipping pending further investigation");

        var expect = 1234.5678d;
        var lower = expect / 2;

        Assert.AreEqual(expect, Math.Max(expect, lower));
        Assert.AreEqual(expect, Math.Max(lower, expect));
    }

    [TestMethod]
    public void Max_Double_returns_NaN_if_val1_is_NaN()
    {
        Assert.IsTrue(double.IsNaN(Math.Max(double.NaN, Math.PI)));
    }

    [TestMethod]
    public void Max_Double_returns_NaN_if_val2_is_NaN()
    {
        Assert.IsTrue(double.IsNaN(Math.Max(Math.PI, double.NaN)));
    }

    [TestMethod]
    public void Max_Float_returns_greater_value()
    {
        var expect = 1234.5678f;
        var lower = expect / 2;

        Assert.AreEqual(expect, Math.Max(expect, lower));
        Assert.AreEqual(expect, Math.Max(lower, expect));
    }

    [TestMethod]
    public void Max_Float_returns_NaN_if_val1_is_NaN()
    {
        Assert.IsTrue(double.IsNaN(Math.Max(float.NaN, (float) Math.PI)));
    }

    [TestMethod]
    public void Max_Float_returns_NaN_if_val2_is_NaN()
    {
        Assert.IsTrue(double.IsNaN(Math.Max((float) Math.PI, float.NaN)));
    }

    [TestMethod]
    public void Min_Double_returns_lesser_value()
    {
        var expect = 1234.5678d;
        var higher = expect * 2.0d;

        Assert.AreEqual(expect, Math.Min(expect, higher));
        Assert.AreEqual(expect, Math.Min(higher, expect));
    }

    [TestMethod]
    public void Min_Double_returns_NaN_if_val1_is_NaN()
    {
        // This test incorrectly returns PI
        Assert.IsTrue(double.IsNaN(Math.Min(double.NaN, Math.PI)));
    }

    [TestMethod]
    public void Min_Double_returns_NaN_if_val2_is_NaN()
    {
        Assert.IsTrue(double.IsNaN(Math.Min(Math.PI, double.NaN)));
    }

    [TestMethod]
    public void Min_Float_returns_lesser_value()
    {
        var expect = 1234.5678f;
        var higher = expect * 2.0f;

        Assert.AreEqual(expect, Math.Min(expect, higher));
        Assert.AreEqual(expect, Math.Min(higher, expect));
    }

    [TestMethod]
    public void Min_Float_returns_NaN_if_val1_is_NaN()
    {
        // This test incorrectly returns PI
        Assert.IsTrue(double.IsNaN(Math.Min(float.NaN, (float) Math.PI)));
    }

    [TestMethod]
    public void Min_Float_returns_NaN_if_val2_is_NaN()
    {
        Assert.IsTrue(double.IsNaN(Math.Min((float) Math.PI, float.NaN)));
    }
}
