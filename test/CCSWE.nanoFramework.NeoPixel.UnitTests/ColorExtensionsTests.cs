using System.Drawing;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.NeoPixel.UnitTests
{
    [TestClass]
    public class ColorExtensionsTests
    {
        [TestMethod]
        public void ToBytes_should_handle_GRB_color_order()
        {
            var expect = Color.White;
            var actual = expect.ToBytes(ColorOrder.GRB);

            Assert.AreEqual(expect.G, actual[0]);
            Assert.AreEqual(expect.R, actual[1]);
            Assert.AreEqual(expect.B, actual[2]);
        }

        [TestMethod]
        public void ToBytes_should_handle_RGB_color_order()
        {
            var expect = Color.White;
            var actual = expect.ToBytes(ColorOrder.GRB);

            Assert.AreEqual(expect.R, actual[0]);
            Assert.AreEqual(expect.G, actual[1]);
            Assert.AreEqual(expect.B, actual[2]);
        }
    }
}
