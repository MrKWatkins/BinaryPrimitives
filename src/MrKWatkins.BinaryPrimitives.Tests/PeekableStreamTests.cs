using System.IO.Compression;

namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class PeekableStreamTests
{
    [Test]
    public void Constructor_ThrowsIfNotReadable()
    {
        using var stream = new MemoryStream();
        using var notReadable = new DeflateStream(stream, CompressionMode.Compress);

        // ReSharper disable once AccessToDisposedClosure
        AssertThat.Invoking(() => new PeekableStream(notReadable)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Read_NoPeek()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);
        peekable.Position.Should().Equal(0);

        var buffer = new byte[4];
        peekable.Read(buffer, 1, 1).Should().Equal(1);
        buffer.Should().SequenceEqual(0, 1, 0, 0);
        peekable.Position.Should().Equal(1);

        peekable.Read(buffer, 1, 2).Should().Equal(2);
        buffer.Should().SequenceEqual(0, 2, 3, 0);
        peekable.Position.Should().Equal(3);

        peekable.Read(buffer, 1, 3).Should().Equal(2);
        buffer.Should().SequenceEqual(0, 4, 5, 0);
        peekable.Position.Should().Equal(5);

        peekable.Read(buffer, 1, 1).Should().Equal(0);
        peekable.Position.Should().Equal(5);
    }

    [Test]
    public void Read_WithPeek()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);
        peekable.Position.Should().Equal(0);

        peekable.Peek().Should().Equal(1);
        peekable.Position.Should().Equal(0);

        var buffer = new byte[4];
        peekable.Read(buffer, 1, 1).Should().Equal(1);
        buffer.Should().SequenceEqual(0, 1, 0, 0);
        peekable.Position.Should().Equal(1);

        peekable.Peek().Should().Equal(2);
        peekable.Position.Should().Equal(1);

        // Peek again should return the same.
        peekable.Peek().Should().Equal(2);
        peekable.Position.Should().Equal(1);

        peekable.Read(buffer, 1, 2).Should().Equal(2);
        buffer.Should().SequenceEqual(0, 2, 3, 0);
        peekable.Position.Should().Equal(3);

        peekable.Read(buffer, 1, 3).Should().Equal(2);
        buffer.Should().SequenceEqual(0, 4, 5, 0);
        peekable.Position.Should().Equal(5);

        peekable.Peek().Should().Equal(-1);
        peekable.Read(buffer, 1, 1).Should().Equal(0);
        peekable.Position.Should().Equal(5);
    }

    [Test]
    public void Seek()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);

        peekable.Seek(1, SeekOrigin.Begin);
        peekable.ReadByte().Should().Equal(2);
        peekable.Peek().Should().Equal(3);

        peekable.Seek(3, SeekOrigin.Begin);
        peekable.ReadByte().Should().Equal(4);
        peekable.Peek().Should().Equal(5);

        peekable.Seek(2, SeekOrigin.Begin);
        peekable.Peek().Should().Equal(3);
        peekable.ReadByte().Should().Equal(3);
    }

    [Test]
    public void Position()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);

        peekable.Position = 2;
        peekable.Peek().Should().Equal(3);

        peekable.Position = 4;
        peekable.Peek().Should().Equal(5);

        peekable.Position = 1;
        peekable.ReadByte().Should().Equal(2);
        peekable.Peek().Should().Equal(3);
    }

    [Test]
    public void CanRead()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);
        peekable.CanRead.Should().BeTrue();
    }

    [Test]
    public void CanWrite()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);
        peekable.CanWrite.Should().BeFalse();
    }

    [Test]
    public void CanSeek()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);
        peekable.CanSeek.Should().BeTrue();
    }

    [Test]
    public void Length()
    {
        using var stream = new MemoryStream([1, 2, 3, 4, 5]);
        using var peekable = new PeekableStream(stream);
        peekable.Length.Should().Equal(5);
    }

    [Test]
    public void Dispose_LeaveOpenFalse()
    {
        using var stream = new MemoryStream();
        var peekable = new PeekableStream(stream, leaveOpen: false);

        peekable.Dispose();

        stream.Invoking(s => s.ReadByte()).Should().Throw<ObjectDisposedException>();
    }

    [Test]
    public void Dispose_LeaveOpenTrue()
    {
        using var stream = new MemoryStream();
        var peekable = new PeekableStream(stream, leaveOpen: true);

        peekable.Dispose();

        stream.Invoking(s => s.ReadByte()).Should().NotThrow();
    }

    [Test]
    public void MembersThatWriteThrow()
    {
        using var stream = new MemoryStream();
        using var peekable = new PeekableStream(stream);

        peekable.Invoking(p => p.Flush()).Should().Throw<NotSupportedException>();
        peekable.Invoking(p => p.SetLength(1)).Should().Throw<NotSupportedException>();
        peekable.Invoking(p => p.Write([], 0, 0)).Should().Throw<NotSupportedException>();
    }

    [Test]
    public void MembersThrowIfDisposed()
    {
        using var stream = new MemoryStream();
        var peekable = new PeekableStream(stream);
        peekable.Dispose();

        peekable.Invoking(p => p.Peek()).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Read([], 0, 0)).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Seek(0, SeekOrigin.Begin)).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Position).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Position = 0).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.CanRead).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.CanSeek).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.CanWrite).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Length).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Flush()).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.SetLength(1)).Should().Throw<ObjectDisposedException>();
        peekable.Invoking(p => p.Write([], 0, 0)).Should().Throw<ObjectDisposedException>();
    }
}