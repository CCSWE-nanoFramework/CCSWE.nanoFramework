using System.Diagnostics.CodeAnalysis;

namespace CCSWE.nanoFramework.WebServer.StaticFiles
{
    /// <summary>
    /// Used to look up MIME types given a file path.
    /// </summary>
    public interface IContentTypeProvider
    {
        /// <summary>
        /// Given a file path, determine the MIME type.
        /// </summary>
        /// <param name="subpath">A file path.</param>
        /// <param name="contentType">The resulting MIME type.</param>
        /// <returns><see langword="true"/> if MIME type could be determined; otherwise, <see langword="false"/>.</returns>
        bool TryGetContentType(string subpath, [MaybeNullWhen(false)] out string contentType);
    }
}
