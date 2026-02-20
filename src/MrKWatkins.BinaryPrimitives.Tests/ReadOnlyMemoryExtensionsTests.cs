using System.Buffers;

namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ReadOnlyMemoryExtensionsTests
{
    [Test]
    public void CreateWrappedSequence_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 1, 2 };
        var wrap = bytes.CreateWrappedSequence();

        var reader = new SequenceReader<byte>(wrap);
        reader.TryRead(out var result).Should().BeTrue();
        result.Should().Equal(1);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(2);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(1);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(2);
        reader.TryRead(out _).Should().BeFalse();
    }

    [Test]
    public void CreateWrappedSequence_ReadOnlyMemory_StartIndex()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 1, 2, 3 };
        var wrap = bytes.CreateWrappedSequence(2);

        var reader = new SequenceReader<byte>(wrap);
        reader.TryRead(out var result).Should().BeTrue();
        result.Should().Equal(3);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(1);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(2);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(3);
        reader.TryRead(out _).Should().BeFalse();
    }


    [Test]
    public void GetInt16_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        bytes.GetInt16(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetInt16_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        bytes.GetInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetInt16(1, Endian.Big).Should().Equal(0x0203);
    }


    [Test]
    public void GetInt32_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };

        bytes.GetInt32(1).Should().Equal(0x05040302);
    }

    [Test]
    public void GetInt32_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };

        bytes.GetInt32(1, Endian.Little).Should().Equal(0x05040302);
        bytes.GetInt32(2, Endian.Big).Should().Equal(0x03040506);
    }


    [Test]
    public void GetInt64_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B };

        bytes.GetInt64(1).Should().Equal(0x0908070605040302L);
    }

    [Test]
    public void GetInt64_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B };

        bytes.GetInt64(1, Endian.Little).Should().Equal(0x0908070605040302L);
        bytes.GetInt64(2, Endian.Big).Should().Equal(0x030405060708090AL);
    }


    [Test]
    public void GetUInt24_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x78, 0x56, 0x34, 0x12 };

        ((int)bytes.GetUInt24(0)).Should().Equal(0x345678);
        ((int)bytes.GetUInt24(1)).Should().Equal(0x123456);
    }

    [Test]
    public void GetUInt24_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x78, 0x56, 0x34, 0x12 };

        ((int)bytes.GetUInt24(0, Endian.Little)).Should().Equal(0x345678);
        ((int)bytes.GetUInt24(1, Endian.Little)).Should().Equal(0x123456);
        ((int)bytes.GetUInt24(0, Endian.Big)).Should().Equal(0x785634);
        ((int)bytes.GetUInt24(1, Endian.Big)).Should().Equal(0x563412);
    }


    [Test]
    public void GetUInt32_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };

        bytes.GetUInt32(1).Should().Equal(0x05040302U);
    }

    [Test]
    public void GetUInt32_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };

        bytes.GetUInt32(1, Endian.Little).Should().Equal(0x05040302U);
        bytes.GetUInt32(2, Endian.Big).Should().Equal(0x03040506U);
    }


    [Test]
    public void GetUInt64_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B };

        bytes.GetUInt64(1).Should().Equal(0x0908070605040302UL);
    }

    [Test]
    public void GetUInt64_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B };

        bytes.GetUInt64(1, Endian.Little).Should().Equal(0x0908070605040302UL);
        bytes.GetUInt64(2, Endian.Big).Should().Equal(0x030405060708090AUL);
    }


    [Test]
    public void GetUInt16_ReadOnlyMemory()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        bytes.GetUInt16(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetUInt16_ReadOnlyMemory_Endian()
    {
        ReadOnlyMemory<byte> bytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        bytes.GetUInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetUInt16(2, Endian.Big).Should().Equal(0x0304);
    }
}