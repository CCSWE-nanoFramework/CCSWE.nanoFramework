using System.Diagnostics.CodeAnalysis;
using CCSWE.nanoFramework.Net;

namespace CCSWE.nanoFramework.WebServer.StaticFiles
{
    /// <summary>
    /// Used to look up MIME types given a file path via <see cref="MimeType.GetMimeTypeFromFileName"/>.
    /// </summary>
    /// <remarks><c>application/octet-stream</c> will be returned if a specific MIME type cannot be determined.</remarks>
    public class ContentTypeProvider: IContentTypeProvider
    {
        /// <inheritdoc />
        /// <remarks><c>application/octet-stream</c> will be returned if a specific MIME type cannot be determined.</remarks>
        public bool TryGetContentType(string subpath, [MaybeNullWhen(false)] out string contentType)
        {
            contentType = MimeType.GetMimeTypeFromFileName(subpath);

            return true;
        }
    }
}
