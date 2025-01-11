using System;
using System.Drawing;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.NeoPixel.UnitTests
{
    [TestClass]
    public class ColorConverterTests
    {
        [TestMethod]
        public void ScaleBrightness_should_return_correct_color()
        {
            var tests = new[]
            {
                Color.White,
                Color.FromHex("#26C467"),
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.DarkOrchid,
                Color.Orange,
                Color.DeepPink,
                Color.DarkCyan
            };

            var expectations = new[]
            {
                new[] { 127, 127, 127},
                new[] { 24, 127, 67 },
                new[] { 127, 0, 0 },
                new[] { 0, 127, 0 },
                new[] { 0, 0, 127 },
                new[] { 127, 127, 0 },
                new[] { 95, 31, 127 },
                new[] { 127, 82, 0 },
                new[] { 127, 9, 73 },
                new[] { 0, 127, 127 },
            };

            for (var i = 0; i < tests.Length; i++)
            {
                var test = tests[i];
                var expected = expectations[i];

                var actual = ColorConverter.ScaleBrightness(test, 0.5f);

                //Console.WriteLine($"ScaleBrightness_should_return_correct_color: {test}");
                Assert.AreEqual(expected[0], actual.R);
                Assert.AreEqual(expected[1], actual.G);
                Assert.AreEqual(expected[2], actual.B);
            }
        }

        [TestMethod]
        public void ToHsbColor_should_return_correct_color()
        {
            var tests = new[]
            {
                Color.White,
                Color.Black,
                Color.FromHex("#26C467"),
                Color.Red, 
                Color.Green, 
                Color.Blue, 
                Color.Yellow, 
                Color.DarkOrchid, 
                Color.Orange, 
                Color.DeepPink, 
                Color.DarkCyan
            };

            var expectations = new[]
            {
                new[] { 0, 0, 100 },
                new[] { 0, 0, 0 },
                new[] { 144, 80, 76 },
                new[] { 0, 100, 100 },
                new[] { 120, 100, 50 },
                new[] { 240, 100, 100 },
                new[] { 60, 100, 100 },
                new[] { 280, 75, 80 },
                new[] { 38, 100, 100 },
                new[] { 327, 92, 100 },
                new[] { 180, 100, 54 },
            };

            for (var i = 0; i < tests.Length; i++)
            {
                var test = tests[i];
                var expected = expectations[i];

                var actual = ColorConverter.ToHsbColor(test);

                //Console.WriteLine($"ToHsbColor_should_return_correct_color: {test}");
                Assert.AreEqual(expected[0], (int)actual.Hue);
                Assert.AreEqual(expected[1], (int)actual.Saturation);
                Assert.AreEqual(expected[2], (int)actual.Brightness);
            }
        }

        [TestMethod]
        public void ToHslColor_should_return_correct_color()
        {
            var tests = new[]
            {
                Color.White,
                Color.Black,
                Color.FromHex("#26C467"),
                Color.Red, 
                Color.Green, 
                Color.Blue, 
                Color.Yellow, 
                Color.DarkOrchid, 
                Color.Orange, 
                Color.DeepPink, 
                Color.DarkCyan
            };


            var expectations = new[]
            {
                new[] { 0, 0, 100 },
                new[] { 0, 0, 0 },
                new[] { 144, 67, 45 },
                new[] { 0, 100, 50 },
                new[] { 120, 100, 25 },
                new[] { 240, 100, 50 },
                new[] { 60, 100, 50 },
                new[] { 280, 60, 49 },
                new[] { 38, 100, 50 },
                new[] { 327, 100, 53 },
                new[] { 180, 100, 27 },
            };

            for (var i = 0; i < tests.Length; i++)
            {
                var test = tests[i];
                var expected = expectations[i];

                var actual = ColorConverter.ToHslColor(test);

                //Console.WriteLine($"ToHslColor_should_return_correct_color: {test}");
                Assert.AreEqual(expected[0], (int)actual.Hue);
                Assert.AreEqual(expected[1], (int)actual.Saturation);
                Assert.AreEqual(expected[2], (int)actual.Light);
            }
        }
    }
}
