using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCSWE.nanoFramework
{
    // Copied over from https://github.com/CoryCharlton/CCSWE.Core/blob/master/src/Core/Ensure.cs

    /// <summary>
    /// A helper class for ensuring parameter conditions.
    /// </summary>
    public static partial class Ensure
    {
        internal static Exception GetException(Type exception, string paramName, string? message = null)
        {
            if (exception == typeof(ArgumentNullException))
            {
                return new ArgumentNullException(paramName, message);
            }

            if (exception == typeof(ArgumentOutOfRangeException))
            {
                return new ArgumentOutOfRangeException(paramName, message);
            }

            if (exception == typeof(IndexOutOfRangeException))
            {
                return new IndexOutOfRangeException(message);
            }

            if (exception == typeof(InvalidOperationException))
            {
                return new InvalidOperationException(message);
            }

            // Defaulting to ArgumentException. This all worked better with generics :)
            return new ArgumentException(message, paramName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the value is <c>null</c>.
        /// </summary>
        /// <param name="paramName">The name of the parameter we are validating.</param>
        /// <param name="value">The value that will be evaluated.</param>
        /// <param name="message">The message associated with the <see cref="Exception"/></param>
        /// <exception cref="ArgumentNullException">Thrown when the value is <c>null</c></exception>.
        public static void IsNotNull([NotNull] object? value, string? message = null, [CallerArgumentExpression(nameof(value))] string paramName = null!)
        {
            if (value is not null)
            {
                return;
            }

            throw GetException(typeof(ArgumentNullException), paramName, string.IsNullOrEmpty(message) ? $"The value passed for '{paramName}' is null." : message);
        }


        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the value is <c>null</c> or empty.
        /// </summary>
        /// <param name="paramName">The name of the parameter we are validating.</param>
        /// <param name="value">The value that will be evaluated.</param>
        /// <param name="message">The message associated with the <see cref="Exception"/></param>
        /// <exception cref="ArgumentException">Thrown when the value is <c>null</c> or empty.</exception>
        public static void IsNotNullOrEmpty([NotNull] string? value, string? message = null, [CallerArgumentExpression(nameof(value))] string paramName = null!)
        {
            IsNotNull(value, message, paramName);

            if (value.Length != 0)
            {
                return;
            }

            throw GetException(typeof(ArgumentException), paramName, string.IsNullOrEmpty(message) ? $"The value passed for '{paramName}' cannot be empty." : message);
        }


        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the expression evaluates to <c>false</c>.
        /// </summary>
        /// <param name="expression">The expression that will be evaluated.</param>
        /// <param name="message">The message associated with the <see cref="Exception"/>.</param>
        /// <param name="paramName">The name of the parameter we are validating.</param>
        public static void IsValid(bool expression, string? message = null, [CallerArgumentExpression(nameof(expression))] string paramName = null!)
        {
            if (expression)
            {
                return;
            }

            throw GetException(typeof(ArgumentException), paramName, string.IsNullOrEmpty(message) ? $"The value passed for '{paramName}' is not valid." : message);
        }
    }
}
