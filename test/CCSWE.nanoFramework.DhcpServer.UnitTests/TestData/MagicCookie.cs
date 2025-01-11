using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.DhcpServer.UnitTests.TestData
{
    internal static class MagicCookie
    {
        public static readonly byte[] Expected = { 99, 130, 83, 99 };
        public const int Length = 4;

        public static void AssertValidCookie(byte[] actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(Length, actual!.Length);

            for (var i = 0; i < Length; i++)
            {
                Assert.AreEqual(Expected[i], actual[i]);
            }
        }
    }
}
