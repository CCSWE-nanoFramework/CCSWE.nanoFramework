using System;

namespace CCSWE.nanoFramework.Text
{
    /// <summary>
    /// Helper methods to parse <see cref="TimeSpan"/>s.
    /// </summary>
    public static class TimeSpanConverter
    {
        /// <summary>
        /// Convert a string value to a <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="value"><see cref="string"/> to convert.</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        /// <remarks>This was improved from https://github.com/nanoframework/nanoFramework.Json/blob/16a7727c389007faaddd22eb80034a7f39b4ce5f/nanoFramework.Json/Converters/TimeSpanConverter.cs#L33</remarks>
        public static TimeSpan FromString(string value)
        {
            Ensure.IsNotNull(value);

            var tokens = value.Split(':', '.');
            if (tokens.Length == 0)
            {
                return TimeSpan.Zero;
            }

            var indexOfFirstDot = value.IndexOf('.');
            var indexOfSecondDot = indexOfFirstDot > -1 ? value.IndexOf('.', indexOfFirstDot + 1) : -1;
            var indexOfFirstColon = value.IndexOf(':');
            var indexOfSecondColon = indexOfFirstColon > -1 ? value.IndexOf(':', indexOfFirstColon + 1) : -1;

            var isNegative = value.StartsWith("-");

            var hasDays = indexOfFirstDot > 0 && indexOfFirstDot < indexOfFirstColon || tokens.Length == 1;
            var hasTicks = hasDays ? indexOfSecondDot > indexOfFirstDot : indexOfFirstDot > -1;
            var hasHours = indexOfFirstColon > 0;
            var hasMinutes = hasHours && indexOfFirstColon > -1;
            var hasSeconds = hasMinutes && indexOfSecondColon > -1;

            if (hasTicks && !hasHours)
            {
                throw new InvalidCastException();
            }

            var tokenIndex = 0;

            var days = ParseToken(hasDays, tokens, ref tokenIndex);
            var hours = ParseToken(hasHours, tokens, ref tokenIndex);
            var minutes = ParseToken(hasMinutes, tokens, ref tokenIndex);
            var seconds = ParseToken(hasSeconds, tokens, ref tokenIndex);
            var ticks = ParseTicks(hasTicks, tokens, tokenIndex);

            if (IsInvalidTimeSpan(hours, minutes, seconds))
            {
                throw new InvalidCastException();
            }

            var timeSpan = new TimeSpan(days, hours, minutes, seconds, 0).Add(new TimeSpan(ticks));

            return isNegative ? -timeSpan : timeSpan;
        }

        private static bool IsInvalidTimeSpan(int hour, int minutes, int seconds)
        {
            if (hour is < 0 or > 24)
            {
                return true;
            }

            if (minutes is < 0 or >= 60)
            {
                return true;
            }

            if (seconds is < 0 or >= 60)
            {
                return true;
            }

            return false;
        }

        private static int ParseTicks(bool hasTicks, string[] tokens, int tokenIndex)
        {
            if (!hasTicks || tokenIndex > tokens.Length)
            {
                return 0;
            }

            var token = tokens[tokenIndex];

            if (token.Length > 7)
            {
                token = token.Substring(0, 7);
            }

            if (!int.TryParse(token, out var value))
            {
                throw new InvalidCastException();
            }

            value = token.Length switch
            {
                1 => value * 1_000_000,
                2 => value * 100_000,
                3 => value * 10_000,
                4 => value * 1_000,
                5 => value * 100,
                6 => value * 10,
                _ => value
            };

            return value >= 0 ? value : value * -1;
        }

        private static int ParseToken(bool hasValue, string[] tokens, ref int tokenIndex)
        {
            if (!hasValue)
            {
                return 0;
            }

            if (tokenIndex > tokens.Length)
            {
                return 0;
            }

            if (!int.TryParse(tokens[tokenIndex++], out var value))
            {
                throw new InvalidCastException();
            }

            return value >= 0 ? value : value * -1;
        }

        /// <summary>
        /// Converts the value of the <see cref="TimeSpan"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the <see cref="TimeSpan"/> value.</returns>
        /// <remarks>The returned string is formatted with the "c" format specifier and has the following format: [-][d.]hh:mm:ss[.fffffff]</remarks>
        public static string ToString(TimeSpan value)
        {
            Ensure.IsNotNull(value);

            return value.ToString();
        }
    }
}
