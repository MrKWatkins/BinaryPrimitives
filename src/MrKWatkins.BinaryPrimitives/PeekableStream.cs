namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// A read-only <see cref="Stream" /> wrapper that supports peeking at the next byte without consuming it.
/// </summary>
public sealed class PeekableStream : Stream
{
    private const int NotPeeked = int.MinValue;

    private readonly Stream stream;
    private readonly bool leaveOpen;
    private int peeked = NotPeeked;
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PeekableStream" /> class.
    /// </summary>
    /// <param name="stream">The underlying readable stream to wrap.</param>
    /// <param name="leaveOpen"><c>true</c> to leave <paramref name="stream" /> open when this stream is disposed; otherwise, <c>false</c>. Defaults to <c>true</c>.</param>
    /// <exception cref="ArgumentException"><paramref name="stream" /> is not readable.</exception>
    public PeekableStream(Stream stream, bool leaveOpen = true)
    {
        if (!stream.CanRead)
        {
            throw new ArgumentException("Value is not readable.", nameof(stream));
        }
        this.stream = stream;
        this.leaveOpen = leaveOpen;
    }

    /// <summary>
    /// Reads the next byte from the stream without consuming it. Subsequent calls to <see cref="Peek" /> will return
    /// the same value until the byte is consumed by a read operation, or the position is changed.
    /// </summary>
    /// <returns>The next byte in the stream, or -1 if the end of the stream has been reached.</returns>
    public int Peek()
    {
        VerifyNotDisposed();

        peeked = ReadByte();
        return peeked;
    }

    /// <summary>
    /// Gets a value indicating whether the end of the stream has been reached.
    /// </summary>
    public bool EndOfStream => Peek() == -1;

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        VerifyNotDisposed();

        switch (peeked)
        {
            case -1:
                return 0;

            case NotPeeked:
                return stream.Read(buffer, offset, count);
        }

        buffer[offset] = (byte)peeked;
        peeked = NotPeeked;

        if (count == 1)
        {
            return 1;
        }

        return stream.Read(buffer, offset + 1, count - 1) + 1;
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin)
    {
        VerifyNotDisposed();

        peeked = NotPeeked;
        return stream.Seek(offset, origin);
    }

    /// <inheritdoc />
    public override long Position
    {
        get
        {
            VerifyNotDisposed();
            if (peeked >= 0)
            {
                return stream.Position - 1;
            }
            return stream.Position;
        }
        set
        {
            VerifyNotDisposed();
            peeked = NotPeeked;
            stream.Position = value;
        }
    }

    /// <inheritdoc />
    public override bool CanRead => !disposed && stream.CanRead;

    /// <inheritdoc />
    public override bool CanSeek => !disposed && stream.CanSeek;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override long Length
    {
        get
        {
            VerifyNotDisposed();
            return stream.Length;
        }
    }

    /// <inheritdoc />
    public override void Flush() { VerifyNotDisposed(); }

    /// <inheritdoc />
    public override void SetLength(long value) => ThrowNotWriteable();

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) => ThrowNotWriteable();

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        if (disposing && !disposed)
        {
            disposed = true;
            if (!leaveOpen)
            {
                stream.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    private void VerifyNotDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(PeekableStream));
        }
    }

    [DoesNotReturn]
    private void ThrowNotWriteable()
    {
        VerifyNotDisposed();
        throw new NotSupportedException("Stream is not writable.");
    }
}