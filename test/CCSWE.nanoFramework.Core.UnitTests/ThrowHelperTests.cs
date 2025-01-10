using System;
using nanoFramework.TestFramework;

namespace CCSWE.nanoFramework.Core.UnitTests
{
    [TestClass]
    public class ThrowHelperTests
    {
        [TestMethod]
        public void ThrowIfNull_throws_ArgumentNullException_when_argument_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => ThrowHelper.ThrowIfNull(null));

        }

        [TestMethod]
        public void ThrowIfNull_throws_ArgumentNullException_with_correct_ParamName_when_argument_is_null()
        {
            object unitTestParameter = null;
            const string expect = nameof(unitTestParameter);
            var testExecuted = false;

            try
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                ThrowHelper.ThrowIfNull(unitTestParameter);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(expect, ex.ParamName);
                testExecuted = true;
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Assert.IsTrue(testExecuted);
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_throws_ArgumentException_when_argument_is_empty()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => ThrowHelper.ThrowIfNullOrEmpty(string.Empty));

        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_throws_ArgumentExceptionException_with_correct_ParamName_when_argument_is_empty()
        {
            var unitTestParameter = string.Empty;
            const string expect = nameof(unitTestParameter);
            var testExecuted = false;

            try
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                ThrowHelper.ThrowIfNullOrEmpty(unitTestParameter);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(expect, ex.ParamName);
                testExecuted = true;
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Assert.IsTrue(testExecuted);
        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_throws_ArgumentNullException_when_argument_is_null()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => ThrowHelper.ThrowIfNullOrEmpty(null));

        }

        [TestMethod]
        public void ThrowIfNullOrEmpty_throws_ArgumentNullException_with_correct_ParamName_when_argument_is_null()
        {
            string unitTestParameter = null;
            const string expect = nameof(unitTestParameter);
            var testExecuted = false;

            try
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                ThrowHelper.ThrowIfNullOrEmpty(unitTestParameter);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual(expect, ex.ParamName);
                testExecuted = true;
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            Assert.IsTrue(testExecuted);
        }

    }
}
