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
    public void ReadInt16OrThrow()
    {
        using var stream = new MemoryStream([0x34, 0x12]);
        stream.ReadInt16OrThrow().Should().Equal(0x1234);
    }

    [Test]
    public void ReadInt16OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34]);
        stream.ReadInt16OrThrow(Endian.Big).Should().Equal(0x1234);
    }

    [Test]
    public void WriteInt16()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteInt16(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12);

        bytes.WriteInt16(0x5678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12, 0x56, 0x78);
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

    [Test]
    public void ReadUInt24OrThrow()
    {
        using var stream = new MemoryStream([0x56, 0x34, 0x12]);
        stream.ReadUInt24OrThrow().Should().Equal(0x123456);
    }

    [Test]
    public void ReadUInt24OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56]);
        stream.ReadUInt24OrThrow(Endian.Big).Should().Equal(0x123456);
    }

    [Test]
    public void WriteUInt24()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteUInt24(0x123456);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x56, 0x34, 0x12);

        bytes.WriteUInt24(0x789ABC, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x56, 0x34, 0x12, 0x78, 0x9A, 0xBC);
    }

    [Test]
    public void ReadInt32OrThrow()
    {
        using var stream = new MemoryStream([0x78, 0x56, 0x34, 0x12]);
        stream.ReadInt32OrThrow().Should().Equal(0x12345678);
    }

    [Test]
    public void ReadInt32OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78]);
        stream.ReadInt32OrThrow(Endian.Big).Should().Equal(0x12345678);
    }

    [Test]
    public void WriteInt32()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteInt32(0x12345678);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12);

        bytes.WriteInt32(0x12345678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78);
    }

    [Test]
    public void ReadUInt32OrThrow()
    {
        using var stream = new MemoryStream([0x78, 0x56, 0x34, 0x12]);
        stream.ReadUInt32OrThrow().Should().Equal(0x12345678U);
    }

    [Test]
    public void ReadUInt32OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78]);
        stream.ReadUInt32OrThrow(Endian.Big).Should().Equal(0x12345678U);
    }

    [Test]
    public void WriteUInt32()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteUInt32(0x12345678U);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12);

        bytes.WriteUInt32(0x12345678U, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78);
    }

    [Test]
    public void ReadInt64OrThrow()
    {
        using var stream = new MemoryStream([0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12]);
        stream.ReadInt64OrThrow().Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public void ReadInt64OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF]);
        stream.ReadInt64OrThrow(Endian.Big).Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public void WriteInt64()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteInt64(0x1234567890ABCDEF);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12);

        bytes.WriteInt64(0x1234567890ABCDEF, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF);
    }

    [Test]
    public void ReadUInt64OrThrow()
    {
        using var stream = new MemoryStream([0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12]);
        stream.ReadUInt64OrThrow().Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public void ReadUInt64OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF]);
        stream.ReadUInt64OrThrow(Endian.Big).Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public void WriteUInt64()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteUInt64(0x1234567890ABCDEF);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12);

        bytes.WriteUInt64(0x1234567890ABCDEF, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF);
    }
}