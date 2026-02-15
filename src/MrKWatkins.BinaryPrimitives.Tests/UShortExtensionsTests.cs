namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class UShortExtensionsTests
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