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

        ((int)bytes.GetUInt24(0)).Should().Equal(0x345678);
        ((int)bytes.GetUInt24(1)).Should().Equal(0x123456);
    }


    [Test]
    public void GetUInt24_List_Endian()
    {
        List<byte> bytes = [0x78, 0x56, 0x34, 0x12];

        ((int)bytes.GetUInt24(0, Endian.Little)).Should().Equal(0x345678);
        ((int)bytes.GetUInt24(1, Endian.Little)).Should().Equal(0x123456);
        ((int)bytes.GetUInt24(0, Endian.Big)).Should().Equal(0x785634);
        ((int)bytes.GetUInt24(1, Endian.Big)).Should().Equal(0x563412);
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
    public void GetUInt16_List()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetUInt16(1).Should().Equal(0x0302);
    }


    [Test]
    public void GetUInt16_List_Endian()
    {
        List<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetUInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetUInt16(2, Endian.Big).Should().Equal(0x0304);
    }


    [Test]
    public void SetInt16_List()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x04];

        bytes.SetInt16(1, 0x1234);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);
    }

    [Test]
    public void SetInt16_List_Endian()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x04];

        bytes.SetInt16(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);

        bytes.SetInt16(1, 0x1234, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x12, 0x34, 0x04);
    }


    [Test]
    public void SetInt32_List()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x06];

        bytes.SetInt32(1, 0x12345678);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);
    }

    [Test]
    public void SetInt32_List_Endian()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x06];

        bytes.SetInt32(1, 0x12345678, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);

        bytes.SetInt32(1, 0x12345678, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x12, 0x34, 0x56, 0x78, 0x06);
    }


    [Test]
    public void SetInt64_List()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A];

        bytes.SetInt64(1, 0x1234567890ABCDEF);
        bytes.Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x0A);
    }

    [Test]
    public void SetInt64_List_Endian()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A];

        bytes.SetInt64(1, 0x1234567890ABCDEF, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x0A);

        bytes.SetInt64(1, 0x1234567890ABCDEF, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x0A);
    }


    [Test]
    public void SetUInt24_List()
    {
        List<byte> bytes = [0x78, 0x00, 0x00, 0x00, 0x12];

        bytes.SetUInt24(1, (UInt24)0x123456);
        bytes.Should().SequenceEqual(0x78, 0x56, 0x34, 0x12, 0x12);
    }

    [Test]
    public void SetUInt24_List_Endian()
    {
        List<byte> bytes = [0x78, 0x00, 0x00, 0x00, 0x12];

        bytes.SetUInt24(1, (UInt24)0x123456, Endian.Little);
        bytes.Should().SequenceEqual(0x78, 0x56, 0x34, 0x12, 0x12);

        bytes.SetUInt24(1, (UInt24)0x123456, Endian.Big);
        bytes.Should().SequenceEqual(0x78, 0x12, 0x34, 0x56, 0x12);
    }


    [Test]
    public void SetUInt32_List()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x06];

        bytes.SetUInt32(1, 0x12345678U);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);
    }

    [Test]
    public void SetUInt32_List_Endian()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x06];

        bytes.SetUInt32(1, 0x12345678U, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);

        bytes.SetUInt32(1, 0x12345678U, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x12, 0x34, 0x56, 0x78, 0x06);
    }


    [Test]
    public void SetUInt64_List()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A];

        bytes.SetUInt64(1, 0x1234567890ABCDEFUL);
        bytes.Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x0A);
    }

    [Test]
    public void SetUInt64_List_Endian()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A];

        bytes.SetUInt64(1, 0x1234567890ABCDEFUL, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0xEF, 0xCD, 0xAB, 0x90, 0x78, 0x56, 0x34, 0x12, 0x0A);

        bytes.SetUInt64(1, 0x1234567890ABCDEFUL, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x0A);
    }


    [Test]
    public void SetUInt16_List()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x04];

        bytes.SetUInt16(1, 0x1234);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);
    }

    [Test]
    public void SetUInt16_List_Endian()
    {
        List<byte> bytes = [0x01, 0x00, 0x00, 0x04];

        bytes.SetUInt16(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);

        bytes.SetUInt16(1, 0x1234, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x12, 0x34, 0x04);
    }
}