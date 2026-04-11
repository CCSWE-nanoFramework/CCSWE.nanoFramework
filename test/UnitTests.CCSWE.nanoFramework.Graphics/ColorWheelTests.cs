using CCSWE.nanoFramework.Graphics;
using nanoFramework.TestFramework;

namespace UnitTests.CCSWE.nanoFramework.Graphics;

[TestClass]
public class ColorWheelTests
{
    // ------------------------------------------------------------------ GetColor

    [TestMethod]
    public void GetColor_at_position_0_should_return_red()
    {
        var color = ColorWheel.GetColor(0);

        Assert.AreEqual((byte)0, color.R);
        Assert.AreEqual((byte)255, color.G);
        Assert.AreEqual((byte)0, color.B);
    }

    [TestMethod]
    public void GetColor_at_position_84_should_be_in_first_sector()
    {
        var color = ColorWheel.GetColor(84);

        // sector 1: R = 84*3=252, G = 255-252=3, B = 0
        Assert.AreEqual((byte)252, color.R);
        Assert.AreEqual((byte)3, color.G);
        Assert.AreEqual((byte)0, color.B);
    }

    [TestMethod]
    public void GetColor_at_position_85_should_be_in_second_sector()
    {
        // position 85 enters case < 170: position becomes 0, R=255, G=0, B=0
        var color = ColorWheel.GetColor(85);

        Assert.AreEqual((byte)255, color.R);
        Assert.AreEqual((byte)0, color.G);
        Assert.AreEqual((byte)0, color.B);
    }

    [TestMethod]
    public void GetColor_at_position_170_should_be_in_third_sector()
    {
        // position 170 enters default: position becomes 0, R=0, G=0, B=255
        var color = ColorWheel.GetColor(170);

        Assert.AreEqual((byte)0, color.R);
        Assert.AreEqual((byte)0, color.G);
        Assert.AreEqual((byte)255, color.B);
    }

    [TestMethod]
    public void GetColor_at_position_255_should_be_near_blue_end()
    {
        // position 255 enters default: position becomes 85, R=0, G=255, B=255-255=0
        var color = ColorWheel.GetColor(255);

        Assert.AreEqual((byte)0, color.R);
        Assert.AreEqual((byte)255, color.G);
        Assert.AreEqual((byte)0, color.B);
    }
}
