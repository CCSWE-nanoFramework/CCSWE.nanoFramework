using System;
using CCSWE.nanoFramework;
using nanoFramework.TestFramework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
namespace UnitTests.CCSWE.nanoFramework.Core;
[TestClass]
public class StringsTest
{
    [TestMethod]
    public void Join_should_throw_if_values_is_null()
    {
        Assert.ThrowsException(typeof(ArgumentNullException), () => Strings.Join(string.Empty, null));
    }
}
