using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

public sealed class ReadOnlyListStream(IReadOnlyList<byte> list) : Stream
{
    private int position;
    private bool disposed;

    public override int Read(byte[] buffer, int offset, int count)
    {
        VerifyNotDisposed();

        if (offset < 0 || offset >= buffer.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(offset), offset, $"Value must be not be negative and be less than the length of {nameof(buffer)}, {buffer.Length}.");
        }
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Value must be not be negative.");
        }

        var startPosition = position;
        var maximumCanRead = Math.Min(count, list.Count - position);
        ref var bufferRef = ref MemoryMarshal.GetReference(buffer.AsSpan(offset));
        for (var f = startPosition; f < startPosition + maximumCanRead; f++)
        {
            bufferRef = list[f];
            bufferRef = ref Unsafe.Add(ref bufferRef, 1);
            position++;
        }

        return position - startPosition;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        VerifyNotDisposed();

        Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => position + offset,
            SeekOrigin.End => list.Count - 1 - offset,
            _ => throw new NotSupportedException($"The {nameof(SeekOrigin)} value {origin} is not supported.")
        };

        return Position;
    }

    public override bool CanRead
    {
        get
        {
            VerifyNotDisposed();
            return true;
        }
    }

    public override bool CanSeek
    {
        get
        {
            VerifyNotDisposed();
            return true;
        }
    }

    public override bool CanWrite
    {
        get
        {
            VerifyNotDisposed();
            return false;
        }
    }

    public override long Length
    {
        get
        {
            VerifyNotDisposed();
            return list.Count;
        }
    }

    public override long Position
    {
        get
        {
            VerifyNotDisposed();
            return position;
        }
        set
        {
            VerifyNotDisposed();
            if (value < 0 || value >= list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Value must be in the range 0 -> {list.Count - 1}");
            }
            position = (int)value;
        }
    }

    public override void Flush() => ThrowNotWriteable();

    public override void SetLength(long value) => ThrowNotWriteable();

    public override void Write(byte[] buffer, int offset, int count) => ThrowNotWriteable();

    protected override void Dispose(bool disposing)
    {
        if (disposing && !disposed)
        {
            disposed = true;
        }
        base.Dispose(disposing);
    }

    private void VerifyNotDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(ReadOnlyListStream));
        }
    }

    [DoesNotReturn]
    private void ThrowNotWriteable()
    {
        VerifyNotDisposed();
        throw new NotSupportedException("Stream is not writable.");
    }
}