namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class UInt32ExtensionsTests
{
    [Test]
    public void GetUInt32_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1).Should().Equal(0x05040302U);
    }

    [Test]
    public void GetUInt32_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1, Endian.Little).Should().Equal(0x05040302U);
        bytes.GetUInt32(2, Endian.Big).Should().Equal(0x03040506U);
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
    public void GetUInt32_IReadOnlyList()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1).Should().Equal(0x05040302U);
    }

    [Test]
    public void GetUInt32_IReadOnlyList_Endian()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetUInt32(1, Endian.Little).Should().Equal(0x05040302U);
        bytes.GetUInt32(2, Endian.Big).Should().Equal(0x03040506U);
    }

    [Test]
    public void SetUInt32_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetUInt32(1, 0x12345678U);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);

        bytes.SetUInt32(2, 0x56789ABCU);
        bytes.Should().SequenceEqual(0x01, 0x78, 0xBC, 0x9A, 0x78, 0x56);
    }

    [Test]
    public void SetUInt32_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetUInt32(1, 0x12345678U, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x34, 0x12, 0x06);

        bytes.SetUInt32(2, 0x56789ABCU, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x78, 0xBC, 0x9A, 0x78, 0x56);

        bytes.SetUInt32(2, 0x56789ABCU, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x78, 0x56, 0x78, 0x9A, 0xBC);
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
}