using System.IO;

namespace CCSWE.nanoFramework.FileStorage
{
    /// <summary>
    /// Provides access to a file system.
    /// </summary>
    public interface IFileStorage
    {
        /// <summary>Creates or overwrites a file in the specified path.</summary>
        /// <param name="path">The path and name of the file to create.</param>
        FileStream Create(string path);

        /// <summary>Deletes the specified file.</summary>
        /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="path" /> is <see langword="null" /> or empty.</exception>
        /// <exception cref="T:System.IO.IOException"><paramref name="path" /> is read-only or a directory.</exception>
        void Delete(string path);

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The file to check.</param>
        /// <returns><c>true</c> if the file exists; otherwise <c>false</c>.</returns>
        bool Exists(string path);

        /// <summary>
        /// Returns the names of subdirectories (including their paths) in the specified directory.
        /// </summary>
        /// <param name="path">The absolute path to the directory to search.</param>
        string[] GetDirectories(string path);

        /// <summary>
        /// Returns the names of files (including their paths) in the specified directory.
        /// </summary>
        /// <param name="path">The absolute path to the directory to search.</param>
        string[] GetFiles(string path);

        /// <summary>
        /// Opens an existing file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>A <see cref="FileStream"/> on the specified path.</returns>
        FileStream OpenRead(string path);

        /// <summary>
        /// Opens an existing UTF-8 encoded text file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>A <see cref="StreamReader"/> on the specified path.</returns>
        StreamReader OpenText(string path);

        /// <summary>
        /// Opens an existing file or creates a new file for writing.
        /// </summary>
        /// <param name="path">The file to be opened for writing.</param>
        FileStream OpenWrite(string path);

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        byte[] ReadAllBytes(string path);

        /// <summary>
        /// Opens a text file, reads all the text in the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        string ReadAllText(string path);

        /// <summary>
        /// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="bytes">The bytes to write to the file.</param>
        void WriteAllBytes(string path, byte[] bytes);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        void WriteAllText(string path, string? contents);
    }
}
