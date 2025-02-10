using System;
using CCSWE.nanoFramework.WebServer.StaticFiles;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.StaticFiles
{
    internal class FileProviderMock: IFileProvider
    {
        public IFileInfo GetFileInfo(string subpath)
        {
            throw new NotImplementedException();
        }
    }
}
