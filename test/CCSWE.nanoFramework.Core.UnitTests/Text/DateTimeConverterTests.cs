using System;
using CCSWE.nanoFramework.Text;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Core.UnitTests.Text
{
    [TestClass]
    public class DateTimeConverterTests
    {
        [TestMethod]
        public void FromString_parses_string()
        {
            // TODO: Add offset strings
            var values = new[]
            {
                "2024-05-15T04:20:05Z",
                "2024-05-15T04:20:05.6Z",
                "2024-05-15T04:20:05.06Z",
                "2024-05-15T04:20:05.069Z",
                "2024-05-15T04:20:05.8342Z",
                "2024-05-15T04:20:05.26323Z",
                "2024-05-15T04:20:05.045234Z",
                "2024-05-15T04:20:05.0012345Z",
                "2024-05-15T04:20:05.03423456Z",
                "2024-05-15T04:20:05.069-01:05",
                "2024-05-15T04:20:05.069+01:05",
            };

            var expectations = new[]
            {
                CreateDateTime(2024, 5, 15, 4, 20, 5),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 600),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 60),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 69),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 834, 2000),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 263, 2300),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 45, 2340),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 1, 2345),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 34, 2345),
                CreateDateTime(2024, 5, 15, 5, 25, 5, 69),
                CreateDateTime(2024, 5, 15, 3, 15, 5, 69),
            };

            for (var i = 0; i < values.Length; i++)
            {
                var expect = expectations[i];
                var actual = DateTimeConverter.FromString(values[i]);

                Console.WriteLine($"[{i}] expect: {expect.Ticks}");
                Console.WriteLine($"    actual: {actual.Ticks}");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestMethod]
        public void ToString_returns_valid_string()
        {
            var values = new[]
            {
                CreateDateTime(2024, 5, 15, 4, 20, 5),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 600),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 60),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 69),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 834, 2000),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 263, 2300),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 45, 2340),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 1, 2345),
                CreateDateTime(2024, 5, 15, 4, 20, 5, 34, 2345),
            };

            var expectations = new[]
            {
                "2024-05-15T04:20:05.0000000Z",
                "2024-05-15T04:20:05.6000000Z",
                "2024-05-15T04:20:05.0600000Z",
                "2024-05-15T04:20:05.0690000Z",
                "2024-05-15T04:20:05.8342000Z",
                "2024-05-15T04:20:05.2632300Z",
                "2024-05-15T04:20:05.0452340Z",
                "2024-05-15T04:20:05.0012345Z",
                "2024-05-15T04:20:05.0342345Z",
            };

            for (var i = 0; i < values.Length; i++)
            {
                var expect = expectations[i];
                var actual = DateTimeConverter.ToString(values[i]);

                Assert.AreEqual(expect, actual);
            }
        }

        private static DateTime CreateDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond = 0, int extraTicks = 0)
        {
            var dateTime = new DateTime(year, month, day, hour, minute, second, millisecond);

            if (extraTicks > 0)
            {
                dateTime = dateTime.AddTicks(extraTicks);
            }

            return dateTime;
        }
    }
}
