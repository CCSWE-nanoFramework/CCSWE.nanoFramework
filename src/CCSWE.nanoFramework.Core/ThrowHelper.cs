using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace CCSWE.nanoFramework
{
    /// <summary>
    /// Helper methods for <see cref="ArgumentException"/> and <see cref="ArgumentNullException"/>
    /// </summary>
    public static class ThrowHelper
    {
        /// <summary>Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.</summary>
        /// <param name="argument">The reference type argument to validate as non-null.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
        public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (argument is null)
            {
                ThrowArgumentNullException(paramName);
            }
        }

        /// <summary>Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null or an <see cref="ArgumentException"/> if <paramref name="argument"/> is an empty string.</summary>
        /// <param name="argument">The reference type argument to validate as non-null or empty.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
        public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
        {
            ThrowIfNull(argument, paramName);

            if (Strings.IsNullOrEmpty(argument))
            {
                ThrowArgumentException($"'{paramName}' is empty.", paramName);
            }
        }

        [DoesNotReturn]
        private static void ThrowArgumentException(string? message, string? paramName) => throw new ArgumentException(message, paramName);

        [DoesNotReturn]
        private static void ThrowArgumentNullException(string? paramName) => throw new ArgumentNullException(paramName);
    }
}
