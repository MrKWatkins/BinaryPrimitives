namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteSpanExtensionsTests
{
    [Test]
    public void GetInt16_Span()
    {
        Span<byte> bytes = [0x01, 0x02];

        bytes.GetInt16().Should().Equal(0x0201);
    }


    [Test]
    public void GetInt16_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02];

        bytes.GetInt16(Endian.Little).Should().Equal(0x0201);
        bytes.GetInt16(Endian.Big).Should().Equal(0x0102);
    }


    [Test]
    public void SetInt16_Span()
    {
        Span<byte> bytes = [0xFF, 0xFE];

        bytes.SetInt16(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x34, 0x12);
    }


    [Test]
    public void SetInt16_Span_Endian()
    {
        Span<byte> bytes = [0xFF, 0xFE];

        bytes.SetInt16(0x1234, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0x34, 0x12);

        bytes.SetInt16(0x1234, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34);
    }


    [Test]
    public void GetInt32_Span()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt32().Should().Equal(0x04030201);
    }


    [Test]
    public void GetInt32_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt32(Endian.Little).Should().Equal(0x04030201);
        bytes.GetInt32(Endian.Big).Should().Equal(0x01020304);
    }


    [Test]
    public void SetInt32_Span()
    {
        Span<byte> bytes = [0xFF, 0xFE, 0xFD, 0xFC];

        bytes.SetInt32(0x12345678);
        bytes.ToArray().Should().SequenceEqual(0x78, 0x56, 0x34, 0x12);
    }


    [Test]
    public void SetInt32_Span_Endian()
    {
        Span<byte> bytes = [0xFF, 0xFE, 0xFD, 0xFC];

        bytes.SetInt32(0x12345678, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0x78, 0x56, 0x34, 0x12);

        bytes.SetInt32(0x12345678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34, 0x56, 0x78);
    }


    [Test]
    public void GetInt64_Span()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetInt64().Should().Equal(0x0807060504030201L);
    }


    [Test]
    public void GetInt64_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetInt64(Endian.Little).Should().Equal(0x0807060504030201L);
        bytes.GetInt64(Endian.Big).Should().Equal(0x0102030405060708L);
    }


    [Test]
    public void SetInt64_Span()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.SetInt64(0x123456789ABCDEF0L);
        bytes.ToArray().Should().SequenceEqual(0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12);
    }


    [Test]
    public void SetInt64_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.SetInt64(0x123456789ABCDEF0L, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12);

        bytes.SetInt64(0x123456789ABCDEF0L, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0);
    }


    [Test]
    public void GetUInt24_Span()
    {
        Span<byte> bytes = [0x78, 0x56, 0x34];

        bytes.GetUInt24().Should().Equal(0x345678);
    }


    [Test]
    public void GetUInt24_Span_Endian()
    {
        Span<byte> bytes = [0x78, 0x56, 0x34];

        bytes.GetUInt24(Endian.Little).Should().Equal(0x345678);
        bytes.GetUInt24(Endian.Big).Should().Equal(0x785634);
    }


    [Test]
    public void SetUInt24_Span()
    {
        Span<byte> bytes = [0x00, 0x00, 0x00];

        bytes.SetUInt24(0x123456);
        bytes.ToArray().Should().SequenceEqual(0x56, 0x34, 0x12);

        bytes.SetUInt24(0x654321);
        bytes.ToArray().Should().SequenceEqual(0x21, 0x43, 0x65);
    }


    [Test]
    public void SetUInt24_Span_Endian()
    {
        Span<byte> bytes = [0x00, 0x00, 0x00];

        bytes.SetUInt24(0x123456, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0x56, 0x34, 0x12);

        bytes.SetUInt24(0x123456, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34, 0x56);
    }


    [Test]
    public void GetUInt32_Span()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetUInt32().Should().Equal(0x04030201U);
    }


    [Test]
    public void GetUInt32_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetUInt32(Endian.Little).Should().Equal(0x04030201U);
        bytes.GetUInt32(Endian.Big).Should().Equal(0x01020304U);
    }


    [Test]
    public void SetUInt32_Span()
    {
        Span<byte> bytes = [0xFF, 0xFE, 0xFD, 0xFC];

        bytes.SetUInt32(0x12345678U);
        bytes.ToArray().Should().SequenceEqual(0x78, 0x56, 0x34, 0x12);
    }


    [Test]
    public void SetUInt32_Span_Endian()
    {
        Span<byte> bytes = [0xFF, 0xFE, 0xFD, 0xFC];

        bytes.SetUInt32(0x12345678U, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0x78, 0x56, 0x34, 0x12);

        bytes.SetUInt32(0x12345678U, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34, 0x56, 0x78);
    }


    [Test]
    public void GetUInt64_Span()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetUInt64().Should().Equal(0x0807060504030201UL);
    }


    [Test]
    public void GetUInt64_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.GetUInt64(Endian.Little).Should().Equal(0x0807060504030201UL);
        bytes.GetUInt64(Endian.Big).Should().Equal(0x0102030405060708UL);
    }


    [Test]
    public void SetUInt64_Span()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.SetUInt64(0x123456789ABCDEF0UL);
        bytes.ToArray().Should().SequenceEqual(0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12);
    }


    [Test]
    public void SetUInt64_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08];

        bytes.SetUInt64(0x123456789ABCDEF0UL, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12);

        bytes.SetUInt64(0x123456789ABCDEF0UL, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0);
    }


    [Test]
    public void GetWord_Span()
    {
        Span<byte> bytes = [0x01, 0x02];

        bytes.GetWord().Should().Equal(0x0201);
    }


    [Test]
    public void GetWord_Span_Endian()
    {
        Span<byte> bytes = [0x01, 0x02];

        bytes.GetWord(Endian.Little).Should().Equal(0x0201);
        bytes.GetWord(Endian.Big).Should().Equal(0x0102);
    }


    [Test]
    public void SetWord_Span()
    {
        Span<byte> bytes = [0xFF, 0xFE];

        bytes.SetWord(0x1234);
        bytes.ToArray().Should().SequenceEqual(0x34, 0x12);
    }


    [Test]
    public void SetWord_Span_Endian()
    {
        Span<byte> bytes = [0xFF, 0xFE];

        bytes.SetWord(0x1234, Endian.Little);
        bytes.ToArray().Should().SequenceEqual(0x34, 0x12);

        bytes.SetWord(0x1234, Endian.Big);
        bytes.ToArray().Should().SequenceEqual(0x12, 0x34);
    }
}