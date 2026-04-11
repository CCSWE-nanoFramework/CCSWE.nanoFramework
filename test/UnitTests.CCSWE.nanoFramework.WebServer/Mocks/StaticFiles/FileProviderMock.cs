using System;
using CCSWE.nanoFramework.WebServer.StaticFiles;

namespace UnitTests.CCSWE.nanoFramework.WebServer.Mocks.StaticFiles;
internal class FileProviderMock: IFileProvider
{
    public IFileInfo GetFileInfo(string subpath)
    {
        throw new NotImplementedException();
    }
}
