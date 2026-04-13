using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CCSWE.nanoFramework;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class Strings
{
    /// <summary>
    /// Compares two strings for equality, ignoring case.
    /// </summary>
    /// <param name="a">The first string to compare.</param>
    /// <param name="b">The second string to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the strings are equal, ignoring case; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Case folding is limited to ASCII letters (A–Z / a–z). Non-ASCII characters are compared as-is.
    /// If both strings are <see langword="null"/>, the method returns <see langword="true"/>.
    /// If only one of the strings is <see langword="null"/>, the method returns <see langword="false"/>.
    /// </remarks>
    public static bool EqualsIgnoreCase(string? a, string? b)
    {
        if (a is null || b is null)
        {
            return a is null && b is null;
        }
        
        return a.Length == b.Length && string.Equals(a.ToUpper(), b.ToUpper());
    }
        
    /// <summary>
    /// Indicates whether the specified string is <see langword="null"/> or an empty string ("").
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><see langword="true"/> if the value parameter is <see langword="null"/> or an empty string (""); otherwise, <see langword="false"/>.</returns>
    /// <remarks>This only exists in order to apply the <see cref="NotNullWhenAttribute"/> to <paramref name="value"/>.</remarks>
    [Obsolete("string.IsNullOrEmpty is properly attributed now so this is no longer needed")]
    public static bool IsNullOrEmpty([NotNullWhen(false)] string? value) => string.IsNullOrEmpty(value);

    /* TODO: Come back to this
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] string? value)
    {
        if (value == null) return true;

        for (int i = 0; i < value.Length; i++)
        {
            if (!char.IsWhiteSpace(value[i])) return false;
        }

        return true;
    }
    */

    /// <summary>
    /// Concatenates all the elements of a string array, using the specified separator between each element.
    /// </summary>
    /// <param name="separator">The string to use as a separator. <paramref name="separator"/> is included in the returned string only if <paramref name="value"/> has more than one element.</param>
    /// <param name="value">An array that contains the elements to concatenate.</param>
    /// <returns>
    /// A string that consists of the elements in <paramref name="value"/> delimited by the <paramref name="separator"/> string.
    /// 
    /// -or-
    /// 
    /// <see cref="string.Empty"/> if values has zero elements.
    /// </returns>
    public static string Join(string? separator, params string[]? value)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value.Length)
        {
            case 0:
                return string.Empty;
            case 1:
                return value[0];
        }

        var join = new StringBuilder();
        var length = value.Length;

        for (var i = 0; i < length; i++)
        {
            join.Append(value[i]);

            if (i < length - 1)
            {
                join.Append(separator);
            }
        }

        return join.ToString();
    }
}