namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class Int16ExtensionsTests
{
    [Test]
    public void GetInt16_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetInt16_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetInt16(1, Endian.Big).Should().Equal(0x0203);
    }

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
    public void GetInt16_IReadOnlyList()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetInt16_IReadOnlyList_Endian()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetInt16(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetInt16(1, Endian.Big).Should().Equal(0x0203);
    }

    [Test]
    public void SetInt16_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetInt16(1, 0x1234);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);
    }

    [Test]
    public void SetInt16_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetInt16(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x04);

        bytes.SetInt16(1, 0x5678, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x56, 0x78, 0x04);
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
}