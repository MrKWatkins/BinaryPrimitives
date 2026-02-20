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
    public void ReadUInt16OrThrow()
    {
        using var stream = new MemoryStream([0x34, 0x12]);
        stream.ReadUInt16OrThrow().Should().Equal(0x1234);
    }

    [Test]
    public void ReadUInt16OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34]);
        stream.ReadUInt16OrThrow(Endian.Big).Should().Equal(0x1234);
    }

    [Test]
    public void WriteUInt16()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteUInt16(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12);

        bytes.WriteUInt16(0x5678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12, 0x56, 0x78);
    }

    [Test]
    public void ReadUInt24OrThrow()
    {
        using var stream = new MemoryStream([0x56, 0x34, 0x12]);
        ((int)stream.ReadUInt24OrThrow()).Should().Equal(0x123456);
    }

    [Test]
    public void ReadUInt24OrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56]);
        ((int)stream.ReadUInt24OrThrow(Endian.Big)).Should().Equal(0x123456);
    }

    [Test]
    public void WriteUInt24()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteUInt24((UInt24)0x123456);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x56, 0x34, 0x12);

        bytes.WriteUInt24((UInt24)0x789ABC, Endian.Big);
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


    [Test]
    public async Task ReadAllBytesAsync()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x03]);
        (await stream.ReadAllBytesAsync()).Should().SequenceEqual(0x01, 0x02, 0x03);
    }

    [Test]
    public async Task ReadAllBytesAsync_EmptyStream()
    {
        using var stream = new MemoryStream();
        (await stream.ReadAllBytesAsync()).Should().SequenceEqual();
    }

    [Test]
    public async Task ReadAllBytesAsync_PartiallyReadStream()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x03]);
        stream.ReadByte();
        (await stream.ReadAllBytesAsync()).Should().SequenceEqual(0x02, 0x03);
    }

    [Test]
    public async Task ReadExactlyAsync()
    {
        using var stream = new MemoryStream([0x01, 0x02, 0x03]);
        (await stream.ReadExactlyAsync(2)).Should().SequenceEqual(0x01, 0x02);
    }

    [Test]
    public void ReadExactlyAsync_EndOfStream()
    {
        using var stream = new MemoryStream([0x01]);
        Assert.ThrowsAsync<EndOfStreamException>(async () => _ = await stream.ReadExactlyAsync(2));
    }

    [Test]
    public async Task ReadByteOrThrowAsync()
    {
        using var stream = new MemoryStream([0xAB]);
        (await stream.ReadByteOrThrowAsync()).Should().Equal(0xAB);
    }

    [Test]
    public void ReadByteOrThrowAsync_EndOfStream()
    {
        using var stream = new MemoryStream();
        Assert.ThrowsAsync<EndOfStreamException>(async () => _ = await stream.ReadByteOrThrowAsync());
    }

    [Test]
    public async Task ReadInt16OrThrowAsync()
    {
        using var stream = new MemoryStream([0x34, 0x12]);
        (await stream.ReadInt16OrThrowAsync()).Should().Equal(0x1234);
    }

    [Test]
    public async Task ReadInt16OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34]);
        (await stream.ReadInt16OrThrowAsync(Endian.Big)).Should().Equal(0x1234);
    }

    [Test]
    public async Task WriteInt16Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteInt16Async(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12);

        await bytes.WriteInt16Async(0x5678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12, 0x56, 0x78);
    }

    [Test]
    public async Task ReadUInt16OrThrowAsync()
    {
        using var stream = new MemoryStream([0x34, 0x12]);
        (await stream.ReadUInt16OrThrowAsync()).Should().Equal(0x1234);
    }

    [Test]
    public async Task ReadUInt16OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34]);
        (await stream.ReadUInt16OrThrowAsync(Endian.Big)).Should().Equal(0x1234);
    }

    [Test]
    public async Task WriteUInt16Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteUInt16Async(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12);

        await bytes.WriteUInt16Async(0x5678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x34, 0x12, 0x56, 0x78);
    }

    [Test]
    public async Task ReadUInt24OrThrowAsync()
    {
        using var stream = new MemoryStream([0x56, 0x34, 0x12]);
        ((int)await stream.ReadUInt24OrThrowAsync()).Should().Equal(0x123456);
    }

    [Test]
    public async Task ReadUInt24OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56]);
        ((int)await stream.ReadUInt24OrThrowAsync(Endian.Big)).Should().Equal(0x123456);
    }

    [Test]
    public async Task WriteUInt24Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteUInt24Async((UInt24)0x123456);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x56, 0x34, 0x12);

        await bytes.WriteUInt24Async((UInt24)0x789ABC, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x56, 0x34, 0x12, 0x78, 0x9A, 0xBC);
    }

    [Test]
    public async Task ReadInt32OrThrowAsync()
    {
        using var stream = new MemoryStream([0x78, 0x56, 0x34, 0x12]);
        (await stream.ReadInt32OrThrowAsync()).Should().Equal(0x12345678);
    }

    [Test]
    public async Task ReadInt32OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78]);
        (await stream.ReadInt32OrThrowAsync(Endian.Big)).Should().Equal(0x12345678);
    }

    [Test]
    public async Task WriteInt32Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteInt32Async(0x12345678);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12);

        await bytes.WriteInt32Async(0x12345678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78);
    }

    [Test]
    public async Task ReadUInt32OrThrowAsync()
    {
        using var stream = new MemoryStream([0x78, 0x56, 0x34, 0x12]);
        (await stream.ReadUInt32OrThrowAsync()).Should().Equal(0x12345678U);
    }

    [Test]
    public async Task ReadUInt32OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78]);
        (await stream.ReadUInt32OrThrowAsync(Endian.Big)).Should().Equal(0x12345678U);
    }

    [Test]
    public async Task WriteUInt32Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteUInt32Async(0x12345678U);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12);

        await bytes.WriteUInt32Async(0x12345678U, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78);
    }

    [Test]
    public async Task ReadInt64OrThrowAsync()
    {
        using var stream = new MemoryStream([0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12]);
        (await stream.ReadInt64OrThrowAsync()).Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public async Task ReadInt64OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF]);
        (await stream.ReadInt64OrThrowAsync(Endian.Big)).Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public async Task WriteInt64Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteInt64Async(0x1234567890ABCDEF);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12);

        await bytes.WriteInt64Async(0x1234567890ABCDEF, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF);
    }

    [Test]
    public async Task ReadUInt64OrThrowAsync()
    {
        using var stream = new MemoryStream([0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12]);
        (await stream.ReadUInt64OrThrowAsync()).Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public async Task ReadUInt64OrThrowAsync_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF]);
        (await stream.ReadUInt64OrThrowAsync(Endian.Big)).Should().Equal(0x1234567890ABCDEF);
    }

    [Test]
    public async Task WriteUInt64Async()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        await bytes.WriteUInt64Async(0x1234567890ABCDEF);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12);

        await bytes.WriteUInt64Async(0x1234567890ABCDEF, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF);
    }
}