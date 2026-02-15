namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteListExtensionsTests
{
    [Test]
    public void GetInt16_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetInt16_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetInt16(1, Endian.Big).Should().Equal(0x0203);
    }

    [Test]
    public void GetInt32_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1).Should().Equal(0x05040302);
    }

    [Test]
    public void GetInt32_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1, Endian.Little).Should().Equal(0x05040302);
        bytes.GetInt32(2, Endian.Big).Should().Equal(0x03040506);
    }

    [Test]
    public void GetInt64_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetInt64(1).Should().Equal(0x0908070605040302L);
    }

    [Test]
    public void GetInt64_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetInt64(1, Endian.Little).Should().Equal(0x0908070605040302L);
        bytes.GetInt64(2, Endian.Big).Should().Equal(0x030405060708090AL);
    }

    [Test]
    public void GetUInt24_List()
    {
        List<byte> bytes = [0x78, 0x56, 0x34, 0x12];

        bytes.GetUInt24(0).Should().Equal(0x345678);
        bytes.GetUInt24(1).Should().Equal(0x123456);
    }

    [Test]
    public void GetUInt24_List_Endian()
    {
        List<byte> bytes = [0x78, 0x56, 0x34, 0x12];

        bytes.GetUInt24(0, Endian.Little).Should().Equal(0x345678);
        bytes.GetUInt24(1, Endian.Little).Should().Equal(0x123456);
        bytes.GetUInt24(0, Endian.Big).Should().Equal(0x785634);
        bytes.GetUInt24(1, Endian.Big).Should().Equal(0x563412);
    }

    [Test]
    public void GetUInt32_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1).Should().Equal(0x05040302U);
    }

    [Test]
    public void GetUInt32_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1, Endian.Little).Should().Equal(0x05040302U);
        bytes.GetUInt32(2, Endian.Big).Should().Equal(0x03040506U);
    }

    [Test]
    public void GetUInt64_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetUInt64(1).Should().Equal(0x0908070605040302UL);
    }

    [Test]
    public void GetUInt64_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B];

        bytes.GetUInt64(1, Endian.Little).Should().Equal(0x0908070605040302UL);
        bytes.GetUInt64(2, Endian.Big).Should().Equal(0x030405060708090AUL);
    }

    [Test]
    public void GetWord_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetWord_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetWord(2, Endian.Big).Should().Equal(0x0304);
    }
}