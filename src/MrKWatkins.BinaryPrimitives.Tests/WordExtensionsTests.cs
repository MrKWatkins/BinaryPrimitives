namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class WordExtensionsTests
{
    [TestCase(0b00000000_00000000, 0, false)]
    [TestCase(0b00000000_00000001, 0, true)]
    [TestCase(0b00000000_00000010, 0, false)]
    [TestCase(0b00000000_00000010, 1, true)]
    [TestCase(0b00000000_10000010, 7, true)]
    [TestCase(0b00000001_00000010, 8, true)]
    [TestCase(0b10000001_00000000, 15, true)]
    public void GetBit(int word, int index, bool expected) => ((ushort)word).GetBit(index).Should().Equal(expected);

    [Test]
    public void MostSignificantByte() => ((ushort)0x1234).MostSignificantByte().Should().Equal(0x12);

    [Test]
    public void LeastSignificantByte() => ((ushort)0x1234).LeastSignificantByte().Should().Equal(0x34);

    [Test]
    public void ToBytes() => ((ushort)0x1234).ToBytes().Should().Equal((0x12, 0x34));

    [TestCase(0b00000000_00000000, false)]
    [TestCase(0b00000000_10000000, false)]
    [TestCase(0b00000001_00000000, false)]
    [TestCase(0b01111111_11111111, false)]
    [TestCase(0b10000000_00000000, true)]
    [TestCase(0b10000001_00000000, true)]
    [TestCase(0b10000000_10000000, true)]
    [TestCase(0b11111111_11111111, true)]
    public void SignBit(int word, bool expected) => ((ushort)word).SignBit().Should().Equal(expected);

    [Test]
    public void ToWord_Tuple()
    {
        ((byte)0x12, (byte)0x34).ToWord().Should().Equal(0x3412);
        ((byte)0x12, (byte)0x34).ToWord(Endian.Big).Should().Equal(0x1234);
    }

    [Test]
    public void ToWord_Endian()
    {
        Endian.Little.ToWord(0x12, 0x34).Should().Equal(0x3412);
        Endian.Big.ToWord(0x12, 0x34).Should().Equal(0x1234);
    }

    [Test]
    public void AddWord()
    {
        List<byte> bytes = [0x01];

        bytes.AddWord(0x1234);
        bytes.Should().SequenceEqual(new byte[] { 0x01, 0x34, 0x12 });

        bytes.AddWord(0x5678, Endian.Big);
        bytes.Should().SequenceEqual(new byte[] { 0x01, 0x34, 0x12, 0x56, 0x78 });
    }

    [Test]
    public void GetWord_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetWord_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetWord(2, Endian.Big).Should().Equal(0x0304);
    }

    [Test]
    public void GetWord_ReadOnlySpan()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02];

        bytes.GetWord().Should().Equal(0x0201);
    }

    [Test]
    public void GetWord_ReadOnlySpan_Endian()
    {
        ReadOnlySpan<byte> bytes = [0x01, 0x02];

        bytes.GetWord(Endian.Little).Should().Equal(0x0201);
        bytes.GetWord(Endian.Big).Should().Equal(0x0102);
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

    [Test]
    public void GetWord_IReadOnlyList()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1).Should().Equal(0x0302);
    }

    [Test]
    public void GetWord_IReadOnlyList_Endian()
    {
        IReadOnlyList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.GetWord(1, Endian.Little).Should().Equal(0x0302);
        bytes.GetWord(2, Endian.Big).Should().Equal(0x0304);
    }

    [Test]
    public void SetWord_Array()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetWord(1, 0x1234);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x12, 0x04]);

        bytes.SetWord(2, 0x5678);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x78, 0x56]);
    }

    [Test]
    public void SetWord_Array_Endian()
    {
        byte[] bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetWord(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x12, 0x04]);

        bytes.SetWord(2, 0x5678, Endian.Little);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x78, 0x56]);

        bytes.SetWord(2, 0x5678, Endian.Big);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x56, 0x78]);
    }

    [Test]
    public void SetWord_Span()
    {
        Span<byte> bytes = [0xFF, 0xFE];

        bytes.SetWord(0x1234);
        bytes.ToArray().Should().SequenceEqual([0x34, 0x12]);
    }

    [Test]
    public void SetWord_Span_Endian()
    {
        Span<byte> bytes = [0xFF, 0xFE];

        bytes.SetWord(0x1234, Endian.Little);
        bytes.ToArray().Should().SequenceEqual([0x34, 0x12]);

        bytes.SetWord(0x1234, Endian.Big);
        bytes.ToArray().Should().SequenceEqual([0x12, 0x34]);
    }

    [Test]
    public void SetWord_IList()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetWord(1, 0x1234);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x12, 0x04]);
    }

    [Test]
    public void SetWord_IList_Endian()
    {
        IList<byte> bytes = [0x01, 0x02, 0x03, 0x04];

        bytes.SetWord(1, 0x1234, Endian.Little);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x12, 0x04]);

        bytes.SetWord(2, 0x5678, Endian.Big);
        bytes.Should().SequenceEqual([0x01, 0x34, 0x56, 0x78]);
    }

    [TestCase(0, 0, false)]
    [TestCase(32767, 0, false)]
    [TestCase(-32768, 32767, false)]
    [TestCase(-16384, 32767, false)]
    [TestCase(16384, 16384, true)]
    [TestCase(32767, 1, true)]
    [TestCase(32767, 32767, true)]
    public void DidAdditionOverflow(int left, int right, bool expected)
    {
        var leftWord = (ushort)left;
        var rightWord = (ushort)right;
        var sum = (ushort)(leftWord + rightWord);
        sum.DidAdditionOverflow(leftWord, rightWord).Should().Equal(expected);
    }

    [TestCase(0, 0, false)]
    [TestCase(32767, 0, false)]
    [TestCase(-32768, 32767, true)]
    [TestCase(-16384, 32767, true)]
    [TestCase(16384, 16384, false)]
    [TestCase(32767, 1, false)]
    [TestCase(32767, 32767, false)]
    public void DidSubtractionOverflow(int left, int right, bool expected)
    {
        var leftWord = (ushort)left;
        var rightWord = (ushort)right;
        var sum = (ushort)(leftWord - rightWord);
        sum.DidSubtractionOverflow(leftWord, rightWord).Should().Equal(expected);
    }

    [TestCase(0b0000_0000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0000_1000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0000_0000_0000_0000, 0b0000_1000_0000_0000, false)]
    [TestCase(0b0000_1000_0000_0000, 0b0000_1000_0000_0000, true)]

    [TestCase(0b0001_0000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0001_1000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0001_0000_0000_0000, 0b0000_1000_0000_0000, false)]
    [TestCase(0b0001_1000_0000_0000, 0b0000_1000_0000_0000, true)]

    [TestCase(0b0000_0000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0000_1000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0000_0000_0000_0000, 0b0001_1000_0000_0000, false)]
    [TestCase(0b0000_1000_0000_0000, 0b0001_1000_0000_0000, true)]

    [TestCase(0b0001_0000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0001_1000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0001_0000_0000_0000, 0b0001_1000_0000_0000, false)]
    [TestCase(0b0001_1000_0000_0000, 0b0001_1000_0000_0000, true)]
    public void DidAdditionHalfCarry(int left, int right, bool expected)
    {
        var leftWord = (ushort)left;
        var rightWord = (ushort)right;
        var sum = (ushort)(leftWord + rightWord);
        sum.DidAdditionHalfCarry(leftWord, rightWord).Should().Equal(expected);
    }

    [TestCase(0b0000_0000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0000_1000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0000_0000_0000_0000, 0b0000_1000_0000_0000, true)]
    [TestCase(0b0000_1000_0000_0000, 0b0000_1000_0000_0000, false)]

    [TestCase(0b0001_0000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0001_1000_0000_0000, 0b0000_0000_0000_0000, false)]
    [TestCase(0b0001_0000_0000_0000, 0b0000_1000_0000_0000, true)]
    [TestCase(0b0001_1000_0000_0000, 0b0000_1000_0000_0000, false)]

    [TestCase(0b0000_0000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0000_1000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0000_0000_0000_0000, 0b0001_1000_0000_0000, true)]
    [TestCase(0b0000_1000_0000_0000, 0b0001_1000_0000_0000, false)]

    [TestCase(0b0001_0000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0001_1000_0000_0000, 0b0001_0000_0000_0000, false)]
    [TestCase(0b0001_0000_0000_0000, 0b0001_1000_0000_0000, true)]
    [TestCase(0b0001_1000_0000_0000, 0b0001_1000_0000_0000, false)]
    public void DidSubtractionHalfBorrow(int left, int right, bool expected)
    {
        var leftWord = (ushort)left;
        var rightWord = (ushort)right;
        var difference = (ushort)(leftWord - rightWord);
        difference.DidSubtractionHalfBorrow(leftWord, rightWord).Should().Equal(expected);
    }
}