
namespace CCSWE.nanoFramework.WebServer.StaticFiles;

/// <summary>
/// A read-only file provider abstraction.
/// </summary>
public interface IFileProvider
{
    /// <summary>
    /// Locate a file at the given path.
    /// </summary>
    /// <param name="subpath">Relative path that identifies the file.</param>
    /// <returns>The file information. Caller must check Exists property.</returns>
    IFileInfo GetFileInfo(string subpath);
}