using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.NeoPixel.UnitTests
{
    [TestClass]
    public class HslColorTests
    {
        // ------------------------------------------------------------------ Equals

        [TestMethod]
        public void Equals_should_return_false_for_different_alpha()
        {
            var a = new HslColor(180f, 50f, 75f, 255);
            var b = new HslColor(180f, 50f, 75f, 128);

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_should_return_false_for_different_hue()
        {
            var a = new HslColor(180f, 50f, 75f, 255);
            var b = new HslColor(181f, 50f, 75f, 255);

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_should_return_false_for_different_light()
        {
            var a = new HslColor(180f, 50f, 75f, 255);
            var b = new HslColor(180f, 50f, 76f, 255);

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_should_return_false_for_different_saturation()
        {
            var a = new HslColor(180f, 50f, 75f, 255);
            var b = new HslColor(180f, 51f, 75f, 255);

            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_should_return_false_for_different_type()
        {
            var a = new HslColor(180f, 50f, 75f, 255);

            Assert.IsFalse(a.Equals("not a color"));
        }

        [TestMethod]
        public void Equals_should_return_false_for_null()
        {
            var a = new HslColor(180f, 50f, 75f, 255);

            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod]
        public void Equals_should_return_true_for_identical_values()
        {
            var a = new HslColor(180f, 50f, 75f, 255);
            var b = new HslColor(180f, 50f, 75f, 255);

            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_should_return_true_for_values_within_tolerance()
        {
            // Tolerance is 0.001f — values differing by 0.0005 must be considered equal.
            var a = new HslColor(180.0000f, 50.0000f, 75.0000f, 255);
            var b = new HslColor(180.0005f, 50.0005f, 75.0005f, 255);

            Assert.IsTrue(a.Equals(b));
        }

        // ------------------------------------------------------------------ GetHashCode

        [TestMethod]
        public void GetHashCode_should_return_same_value_for_equal_colors()
        {
            var a = new HslColor(180f, 50f, 75f, 255);
            var b = new HslColor(180f, 50f, 75f, 255);

            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_should_return_different_values_for_swapped_components()
        {
            // XOR-based hashes collide when components are swapped; multiply-add does not.
            var a = new HslColor(120f, 50f, 75f, 255);
            var b = new HslColor(50f, 120f, 75f, 255);

            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        // ------------------------------------------------------------------ ToString

        [TestMethod]
        public void ToString_should_include_all_component_names()
        {
            var color = new HslColor(180f, 50f, 75f, 255);
            var result = color.ToString();

            Assert.IsTrue(result.Contains("Hue:"));
            Assert.IsTrue(result.Contains("Saturation:"));
            Assert.IsTrue(result.Contains("Light:"));
            Assert.IsTrue(result.Contains("Alpha:"));
        }
    }
}
