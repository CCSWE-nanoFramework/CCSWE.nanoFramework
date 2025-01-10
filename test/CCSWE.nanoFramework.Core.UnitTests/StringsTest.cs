using System;
using nanoFramework.TestFramework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
namespace CCSWE.nanoFramework.Core.UnitTests
{
    [TestClass]
    public class StringsTest
    {
        [TestMethod]
        public void IsNullOrEmpty_returns_true()
        {
            Assert.IsTrue(Strings.IsNullOrEmpty(null));
            Assert.IsTrue(Strings.IsNullOrEmpty(string.Empty));
        }

        [TestMethod]
        public void Join_should_throw_if_values_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => Strings.Join(string.Empty, null));
        }
    }
}
