using CCSWE.nanoFramework.IO;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Core.UnitTests.IO
{
    [TestClass]
    public class PathTests
    {
        [TestMethod]
        public void GetExtension_returns_empty_string()
        {
            Assert.AreEqual(string.Empty, Path.GetExtension(string.Empty));
            Assert.AreEqual(string.Empty, Path.GetExtension("file"));
            Assert.AreEqual(string.Empty, Path.GetExtension("file."));
        }

        [TestMethod]
        public void GetExtension_returns_extension()
        {
            var file = "file.ext";
            var expect = ".ext";

            Assert.AreEqual(expect, Path.GetExtension(file));
            Assert.AreEqual(expect, Path.GetExtension($"I:{file}"));
            Assert.AreEqual(expect, Path.GetExtension(@$"I:\{file}"));
            Assert.AreEqual(expect, Path.GetExtension(@$"I:\directory\{file}"));
            Assert.AreEqual(expect, Path.GetExtension(@$"\{file}"));
            Assert.AreEqual(expect, Path.GetExtension(@$"\\server\share\{file}"));
            Assert.AreEqual(expect, Path.GetExtension(@$"\\server\share\directory\{file}"));
        }

        [TestMethod]
        public void GetExtension_returns_null()
        {
            Assert.IsNull(Path.GetExtension(null));
        }
    }
}
