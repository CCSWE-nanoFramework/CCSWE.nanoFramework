using System;

namespace CCSWE.nanoFramework.Text
{
    /// <summary>
    /// Common string formatting patterns.
    /// </summary>
    public static class StringFormatter
    {
        internal const long Kilobyte = 1024L;
        internal const long Megabyte = 1024L * 1024;
        internal const long Gigabyte = 1024L * 1024 * 1024;
        internal const long Terabyte = 1024L * 1024 * 1024 * 1024;

        /// <summary>
        /// Formats a value to a simple file size string (ie: 1.25 MB).
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A formatted string.</returns>
        public static string ToFileSizeString(long value)
        {
            const string formatString = "F2";

            var absoluteFileSize = value > 0 ? value : value * -1;

            return absoluteFileSize switch
            {
                > Terabyte => $"{(value / (double)Terabyte).ToString(formatString)} TB",
                > Gigabyte => $"{(value / (double)Gigabyte).ToString(formatString)} GB",
                > Megabyte => $"{(value / (double)Megabyte).ToString(formatString)} MB",
                > Kilobyte => $"{(value / (double)Kilobyte).ToString(formatString)} KB",
                _ => $"{((double)value).ToString(formatString)} B"
            };
        }

        /// <summary>
        /// Converts the value of the <see cref="DateTime"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the <see cref="DateTime"/> value.</returns>
        /// <remarks>The returned string is formatted following RFC 3339 using the following format: yyyy-MM-dd'T'HH:mm:ss.fffZ</remarks>
        public static string ToInternetString(DateTime value) => DateTimeConverter.ToString(value);

        /// <summary>
        /// Converts the value of the <see cref="TimeSpan"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the <see cref="TimeSpan"/> value.</returns>
        /// <remarks>The returned string is formatted with the "c" format specifier and has the following format: [-][d.]hh:mm:ss[.fffffff]</remarks>
        public static string ToInternetString(TimeSpan value) => TimeSpanConverter.ToString(value);
    }
}
