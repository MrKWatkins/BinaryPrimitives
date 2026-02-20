namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteReadOnlySpanExtensionsTests
{
    [Test]
    public void GetInt16_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02];

        bytes.GetInt16().Should().Equal(0x0201);
    }


    [Test]
    public void GetInt16_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02];

        bytes.GetInt16(Endian.Little).Should().Equal(0x0201);
        bytes.GetInt16(Endian.Big).Should().Equal(0x0102);
    }


    [Test]
    public void GetInt32_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt32().Should().Equal(0x04030201);
    }


    [Test]
    public void GetInt32_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt32(Endian.Little).Should().Equal(0x04030201);
        bytes.GetInt32(Endian.Big).Should().Equal(0x01020304);
    }


    [Test]
    public void GetInt64_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetInt64().Should().Equal(0x0807060504030201L);
    }


    [Test]
    public void GetInt64_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetInt64(Endian.Little).Should().Equal(0x0807060504030201L);
        bytes.GetInt64(Endian.Big).Should().Equal(0x0102030405060708L);
    }


    [Test]
    public void GetUInt24_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x78, 0x56, 0x34];

        bytes.GetUInt24().Should().Equal(0x345678);
    }


    [Test]
    public void GetUInt24_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x78, 0x56, 0x34];

        bytes.GetUInt24(Endian.Little).Should().Equal(0x345678);
        bytes.GetUInt24(Endian.Big).Should().Equal(0x785634);
    }


    [Test]
    public void GetUInt32_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetUInt32().Should().Equal(0x04030201U);
    }


    [Test]
    public void GetUInt32_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetUInt32(Endian.Little).Should().Equal(0x04030201U);
        bytes.GetUInt32(Endian.Big).Should().Equal(0x01020304U);
    }


    [Test]
    public void GetUInt64_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetUInt64().Should().Equal(0x0807060504030201UL);
    }


    [Test]
    public void GetUInt64_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetUInt64(Endian.Little).Should().Equal(0x0807060504030201UL);
        bytes.GetUInt64(Endian.Big).Should().Equal(0x0102030405060708UL);
    }


    [Test]
    public void GetUInt16_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02];

        bytes.GetUInt16().Should().Equal(0x0201);
    }


    [Test]
    public void GetUInt16_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02];

        bytes.GetUInt16(Endian.Little).Should().Equal(0x0201);
        bytes.GetUInt16(Endian.Big).Should().Equal(0x0102);
    }
}