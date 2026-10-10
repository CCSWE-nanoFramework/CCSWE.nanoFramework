using System;
using System.IO;

namespace CCSWE.nanoFramework.WebServer.Http.Internal;

/// <summary>
/// A read-only <see cref="Stream"/> that reads at most <c>length</c> bytes from the inner stream, then reports end of stream.
/// Disposing it does not dispose the inner stream.
/// </summary>
internal class ContentLengthStream : Stream
{
    private readonly Stream _innerStream;
    private readonly long _length;
    private long _remaining;

    public ContentLengthStream(Stream innerStream, long length)
    {
        ArgumentNullException.ThrowIfNull(innerStream);

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        _innerStream = innerStream;
        _length = length;
        _remaining = length;
    }

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanTimeout => _innerStream.CanTimeout;

    public override bool CanWrite => false;

    public override long Length => _length;

    public override long Position
    {
        get => _length - _remaining;
        set => throw new NotSupportedException();
    }

    public override int ReadTimeout
    {
        get => _innerStream.ReadTimeout;
        set => _innerStream.ReadTimeout = value;
    }

    public override void Flush()
    {
        // Read-only stream; nothing to flush.
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (_remaining <= 0 || count <= 0)
        {
            return 0;
        }

        if (count > _remaining)
        {
            count = (int)_remaining;
        }

        var bytesRead = _innerStream.Read(buffer, offset, count);

        if (bytesRead > 0)
        {
            _remaining -= bytesRead;
        }

        return bytesRead;
    }

    public override int Read(SpanByte buffer)
    {
        if (_remaining <= 0 || buffer.Length == 0)
        {
            return 0;
        }

        if (buffer.Length > _remaining)
        {
            buffer = buffer.Slice(0, (int)_remaining);
        }

        var bytesRead = _innerStream.Read(buffer);

        if (bytesRead > 0)
        {
            _remaining -= bytesRead;
        }

        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
}
