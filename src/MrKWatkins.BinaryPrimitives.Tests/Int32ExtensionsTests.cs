namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class Int32ExtensionsTests
{
    [Test]
    public void GetInt32_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1).Should().Equal(0x05040302);
    }

    [Test]
    public void GetInt32_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1, Endian.Little).Should().Equal(0x05040302);
        bytes.GetInt32(2, Endian.Big).Should().Equal(0x03040506);
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
    public void GetInt32_IReadOnlyList()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1).Should().Equal(0x05040302);
    }

    [Test]
    public void GetInt32_IReadOnlyList_Endian()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.GetInt32(1, Endian.Little).Should().Equal(0x05040302);
        bytes.GetInt32(2, Endian.Big).Should().Equal(0x03040506);
    }

    [Test]
    public void SetInt32_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetInt32(1, 0x12345678);
        bytes.Should().SequenceEqual([0x01, 0x78, 0x56, 0x34, 0x12, 0x06]);

        bytes.SetInt32(2, 0x56789ABC);
        bytes.Should().SequenceEqual([0x01, 0x78, 0xBC, 0x9A, 0x78, 0x56]);
    }

    [Test]
    public void SetInt32_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetInt32(1, 0x12345678, Endian.Little);
        bytes.Should().SequenceEqual([0x01, 0x78, 0x56, 0x34, 0x12, 0x06]);

        bytes.SetInt32(2, 0x56789ABC, Endian.Little);
        bytes.Should().SequenceEqual([0x01, 0x78, 0xBC, 0x9A, 0x78, 0x56]);

        bytes.SetInt32(2, 0x56789ABC, Endian.Big);
        bytes.Should().SequenceEqual([0x01, 0x78, 0x56, 0x78, 0x9A, 0xBC]);
    }

    [Test]
    public void SetInt32_Span()
    {
        Span<byte> bytes = [0xFF, 0xFE, 0xFD, 0xFC];

        bytes.SetInt32(0x12345678);
        bytes.ToArray().Should().SequenceEqual([0x78, 0x56, 0x34, 0x12]);
    }

    [Test]
    public void SetInt32_Span_Endian()
    {
        Span<byte> bytes = [0xFF, 0xFE, 0xFD, 0xFC];

        bytes.SetInt32(0x12345678, Endian.Little);
        bytes.ToArray().Should().SequenceEqual([0x78, 0x56, 0x34, 0x12]);

        bytes.SetInt32(0x12345678, Endian.Big);
        bytes.ToArray().Should().SequenceEqual([0x12, 0x34, 0x56, 0x78]);
    }

    [Test]
    public void SetInt32_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetInt32(1, 0x12345678);
        bytes.Should().SequenceEqual([0x01, 0x78, 0x56, 0x34, 0x12, 0x06]);
    }

    [Test]
    public void SetInt32_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04, 0x05, 0x06];

        bytes.SetInt32(1, 0x12345678, Endian.Little);
        bytes.Should().SequenceEqual([0x01, 0x78, 0x56, 0x34, 0x12, 0x06]);

        bytes.SetInt32(2, 0x56789ABC, Endian.Big);
        bytes.Should().SequenceEqual([0x01, 0x78, 0x56, 0x78, 0x9A, 0xBC]);
    }
}