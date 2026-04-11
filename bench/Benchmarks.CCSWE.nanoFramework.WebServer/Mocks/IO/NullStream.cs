using System;
using System.IO;

namespace Benchmarks.CCSWE.nanoFramework.WebServer.Mocks.IO;

/// <summary>
/// A write-only stream that discards all data. Used in benchmarks to avoid
/// MemoryStream capacity exhaustion across many iterations.
/// </summary>
internal class NullStream : Stream
{
    public override bool CanRead => false;
    public override bool CanSeek => false;
    public override bool CanWrite => true;
    public override long Length => 0;
    public override long Position { get; set; }

    public override void Flush() { }

    public override int Read(SpanByte buffer) => 0;

    public override int Read(byte[] buffer, int offset, int count) => 0;

    public override long Seek(long offset, SeekOrigin origin) => Position;

    public override void SetLength(long value) { }

    public override void Write(byte[] buffer, int offset, int count) { }
}
