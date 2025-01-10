using System;
using System.Runtime.CompilerServices;

namespace CCSWE.nanoFramework
{
    public static partial class Ensure
    {
        private const string ErrorValueIsOutOfRange = "The value passed for '{0}' is out of range.";
        private const string ErrorValueMustBeBetween = "The value passed for '{0}' must be between {1} and {2} (inclusive).";

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the expression evaluates to <c>false</c>.
        /// </summary>
        /// <param name="expression">The expression that will be evaluated.</param>
        /// <param name="message">The message associated with the <see cref="ArgumentOutOfRangeException"/></param>
        /// <param name="paramName">The name of the parameter we are validating.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the expression evaluates to <c>false</c></exception>.
        public static void IsInRange(bool expression, string? message = null, [CallerArgumentExpression(nameof(expression))] string paramName = null!)
        {
            if (expression)
            {
                return;
            }

            throw GetException(typeof(ArgumentOutOfRangeException), paramName, string.IsNullOrEmpty(message) ? string.Format(ErrorValueIsOutOfRange, paramName) : message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the expression evaluates to <c>false</c>.
        /// </summary>
        /// <param name="value">The value that will be evaluated.</param>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        /// <param name="message">The message associated with the <see cref="ArgumentOutOfRangeException"/></param>
        /// <param name="paramName">The name of the parameter we are validating.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the expression evaluates to <c>false</c></exception>.
        public static void IsInRange(double value, double min, double max, string? message = null, [CallerArgumentExpression(nameof(value))] string paramName = null!)
        {
            if (value >= min && value <= max)
            {
                return;
            }

            throw GetException(typeof(ArgumentOutOfRangeException), paramName, string.IsNullOrEmpty(message) ? string.Format(ErrorValueMustBeBetween, paramName, min, max) : message);
        }

        /// <inheritdoc cref="IsInRange(double,double,double,string?,string)"/>
        public static void IsInRange(float value, float min, float max, string? message = null, [CallerArgumentExpression(nameof(value))] string paramName = null!)
        {
            if (value >= min && value <= max)
            {
                return;
            }

            throw GetException(typeof(ArgumentOutOfRangeException), paramName, string.IsNullOrEmpty(message) ? string.Format(ErrorValueMustBeBetween, paramName, min, max) : message);
        }

        /// <inheritdoc cref="IsInRange(double,double,double,string?,string)"/>
        public static void IsInRange(int value, int min, int max, string? message = null, [CallerArgumentExpression(nameof(value))] string paramName = null!)
        {
            if (value >= min && value <= max)
            {
                return;
            }

            throw GetException(typeof(ArgumentOutOfRangeException), paramName, string.IsNullOrEmpty(message) ? string.Format(ErrorValueMustBeBetween, paramName, min, max) : message);
        }

        /// <inheritdoc cref="IsInRange(double,double,double,string?,string)"/>
        public static void IsInRange(long value, long min, long max, string? message = null, [CallerArgumentExpression(nameof(value))] string paramName = null!)
        {
            if (value >= min && value <= max)
            {
                return;
            }

            throw GetException(typeof(ArgumentOutOfRangeException), paramName, string.IsNullOrEmpty(message) ? string.Format(ErrorValueMustBeBetween, paramName, min, max) : message);
        }
    }
}
