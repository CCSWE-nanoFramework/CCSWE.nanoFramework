using System;
using CCSWE.nanoFramework.Text;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Core.UnitTests.Text
{
    [TestClass]
    public class TimeSpanConverterTests
    {
        [TestMethod]
        public void FromString_parses_string()
        {
            var values = new[]
            {
                "-1.02:03:04.005",
                "1.02:03:04.0050000",
                "4.03:02:01.654321",
                "4.03:02:01.65432",
                "4.03:02:01.6543",
                "4.03:02:01.654",
                "4.03:02:01.65",
                "4.03:02:01.6",
                "04:20:19",
                "07:32",
                "-01:23",
                "+03:21"
            };

            var expected = new[]
            {
                -new TimeSpan(1, 2, 3, 4, 5),
                new TimeSpan(1, 2, 3, 4, 5),
                new TimeSpan(4, 3, 2, 1, 654).Add(new TimeSpan(3210)),
                new TimeSpan(4, 3, 2, 1, 654).Add(new TimeSpan(3200)),
                new TimeSpan(4, 3, 2, 1, 654).Add(new TimeSpan(3000)),
                new TimeSpan(4, 3, 2, 1, 654),
                new TimeSpan(4, 3, 2, 1, 650),
                new TimeSpan(4, 3, 2, 1, 600),
                new TimeSpan(4, 20, 19),
                new TimeSpan(7, 32, 0),
                -new TimeSpan(1, 23, 0),
                new TimeSpan(3, 21, 0)
            };

            for (var i = 0; i < values.Length; i++)
            {
                var actual = TimeSpanConverter.FromString(values[i]);

                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void ToString_returns_valid_string()
        {
            var values = new[]
            {
                -new TimeSpan(1, 2, 3, 4, 5),
                new TimeSpan(1, 2, 3, 4, 5),
                new TimeSpan(4, 3, 2, 1, 654).Add(new TimeSpan(3210)),
                new TimeSpan(4, 20, 19),
                new TimeSpan(7, 32, 0),
                new TimeSpan(0, 29, 0),
            };

            var expected = new[]
            {
                "-1.02:03:04.0050000",
                "1.02:03:04.0050000",
                "4.03:02:01.6543210",
                "04:20:19",
                "07:32:00",
                "00:29:00",
            };

            for (var i = 0; i < values.Length; i++)
            {
                var actual = TimeSpanConverter.ToString(values[i]);

                Assert.AreEqual(expected[i], actual);
            }
        }
    }
}
