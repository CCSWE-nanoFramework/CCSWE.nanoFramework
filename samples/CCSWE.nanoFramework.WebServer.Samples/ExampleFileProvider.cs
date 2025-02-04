using System.IO;
using System.Text;
using CCSWE.nanoFramework.WebServer.StaticFiles;

namespace CCSWE.nanoFramework.WebServer.Samples
{
    internal class ExampleFileProvider: IFileProvider
    {
        public IFileInfo GetFileInfo(string subpath)
        {
            if (string.IsNullOrEmpty(subpath) || subpath.Equals("index.html"))
            {
                return new ExampleFileInfo { Exists = true };
            }

            return new ExampleFileInfo { Exists = false };
        }
    }

    internal class ExampleFileInfo : IFileInfo
    {
        public static string Content = "<html><head><title>Example</title></head><body><h1>Hello, World!</h1></body></html>";
        
        public bool Exists { get; init; }

        public string Name => "index.html";

        public Stream CreateReadStream()
        {
            // This is a simple example, so we'll just return a MemoryStream with the content
            // Normally you would return a FileStream or some other stream that reads the file from disk or resources
            return new MemoryStream(Encoding.UTF8.GetBytes(Content));
        }
    }
}
