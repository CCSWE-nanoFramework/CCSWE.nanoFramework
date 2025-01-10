using System;

namespace CCSWE.nanoFramework.Text
{
    /// <summary>
    /// Helper methods to parse <see cref="DateTime"/>s.
    /// </summary>
    public static class DateTimeConverter
    {
        private const int FractionalSecondLength = 7;
        // Correct format is "yyyy-MM-dd'T'HH:mm:ss.fffK" but nanoFramework assumes all dates are UTC and doesn't support K 
        private const string Rfc3339Format = "yyyy-MM-dd'T'HH:mm:ss.fffffffZ"; //"yyyy-MM-dd'T'HH:mm:ss.fffZ";
        private const int Rfc3339OffsetStartIndex = 19;

        /// <summary>
        /// Convert a string value to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value"><see cref="string"/> to convert.</param>
        /// <returns>A <see cref="DateTime"/>.</returns>
        /// <remarks>The <paramref name="value"/> must be formatted following RFC 3339 guidelines.</remarks>
        public static DateTime FromString(string value)
        {
            Ensure.IsNotNullOrEmpty(value);

            var offset = TimeSpan.Zero;

            var indexOfOffset = value.LastIndexOfAny(['-', '+', 'Z']);
            if (indexOfOffset >= Rfc3339OffsetStartIndex)
            {
                var offsetString = value.Substring(indexOfOffset);
                if (offsetString.StartsWith("+") || offsetString.StartsWith("-"))
                {
                    offset = TimeSpanConverter.FromString(offsetString);
                }
                value = value.Substring(0, indexOfOffset);
            }

            var indexOfLastDot = value.LastIndexOf('.');
            if (indexOfLastDot != -1)
            {
                var dateTimePart = value.Substring(0, indexOfLastDot);
                var fractionalSecondsPart = value.Substring(indexOfLastDot + 1);

                if (fractionalSecondsPart.Length > FractionalSecondLength)
                {
                    fractionalSecondsPart = fractionalSecondsPart.Substring(0, FractionalSecondLength);
                }

                if (fractionalSecondsPart.Length < FractionalSecondLength)
                {
                    var padding = FractionalSecondLength - fractionalSecondsPart.Length;
                    fractionalSecondsPart = $"{fractionalSecondsPart}{new string('0', padding)}";
                }

                value = $"{dateTimePart}.{fractionalSecondsPart}";
            }

            return DateTime.Parse(value).Subtract(offset);
        }

        /// <summary>
        /// Converts the value of the <see cref="DateTime"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the <see cref="DateTime"/> value.</returns>
        /// <remarks>The returned string is formatted following RFC 3339 using the following format: yyyy-MM-dd'T'HH:mm:ss.fffZ</remarks>
        public static string ToString(DateTime value)
        {
            Ensure.IsNotNull(value);

            return value.ToString(Rfc3339Format);
        }
    }
}
