using System;
using CCSWE.nanoFramework;
using nanoFramework.TestFramework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
namespace UnitTests.CCSWE.nanoFramework.Core;

[TestClass]
public class StringsTests
{
    #region EqualsIgnoreCase

    [TestMethod]
    public void EqualsIgnoreCase_should_return_false_when_a_is_null()
    {
        Assert.IsFalse(Strings.EqualsIgnoreCase(null, "hello"));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_false_when_b_is_null()
    {
        Assert.IsFalse(Strings.EqualsIgnoreCase("hello", null));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_false_when_different_content()
    {
        Assert.IsFalse(Strings.EqualsIgnoreCase("hello", "world"));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_false_when_different_length()
    {
        Assert.IsFalse(Strings.EqualsIgnoreCase("hello", "hello world"));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_true_when_both_empty()
    {
        Assert.IsTrue(Strings.EqualsIgnoreCase(string.Empty, string.Empty));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_true_when_both_null()
    {
        Assert.IsTrue(Strings.EqualsIgnoreCase(null, null));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_true_when_equal_lowercase()
    {
        Assert.IsTrue(Strings.EqualsIgnoreCase("hello world", "hello world"));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_true_when_equal_mixed_case()
    {
        Assert.IsTrue(Strings.EqualsIgnoreCase("hello world", "Hello World"));
    }

    [TestMethod]
    public void EqualsIgnoreCase_should_return_true_when_equal_uppercase()
    {
        Assert.IsTrue(Strings.EqualsIgnoreCase("hello world", "HELLO WORLD"));
    }

    #endregion

    #region Join

    [TestMethod]
    public void Join_should_return_empty_string_when_values_is_empty()
    {
        Assert.AreEqual(string.Empty, Strings.Join(","));
    }

    [TestMethod]
    public void Join_should_return_joined_string_with_empty_separator()
    {
        Assert.AreEqual("abc", Strings.Join(string.Empty, "a", "b", "c"));
    }

    [TestMethod]
    public void Join_should_return_joined_string_with_null_separator()
    {
        Assert.AreEqual("abc", Strings.Join(null, "a", "b", "c"));
    }

    [TestMethod]
    public void Join_should_return_joined_string_with_separator()
    {
        Assert.AreEqual("a,b,c", Strings.Join(",", "a", "b", "c"));
    }

    [TestMethod]
    public void Join_should_return_single_value_when_values_has_one_element()
    {
        Assert.AreEqual("hello", Strings.Join(",", "hello"));
    }

    [TestMethod]
    public void Join_should_throw_if_values_is_null()
    {
        Assert.ThrowsException(typeof(ArgumentNullException), () => Strings.Join(string.Empty, null));
    }

    #endregion
}
