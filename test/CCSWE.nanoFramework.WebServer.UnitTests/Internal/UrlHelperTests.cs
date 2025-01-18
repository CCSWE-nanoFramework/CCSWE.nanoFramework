using CCSWE.nanoFramework.WebServer.Internal;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Internal
{
    [TestClass]
    public class UrlHelperTests
    {
        [TestMethod]
        public void CombinePathSegments_both_segments_empty()
        {
            var result = UrlHelper.CombinePathSegments(string.Empty, string.Empty);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void CombinePathSegments_both_segments_with_slashes()
        {
            var result = UrlHelper.CombinePathSegments("segmentA/", "/segmentB");
            Assert.AreEqual("segmentA/segmentB", result);
        }

        [TestMethod]
        public void CombinePathSegments_both_segments_without_slashes()
        {
            var result = UrlHelper.CombinePathSegments("segmentA", "segmentB");
            Assert.AreEqual("segmentA/segmentB", result);
        }

        [TestMethod]
        public void CombinePathSegments_empty_first_segment()
        {
            var result = UrlHelper.CombinePathSegments(string.Empty, "segmentB");
            Assert.AreEqual("segmentB", result);
        }

        [TestMethod]
        public void CombinePathSegments_empty_second_segment()
        {
            var result = UrlHelper.CombinePathSegments("segmentA", string.Empty);
            Assert.AreEqual("segmentA", result);
        }

        [TestMethod]
        public void CombinePathSegments_first_segment_with_trailing_slash()
        {
            var result = UrlHelper.CombinePathSegments("segmentA/", "segmentB");
            Assert.AreEqual("segmentA/segmentB", result);
        }

        [TestMethod]
        public void CombinePathSegments_second_segment_with_leading_slash()
        {
            var result = UrlHelper.CombinePathSegments("segmentA", "/segmentB");
            Assert.AreEqual("segmentA/segmentB", result);
        }
    }
}