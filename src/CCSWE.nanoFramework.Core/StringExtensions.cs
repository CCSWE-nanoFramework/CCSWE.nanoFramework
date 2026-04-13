namespace CCSWE.nanoFramework;

/// <summary>
/// Provides extension methods for the <see cref="string"/> type.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Determines whether this string and a specified string have the same value, optionally ignoring case.
    /// </summary>
    /// <param name="value">The string to compare.</param>
    /// <param name="other">The string to compare against.</param>
    /// <param name="ignoreCase"><see langword="true"/> to perform a case-insensitive comparison; <see langword="false"/> for an ordinal comparison.</param>
    /// <returns>
    /// <see langword="true"/> if the strings are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool Equals(this string value, string? other, bool ignoreCase)
    {
        return !ignoreCase ? value.Equals(other) : Strings.EqualsIgnoreCase(value, other);
    }

    /// <summary>
    /// Truncates a string to a specified maximum length, appending an ellipsis if truncation occurs.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="maxLength">The maximum length of the resulting string, including the ellipsis if added.</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
        {
            return value;
        }

        if (maxLength <= 3)
        {
            return value.Substring(0, maxLength);
        }

        return value.Substring(0, maxLength - 3) + "...";
    }
}