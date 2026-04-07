using System.Drawing;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.NeoPixel.UnitTests
{
    [TestClass]
    public class ColorExtensionsTests
    {
        // Use a color with distinct R, G, B values so ordering bugs are detectable.
        private static readonly Color TestColor = Color.FromArgb(255, 10, 20, 30);

        [TestMethod]
        public void ToBytes_should_handle_GRB_color_order()
        {
            var actual = TestColor.ToBytes(ColorOrder.GRB);

            Assert.AreEqual(TestColor.G, actual[0]);
            Assert.AreEqual(TestColor.R, actual[1]);
            Assert.AreEqual(TestColor.B, actual[2]);
        }

        [TestMethod]
        public void ToBytes_should_handle_RGB_color_order()
        {
            var actual = TestColor.ToBytes(ColorOrder.RGB);

            Assert.AreEqual(TestColor.R, actual[0]);
            Assert.AreEqual(TestColor.G, actual[1]);
            Assert.AreEqual(TestColor.B, actual[2]);
        }
    }
}
