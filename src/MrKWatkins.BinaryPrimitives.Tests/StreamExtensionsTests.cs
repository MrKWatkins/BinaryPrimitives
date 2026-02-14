namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class StreamExtensionsTests
{
    [Test]
    public void ReadByteOrThrow()
    {
        using var stream = new MemoryStream([0xAB]);
        stream.ReadByteOrThrow().Should().Equal((byte)0xAB);
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
        stream.ReadWordOrThrow().Should().Equal((ushort)0x1234);
    }

    [Test]
    public void ReadWordOrThrow_BigEndian()
    {
        using var stream = new MemoryStream([0x12, 0x34]);
        stream.ReadWordOrThrow(Endian.Big).Should().Equal((ushort)0x1234);
    }

    [Test]
    public void WriteWord()
    {
        using var bytes = new MemoryStream();
        bytes.WriteByte(0x01);

        bytes.WriteWord(0x1234);
        bytes.ToArray().Should().SequenceEqual(new byte[] { 0x01, 0x34, 0x12 });

        bytes.WriteWord(0x5678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(new byte[] { 0x01, 0x34, 0x12, 0x56, 0x78 });
    }
}