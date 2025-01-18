using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace CCSWE.nanoFramework.IO
{
    /// <summary>
    /// Extensions for <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        private const string StreamDoesNotSupportReading = "Stream does not support reading.";

        /// <summary>
        /// Reads all bytes from the specified stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A byte array containing the data read from the stream.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the stream does not support reading.</exception>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);

            if (!stream.CanRead)
            {
                throw new ArgumentException(StreamDoesNotSupportReading, nameof(stream));
            }

            if (stream is MemoryStream memoryStream)
            {
                return memoryStream.ToArray();
            }

            var bytes = new byte[stream.Length];
            var buffer = new byte[4096];

            var position = 0;

            while (true)
            {
                //Thread.Sleep(1);

                var count = stream.Length;

                if (count > buffer.Length)
                {
                    count = buffer.Length;
                }

                var bytesRead = stream.Read(buffer, 0, (int)count);

                if (bytesRead == 0)
                {
                    break;
                }

                Array.Copy(buffer, 0, bytes, position, bytesRead);

                position += bytesRead;
            }

            return bytes;
        }

        /// <summary>
        /// Reads the stream and converts it to a UTF8 string.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A string containing the data read from the stream.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the stream is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the stream does not support reading.</exception>
        public static string ReadAsString(this Stream stream)
        {
            // TODO: Add GetString(string) overload to Encoding.UTF8 in nanoFramework
            var bytes = stream.ReadAllBytes();
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }
    }
}
