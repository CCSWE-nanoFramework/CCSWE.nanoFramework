using System.IO;

namespace CCSWE.nanoFramework.FileStorage
{
    /// <summary>
    /// Provides access to a file system.
    /// </summary>
    internal class FileStorage: IFileStorage
    {
        /// <inheritdoc />
        public FileStream Create(string path) => File.Create(path);

        /// <inheritdoc />
        public void Delete(string path) => File.Delete(path);

        /// <inheritdoc />
        public bool Exists(string path) => File.Exists(path);

        /// <inheritdoc />
        public string[] GetDirectories(string path) => Directory.GetDirectories(path);

        /// <inheritdoc />
        public string[] GetFiles(string path) => Directory.GetFiles(path);

        /// <inheritdoc />
        public FileStream OpenRead(string path) => File.OpenRead(path);

        /// <inheritdoc />
        public StreamReader OpenText(string path) => File.OpenText(path);

        /// <inheritdoc />
        public FileStream OpenWrite(string path) => File.OpenWrite(path);

        /// <inheritdoc />
        public byte[] ReadAllBytes(string path) => File.ReadAllBytes(path);

        /// <inheritdoc />
        public string ReadAllText(string path) => File.ReadAllText(path);

        /// <inheritdoc />
        public void WriteAllBytes(string path, byte[] bytes) => File.WriteAllBytes(path, bytes);

        /// <inheritdoc />
        public void WriteAllText(string path, string? contents) => File.WriteAllText(path, contents);
    }
}
