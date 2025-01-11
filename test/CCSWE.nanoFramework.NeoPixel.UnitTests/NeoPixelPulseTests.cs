using CCSWE.nanoFramework.NeoPixel.Rmt;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.NeoPixel.UnitTests
{
    [TestClass]
    public class NeoPixelPulseTests
    {
        [TestMethod]
        public void Constructor_should_accept_a_byte_array()
        {
            var expect = new byte[] { 1, 2, 3, 4 };
            var sut = new NeoPixelPulse(expect);

            Assert.AreEqual(expect[0], sut.Duration0);
            Assert.AreEqual(expect[1], sut.Level0);
            Assert.AreEqual(expect[2], sut.Duration1);
            Assert.AreEqual(expect[3], sut.Level1);
        }

        [TestMethod]
        public void Constructor_should_accept_byte_parameters()
        {
            var expect = new byte[] { 1, 2, 3, 4 };
            var sut = new NeoPixelPulse(expect[0], expect[1], expect[2], expect[3]);

            Assert.AreEqual(expect[0], sut.Duration0);
            Assert.AreEqual(expect[1], sut.Level0);
            Assert.AreEqual(expect[2], sut.Duration1);
            Assert.AreEqual(expect[3], sut.Level1);
        }
    }
}
