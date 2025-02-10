using System;
using System.Diagnostics.CodeAnalysis;
using CCSWE.nanoFramework.WebServer.StaticFiles;

namespace CCSWE.nanoFramework.WebServer.UnitTests.Mocks.StaticFiles
{
    internal class ContentTypeProviderMock: IContentTypeProvider
    {
        public bool TryGetContentType(string subpath, [MaybeNullWhen(false)] out string contentType)
        {
            throw new NotImplementedException();
        }
    }
}
