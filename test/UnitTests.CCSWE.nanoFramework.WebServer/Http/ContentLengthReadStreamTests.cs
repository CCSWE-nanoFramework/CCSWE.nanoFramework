using System;
using System.IO;
using CCSWE.nanoFramework.WebServer.Http.Internal;
using nanoFramework.TestFramework;

namespace UnitTests.CCSWE.nanoFramework.WebServer.Http;
[TestClass]
public class ContentLengthReadStreamTests
{
    private static MemoryStream GetInnerStream()
    {
        return new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
    }

    [TestMethod]
    public void Constructor_throws_if_innerStream_is_null()
    {
        Assert.ThrowsException(typeof(ArgumentNullException), () => new ContentLengthReadStream(null!, 0));
    }

    [TestMethod]
    public void Constructor_throws_if_length_is_negative()
    {
        Assert.ThrowsException(typeof(ArgumentOutOfRangeException), () => new ContentLengthReadStream(GetInnerStream(), -1));
    }

    [TestMethod]
    public void Length_returns_length()
    {
        var stream = new ContentLengthReadStream(GetInnerStream(), 5);

        Assert.AreEqual(5L, stream.Length);
    }

    [TestMethod]
    public void Position_returns_bytes_read()
    {
        var stream = new ContentLengthReadStream(GetInnerStream(), 5);

        stream.Read(new byte[3], 0, 3);

        Assert.AreEqual(3L, stream.Position);
    }

    [TestMethod]
    public void Read_returns_requested_bytes()
    {
        var stream = new ContentLengthReadStream(GetInnerStream(), 5);
        var buffer = new byte[8];

        Assert.AreEqual(2, stream.Read(buffer, 0, 2));
        Assert.AreEqual(3, stream.Read(buffer, 2, 6));
        Assert.AreEqual((byte)1, buffer[0]);
        Assert.AreEqual((byte)5, buffer[4]);
        Assert.AreEqual((byte)0, buffer[5]);
    }

    [TestMethod]
    public void Read_returns_zero_after_length_bytes()
    {
        var innerStream = GetInnerStream();
        var stream = new ContentLengthReadStream(innerStream, 5);
        var buffer = new byte[8];

        var bytesRead = stream.Read(buffer, 0, buffer.Length);

        Assert.AreEqual(5, bytesRead);
        Assert.AreEqual(0, stream.Read(buffer, 0, buffer.Length));
        Assert.AreEqual(5L, innerStream.Position);
    }

    [TestMethod]
    public void Read_SpanByte_returns_zero_after_length_bytes()
    {
        var innerStream = GetInnerStream();
        var stream = new ContentLengthReadStream(innerStream, 5);
        var buffer = new SpanByte(new byte[8]);

        Assert.AreEqual(5, stream.Read(buffer));
        Assert.AreEqual(0, stream.Read(buffer));
        Assert.AreEqual(5L, innerStream.Position);
    }

    [TestMethod]
    public void Read_with_zero_length_does_not_read_innerStream()
    {
        var innerStream = GetInnerStream();
        var stream = new ContentLengthReadStream(innerStream, 0);

        Assert.AreEqual(0, stream.Read(new byte[8], 0, 8));
        Assert.AreEqual(0L, innerStream.Position);
    }

    [TestMethod]
    public void Seek_throws()
    {
        var stream = new ContentLengthReadStream(GetInnerStream(), 5);

        Assert.ThrowsException(typeof(NotSupportedException), () => stream.Seek(0, SeekOrigin.Begin));
    }

    [TestMethod]
    public void Write_throws()
    {
        var stream = new ContentLengthReadStream(GetInnerStream(), 5);

        Assert.ThrowsException(typeof(NotSupportedException), () => stream.Write(new byte[1], 0, 1));
    }
}
