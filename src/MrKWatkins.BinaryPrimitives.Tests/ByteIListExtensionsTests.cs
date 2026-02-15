namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteIListExtensionsTests
{
    [Test]
    public void GetWord_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetWord_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetWord(2, Endian.Big).Should().Equal(0x0304);
    }

    [Test]
    public void SetWord_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetWord(1, 0x1234);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);
    }

    [Test]
    public void SetWord_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetWord(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);

        bytes.SetWord(2, 0x5678, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x56, 0x78);
    }

    [Test]
    public void GetInt16_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetInt16_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetInt16(1, Endian.Big).Should().Equal(0x0203);
    }

    [Test]
    public void SetInt16_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetInt16(1, 0x1234);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);
    }

    [Test]
    public void SetInt16_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetInt16(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);

        bytes.SetInt16(1, 0x5678, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x56, 0x78, 0x04);
    }

    [Test]
    public void GetUInt24_IList()
    {
        IList<byte> bytes = [0x78, 0x56, 0x34, 0x12];

        bytes.GetUInt24(0).Should().Equal(0x345678);
        bytes.GetUInt24(1).Should().Equal(0x123456);
    }

    [Test]
    public void GetUInt24_IList_Endian()
    {
        IList<byte> bytes = [0x78, 0x56, 0x34, 0x12];

        bytes.GetUInt24(0, Endian.Little).Should().Equal(0x345678);
        bytes.GetUInt24(1, Endian.Little).Should().Equal(0x123456);
        bytes.GetUInt24(0, Endian.Big).Should().Equal(0x785634);
        bytes.GetUInt24(1, Endian.Big).Should().Equal(0x563412);
    }

    [Test]
    public void SetUInt24_IList()
    {
        IList<byte> bytes = [0x00, 0x00, 0x00, 0x00];

        bytes.SetUInt24(1, 0x123456);
        bytes.Should().SequenceEqual(0x00, 0x56, 0x34, 0x12);

        bytes.SetUInt24(0, 0x78654321);
        bytes.Should().SequenceEqual(0x21, 0x43, 0x65, 0x12);
    }

    [Test]
    public void SetUInt24_IList_Endian()
    {
        IList<byte> bytes = [0x00, 0x00, 0x00, 0x00];

        bytes.SetUInt24(1, 0x123456, Endian.Little);
        bytes.Should().SequenceEqual(0x00, 0x56, 0x34, 0x12);

        bytes.SetUInt24(0, 0x78654321, Endian.Little);
        bytes.Should().SequenceEqual(0x21, 0x43, 0x65, 0x12);

        bytes.SetUInt24(1, 0x123456, Endian.Big);
        bytes.Should().SequenceEqual(0x21, 0x12, 0x34, 0x56);

        bytes.SetUInt24(0, 0x78654321, Endian.Big);
        bytes.Should().SequenceEqual(0x65, 0x43, 0x21, 0x56);
    }

    [Test]
    public void GetInt32_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1).Should().Equal(0x05040302);
    }

    [Test]
    public void GetInt32_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1, Endian.Little).Should().Equal(0x05040302);
        bytes.GetInt32(2, Endian.Big).Should().Equal(0x03040506);
    }

    [Test]
    public void SetInt32_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetInt32(1, 0x12345678);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);
    }

    [Test]
    public void SetInt32_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetInt32(1, 0x12345678, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);

        bytes.SetInt32(2, 0x56789ABC, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x78, 0x9A, 0xBC);
    }

    [Test]
    public void GetUInt32_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1).Should().Equal(0x05040302U);
    }

    [Test]
    public void GetUInt32_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1, Endian.Little).Should().Equal(0x05040302U);
        bytes.GetUInt32(2, Endian.Big).Should().Equal(0x03040506U);
    }

    [Test]
    public void SetUInt32_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetUInt32(1, 0x12345678U);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);
    }

    [Test]
    public void SetUInt32_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetUInt32(1, 0x12345678U, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);

        bytes.SetUInt32(2, 0x56789ABCU, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x78, 0x9A, 0xBC);
    }

    [Test]
    public void GetInt64_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetInt64(1).Should().Equal(0x0908070605040302L);
    }

    [Test]
    public void GetInt64_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetInt64(1, Endian.Little).Should().Equal(0x0908070605040302L);
        bytes.GetInt64(2, Endian.Big).Should().Equal(0x030405060708090AL);
    }

    [Test]
    public void SetInt64_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A];

        bytes.SetInt64(1, 0x123456789ABCDEF0L);
        bytes.Should().SequenceEqual(0x01, 0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12, 0x0A);
    }

    [Test]
    public void SetInt64_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A];

        bytes.SetInt64(1, 0x123456789ABCDEF0L, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12, 0x0A);

        bytes.SetInt64(2, 0x123456789ABCDEF0L, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0xF0, 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0);
    }

    [Test]
    public void GetUInt64_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetUInt64(1).Should().Equal(0x0908070605040302UL);
    }

    [Test]
    public void GetUInt64_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetUInt64(1, Endian.Little).Should().Equal(0x0908070605040302UL);
        bytes.GetUInt64(2, Endian.Big).Should().Equal(0x030405060708090AUL);
    }

    [Test]
    public void SetUInt64_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A];

        bytes.SetUInt64(1, 0x123456789ABCDEF0UL);
        bytes.Should().SequenceEqual(0x01, 0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12, 0x0A);
    }

    [Test]
    public void SetUInt64_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A];

        bytes.SetUInt64(1, 0x123456789ABCDEF0UL, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0xF0, 0xDE, 0xBC, 0x9A, 0x78, 0x56, 0x34, 0x12, 0x0A);

        bytes.SetUInt64(2, 0x123456789ABCDEF0UL, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0xF0, 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0);
    }
}