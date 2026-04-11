using System.Drawing;
using CCSWE.nanoFramework.Graphics;
using nanoFramework.TestFramework;

namespace UnitTests.CCSWE.nanoFramework.Graphics;

[TestClass]
public class ColorExtensionsTests
{
    // Use a color with distinct R, G, B values so ordering bugs are detectable.
    private static readonly Color TestColor = Color.FromArgb(255, 10, 20, 30);

    [TestMethod]
    public void ToBytes_should_handle_Grb_color_order()
    {
        var actual = TestColor.ToBytes(ColorOrder.Grb);

        Assert.AreEqual(TestColor.G, actual[0]);
        Assert.AreEqual(TestColor.R, actual[1]);
        Assert.AreEqual(TestColor.B, actual[2]);
    }

    [TestMethod]
    public void ToBytes_should_handle_Rgb_color_order()
    {
        var actual = TestColor.ToBytes(ColorOrder.Rgb);

        Assert.AreEqual(TestColor.R, actual[0]);
        Assert.AreEqual(TestColor.G, actual[1]);
        Assert.AreEqual(TestColor.B, actual[2]);
    }
}
