using System.Diagnostics.CodeAnalysis;

namespace CCSWE.nanoFramework.IO
{
    /// <summary>
    /// A small subset of <c>System.IO.Path</c> for when that is not available.
    /// </summary>
    public class Path
    {
        /// <summary>
        /// Returns the extension (including the period ".") of the specified path string.
        /// </summary>
        /// <param name="path">The path string from which to get the extension.</param>
        /// <returns>
        /// The extension of the specified path (including the period "."), or <see langword="null"/>, or <see cref="string.Empty"/>.
        /// If <paramref name="path"/> is <see langword="null"/>, <see cref="GetExtension(string)"/> returns <see langword="null"/>.
        /// If path does not have extension information, <see cref="GetExtension(string)"/> returns <see cref="string.Empty"/>.</returns>
        [return: NotNullIfNotNull("path")]
        public static string? GetExtension(string? path)
        {
            if (path is null)
            {
                return null;
            }

            var length = path.Length;

            for (var i = length - 1; i >= 0; i--)
            {
                var ch = path[i];

                if (ch == '.')
                {
                    if (i != length - 1)
                    {
                        return path.Substring(i, length - i);
                    }

                    return string.Empty;
                }

                if (ch is '\\' or '/')
                {
                    break;
                }
            }

            return string.Empty;
        }
    }
}
