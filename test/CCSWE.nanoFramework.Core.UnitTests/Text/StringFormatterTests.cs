using CCSWE.nanoFramework.Text;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Core.UnitTests.Text
{
    [TestClass]
    public class StringFormatterTests
    {
        [TestMethod]
        public void ToFileSizeString_formats_bytes()
        {
            const long value = 123;
            const string expect = "123.00 B";
            var actual = StringFormatter.ToFileSizeString(value);

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void ToFileSizeString_formats_gigabytes()
        {
            const long value = 1_234_567_890;
            const string expect = "1.15 GB";
            var actual = StringFormatter.ToFileSizeString(value);

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void ToFileSizeString_formats_kilobytes()
        {
            const long value = 1_234;
            const string expect = "1.21 KB";
            var actual = StringFormatter.ToFileSizeString(value);

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void ToFileSizeString_formats_megabytes()
        {
            const long value = 1_234_567;
            const string expect = "1.18 MB";
            var actual = StringFormatter.ToFileSizeString(value);

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void ToFileSizeString_formats_terabytes()
        {
            const long value = 1_234_567_890_123;
            const string expect = "1.12 TB";
            var actual = StringFormatter.ToFileSizeString(value);

            Assert.AreEqual(expect, actual);
        }
    }
}
