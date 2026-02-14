namespace MrKWatkins.BinaryPrimitives.Tests;

[SuppressMessage("ReSharper", "AccessToDisposedClosure")]
public sealed class StreamExtensionsTests
{
    [Test]
    public void ReadAllBytes()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x03]);
        stream.ReadAllBytes().Should().SequenceEqual(0x01, 0x02, 0x03);
    }

    [Test]
    public void ReadAllBytes_EmptyStream()
    {
        using var stream = new MemoryStream();
        stream.ReadAllBytes().Should().SequenceEqual();
    }

    [Test]
    public void ReadAllBytes_PartiallyReadStream()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x03]);
        stream.ReadByte();
        stream.ReadAllBytes().Should().SequenceEqual(0x02, 0x03);
    }

    [Test]
    public void ReadExactly()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x03]);
        stream.ReadExactly(2).Should().SequenceEqual(0x01, 0x02);
    }

    [Test]
    public void ReadExactly_EndOfStream()
    {
        using var stream = new MemoryStream([0x01]);
        AssertThat.Invoking(() => stream.ReadExactly(2))
            .Should().Throw<EndOfStreamException>();
    }

    [Test]
    public void ReadByteOrThrow()
    {
        using var stream = new MemoryStream([0xAB]);
        stream.ReadByteOrThrow().Should().Equal(0xAB);
    }

    [Test]
    public void ReadByteOrThrow_EndOfStream()
    {
        using var stream = new MemoryStream();
        AssertThat.Invoking(() => stream.ReadByteOrThrow())
            .Should().Throw<EndOfStreamException>();
    }

    [Test]
    public void ReadWordOrThrow()
    {
        using var stream = new MemoryStream([0x34, 0x12]);
        stream.ReadWordOrThrow().Should().Equal(0x1234);
    }

    [Test]
    public void ReadWordOrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34]);
        stream.ReadWordOrThrow(Endian.Big).Should().Equal(0x1234);
    }

    [Test]
    public void WriteWord()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteWord(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12);

        bytes.WriteWord(0x5678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12, 0x56, 0x78);
    }
}