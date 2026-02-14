namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ReadOnlyListStreamTests
{
    [Test]
    public void Read()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);
        stream.Position.Should().Equal(0);

        var buffer = new byte[] { 0xFF, 0xFE, 0xFD, 0xFC };
        stream.Read(buffer, 1, 1).Should().Equal(1);
        buffer.Should().SequenceEqual(0xFF, 1, 0xFD, 0xFC);
        stream.Position.Should().Equal(1);

        stream.Read(buffer, 1, 2).Should().Equal(2);
        buffer.Should().SequenceEqual(0xFF, 2, 3, 0xFC);
        stream.Position.Should().Equal(3);

        stream.Read(buffer, 0, 3).Should().Equal(2);
        buffer.Should().SequenceEqual(4, 5, 3, 0xFC);
        stream.Position.Should().Equal(5);

        stream.Read(buffer, 1, 1).Should().Equal(0);
        stream.Position.Should().Equal(5);

        stream.Invoking(s => s.Read(buffer, -1, 0)).Should().Throw<ArgumentOutOfRangeException>();
        stream.Invoking(s => s.Read(buffer, 5, 0)).Should().Throw<ArgumentOutOfRangeException>();
        stream.Invoking(s => s.Read(buffer, 0, -1)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void Seek()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);

        stream.Seek(1, SeekOrigin.Begin);
        stream.Position.Should().Equal(1);

        stream.Seek(3, SeekOrigin.End);
        stream.Position.Should().Equal(1);

        stream.Seek(2, SeekOrigin.Current);
        stream.Position.Should().Equal(3);

        stream.Invoking(s => s.Seek(0, (SeekOrigin)int.MaxValue)).Should().Throw<NotSupportedException>();
    }

    [Test]
    public void Position()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);

        stream.Position = 2;
        stream.ReadByte().Should().Equal(3);

        stream.Position = 4;
        stream.ReadByte().Should().Equal(5);

        stream.Position = 1;
        stream.ReadByte().Should().Equal(2);

        stream.Position = 0;
        stream.ReadByte().Should().Equal(1);

        stream.Invoking(s => s.Position = -1).Should().Throw<ArgumentOutOfRangeException>();
        stream.Invoking(s => s.Position = 5).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void CanRead()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);
        stream.CanRead.Should().BeTrue();
    }

    [Test]
    public void CanWrite()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);
        stream.CanWrite.Should().BeFalse();
    }

    [Test]
    public void CanSeek()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);
        stream.CanSeek.Should().BeTrue();
    }

    [Test]
    public void Length()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);
        stream.Length.Should().Equal(5);
    }

    [Test]
    public void MembersThatWriteThrow()
    {
        using var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);

        stream.Invoking(p => p.Flush()).Should().Throw<NotSupportedException>();
        stream.Invoking(p => p.SetLength(1)).Should().Throw<NotSupportedException>();
        stream.Invoking(p => p.Write([], 0, 0)).Should().Throw<NotSupportedException>();
    }

    [Test]
    public void Dispose()
    {
        var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);

        stream.Dispose();

        stream.Invoking(s => s.Dispose()).Should().NotThrow();
    }

    [Test]
    public void MembersThrowIfDisposed()
    {
        var stream = new ReadOnlyListStream([1, 2, 3, 4, 5]);
        stream.Dispose();

        stream.Invoking(p => p.Read([], 0, 0)).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.Seek(0, SeekOrigin.Begin)).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.Position).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.Position = 0).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.CanRead).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.CanSeek).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.CanWrite).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.Length).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.Flush()).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.SetLength(1)).Should().Throw<ObjectDisposedException>();
        stream.Invoking(p => p.Write([], 0, 0)).Should().Throw<ObjectDisposedException>();
    }
}