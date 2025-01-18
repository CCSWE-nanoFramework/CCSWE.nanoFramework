using CCSWE.nanoFramework.IO;

namespace CCSWE.nanoFramework.Net
{
    /// <summary>
    /// Common mime-types.
    /// </summary>
    /// <remarks>
    /// Using https://github.com/markwhitaker/MimeTypes.NET for reference.
    /// </remarks>
    // TODO: Move to another library?
    public static class MimeType
    {
        /// <summary>
        /// application/* mime-types.
        /// </summary>
        public static class Application
        {
            /// <summary>
            /// application/json
            /// </summary>
            public const string Json = "application/json";

            /// <summary>
            /// application/octet-stream
            /// </summary>
            public const string Octet = "application/octet-stream";

            /// <summary>
            /// application/xml
            /// </summary>
            public const string Xml = "application/xml";

            /// <summary>
            /// application/zip
            /// </summary>
            public const string Zip = "application/zip";
        }

        /// <summary>
        /// image/* mime-types.
        /// </summary>
        public static class Image
        {
            /// <summary>
            /// image/gif
            /// </summary>
            public const string Gif = "image/gif";

            /// <summary>
            /// image/jpeg
            /// </summary>
            public const string Jpeg = "image/jpeg";

            /// <summary>
            /// image/png
            /// </summary>
            public const string Png = "image/png";
        }

        /// <summary>
        /// text/* mime-types.
        /// </summary>
        public static class Text
        {
            /// <summary>
            /// text/html
            /// </summary>
            public const string Html = "text/html";

            /// <summary>
            /// text/javascript
            /// </summary>
            public const string JavaScript = "text/javascript";

            /// <summary>
            /// text/plain
            /// </summary>
            public const string Plain = "text/plain";
        }

        /// <summary>
        /// Attempts to map a file name to a mime-type based on the extension.
        /// </summary>
        /// <param name="fileName">The file name to map.</param>
        /// <returns>A <see cref="MimeType"/> if a mapping exists; otherwise, <see cref="Application.Octet"/></returns>
        public static string GetMimeTypeFromFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();

            return extension switch
            {
                ".gif" => Image.Gif,
                ".html" => Text.Html,
                ".jpeg" => Image.Jpeg,
                ".jpg" => Image.Jpeg,
                ".js" => Text.JavaScript,
                ".json" => Application.Json,
                ".png" => Image.Png,
                ".txt" => Text.Plain,
                ".xml" => Application.Xml,
                ".zip" => Application.Zip,
                _ => Application.Octet
            };
        }
    }
}
