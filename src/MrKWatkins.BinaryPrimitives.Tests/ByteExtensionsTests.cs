namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteExtensionsTests
{
    [TestCase(0b00000000, 0b11111111, 0b10101010, 0b10101010)]
    [TestCase(0b00000000, 0b11110000, 0b10101010, 0b10100000)]
    [TestCase(0b10101010, 0b11110000, 0b11110000, 0b11111010)]
    [TestCase(0b10101010, 0b11001100, 0b01011101, 0b11101110)]
    public void CopyBitsFrom(byte input, byte toCopyFrom, byte mask, byte expected) => input.CopyBitsFrom(toCopyFrom, mask).Should().Equal(expected);

    [TestCase(0b0_0000, 0b0_0000, false)]
    [TestCase(0b0_1000, 0b0_0000, false)]
    [TestCase(0b0_0000, 0b0_1000, false)]
    [TestCase(0b0_1000, 0b0_1000, true)]

    [TestCase(0b1_0000, 0b0_0000, false)]
    [TestCase(0b1_1000, 0b0_0000, false)]
    [TestCase(0b1_0000, 0b0_1000, false)]
    [TestCase(0b1_1000, 0b0_1000, true)]

    [TestCase(0b0_0000, 0b1_0000, false)]
    [TestCase(0b0_1000, 0b1_0000, false)]
    [TestCase(0b0_0000, 0b1_1000, false)]
    [TestCase(0b0_1000, 0b1_1000, true)]

    [TestCase(0b1_0000, 0b1_0000, false)]
    [TestCase(0b1_1000, 0b1_0000, false)]
    [TestCase(0b1_0000, 0b1_1000, false)]
    [TestCase(0b1_1000, 0b1_1000, true)]
    public void DidAdditionHalfCarry(byte left, byte right, bool expected)
    {
        var sum = (byte)(left + right);
        sum.DidAdditionHalfCarry(left, right).Should().Equal(expected);
    }

    [TestCase(0, 0, false)]
    [TestCase(127, 0, false)]
    [TestCase(-128, 127, false)]
    [TestCase(-64, 127, false)]
    [TestCase(64, 64, true)]
    [TestCase(127, 1, true)]
    [TestCase(127, 127, true)]
    public void DidAdditionOverflow(int left, int right, bool expected)
    {
        var leftByte = (byte)left;
        var rightByte = (byte)right;
        var sum = (byte)(leftByte + rightByte);
        sum.DidAdditionOverflow(leftByte, rightByte).Should().Equal(expected);
    }

    [TestCase(0b0_0000, 0b0_0000, false)]
    [TestCase(0b0_1000, 0b0_0000, false)]
    [TestCase(0b0_0000, 0b0_1000, true)]
    [TestCase(0b0_1000, 0b0_1000, false)]

    [TestCase(0b1_0000, 0b0_0000, false)]
    [TestCase(0b1_1000, 0b0_0000, false)]
    [TestCase(0b1_0000, 0b0_1000, true)]
    [TestCase(0b1_1000, 0b0_1000, false)]

    [TestCase(0b0_0000, 0b1_0000, false)]
    [TestCase(0b0_1000, 0b1_0000, false)]
    [TestCase(0b0_0000, 0b1_1000, true)]
    [TestCase(0b0_1000, 0b1_1000, false)]

    [TestCase(0b1_0000, 0b1_0000, false)]
    [TestCase(0b1_1000, 0b1_0000, false)]
    [TestCase(0b1_0000, 0b1_1000, true)]
    [TestCase(0b1_1000, 0b1_1000, false)]
    public void DidSubtractionHalfBorrow(byte left, byte right, bool expected)
    {
        var difference = (byte)(left - right);
        difference.DidSubtractionHalfBorrow(left, right).Should().Equal(expected);
    }

    [TestCase(0, 0, false)]
    [TestCase(127, 0, false)]
    [TestCase(64, 64, false)]
    [TestCase(0, -1, false)]
    [TestCase(-1, 1, false)]
    [TestCase(0, 1, false)]
    [TestCase(-64, 64, false)]
    [TestCase(-64, 65, true)]
    [TestCase(127, 128, true)]
    public void DidSubtractionOverflow(int left, int right, bool expected)
    {
        var leftByte = (byte)left;
        var rightByte = (byte)right;
        var difference = (byte)(leftByte - rightByte);
        difference.DidSubtractionOverflow(leftByte, rightByte).Should().Equal(expected);
    }

    [TestCase(0b00000000, 0, false)]
    [TestCase(0b00000001, 0, true)]
    [TestCase(0b00000010, 0, false)]
    [TestCase(0b00000010, 1, true)]
    [TestCase(0b10000010, 7, true)]
    public void GetBit(byte @byte, int index, bool expected) => @byte.GetBit(index).Should().Equal(expected);

    [TestCase(0b11111111, 0, 7, 0b11111111)]
    [TestCase(0b11111111, 1, 6, 0b00111111)]
    [TestCase(0b11100111, 1, 6, 0b00110011)]
    [TestCase(0b00000001, 0, 0, 0b00000001)]
    [TestCase(0b10000000, 7, 7, 0b00000001)]
    [TestCase(0b00001000, 3, 3, 0b00000001)]
    [TestCase(0b00011000, 3, 4, 0b00000011)]
    public void GetBits(byte @byte, int startInclusive, int endExclusive, byte expected) => @byte.GetBits(startInclusive, endExclusive).Should().Equal(expected);

    [TestCase(0, 8)]
    [TestCase(8, 9)]
    [TestCase(-1, 1)]
    [TestCase(6, 2)]
    [TestCase(4, 3)]
    public void GetBits_InvalidRange(int startInclusive, int endExclusive) =>
        AssertThat.Invoking(() => ((byte)255).GetBits(startInclusive, endExclusive))
            .Should().Throw<ArgumentOutOfRangeException>();

    [TestCase(0b00000000, 0b00000000)]
    [TestCase(0b11110000, 0b00001111)]
    [TestCase(0b10101111, 0b00001010)]
    public void HighNibble(byte @byte, byte expected) => @byte.HighNibble().Should().Equal(expected);

    [TestCase(0b00000000, false)]
    [TestCase(0b00000001, false)]
    [TestCase(0b01111111, false)]
    [TestCase(0b10000000, true)]
    [TestCase(0b10000001, true)]
    [TestCase(0b11111111, true)]
    public void LeftMostBit(byte @byte, bool expected) => @byte.LeftMostBit().Should().Equal(expected);

    [TestCase(0b00000000, 0b00000000)]
    [TestCase(0b00001111, 0b00001111)]
    [TestCase(0b11110101, 0b00000101)]
    public void LowNibble(byte @byte, byte expected) => @byte.LowNibble().Should().Equal(expected);

    [TestCase(0b00000000, true)]
    [TestCase(0b00000001, false)]
    [TestCase(0b00000011, true)]
    [TestCase(0b11111110, false)]
    [TestCase(0b11111111, true)]
    public void Parity(byte @byte, bool expected) => @byte.Parity().Should().Equal(expected);

    [TestCase(0b00000000, 0, 0b00000000)]
    [TestCase(0b00000001, 0, 0b00000000)]
    [TestCase(0b00000010, 0, 0b00000010)]
    [TestCase(0b00000010, 1, 0b00000000)]
    [TestCase(0b00000010, 7, 0b00000010)]
    [TestCase(0b10000010, 7, 0b00000010)]
    public void ResetBit(byte @byte, int index, byte expected) => @byte.ResetBit(index).Should().Equal(expected);

    [TestCase(0b00000000, false)]
    [TestCase(0b10000000, false)]
    [TestCase(0b11111110, false)]
    [TestCase(0b00000001, true)]
    [TestCase(0b10000001, true)]
    [TestCase(0b11111111, true)]
    public void RightMostBit(byte @byte, bool expected) => @byte.RightMostBit().Should().Equal(expected);

    [TestCase(0b00000000, 0, 0b00000001)]
    [TestCase(0b00000001, 0, 0b00000001)]
    [TestCase(0b00000010, 0, 0b00000011)]
    [TestCase(0b00000010, 1, 0b00000010)]
    [TestCase(0b00000010, 7, 0b10000010)]
    public void SetBit(byte @byte, int index, byte expected) => @byte.SetBit(index).Should().Equal(expected);

    [TestCase(0b00000000, 0b11111111, 0, 7, 0b11111111)]
    [TestCase(0b10000001, 0b00111111, 1, 6, 0b11111111)]
    [TestCase(0b10000001, 0b00011110, 1, 6, 0b10111101)]
    [TestCase(0b11111111, 0b00011110, 1, 6, 0b10111101)]
    [TestCase(0b10000001, 0b11111111, 4, 4, 0b10010001)]
    [TestCase(0b11111111, 0b11111110, 0, 0, 0b11111110)]
    public void SetBits(byte @byte, byte value, int startInclusive, int endExclusive, byte expected) =>
        @byte.SetBits(value, startInclusive, endExclusive).Should().Equal(expected);

    [TestCase(0, 8)]
    [TestCase(8, 9)]
    [TestCase(-1, 1)]
    [TestCase(6, 2)]
    [TestCase(4, 3)]
    public void SetBits_InvalidRange(int startInclusive, int endExclusive) =>
        AssertThat.Invoking(() => ((byte)255).SetBits(255, startInclusive, endExclusive))
            .Should().Throw<ArgumentOutOfRangeException>();

    [TestCase(0b00000000, 0b00000000, 0b00000000)]
    [TestCase(0b00000000, 0b00001111, 0b11110000)]
    [TestCase(0b01011001, 0b00001111, 0b11111001)]
    [TestCase(0b10101010, 0b11111111, 0b11111010)]
    public void SetHighNibble(byte @byte, byte nibble, byte expected) => @byte.SetHighNibble(nibble).Should().Equal(expected);

    [TestCase(0b00000000, 0b00000000, 0b00000000)]
    [TestCase(0b00000000, 0b00001111, 0b00001111)]
    [TestCase(0b10010101, 0b00001111, 0b10011111)]
    [TestCase(0b10101010, 0b11111111, 0b10101111)]
    public void SetLowNibble(byte @byte, byte nibble, byte expected) => @byte.SetLowNibble(nibble).Should().Equal(expected);

    [TestCase(0b00000000, false)]
    [TestCase(0b00000001, false)]
    [TestCase(0b01111111, false)]
    [TestCase(0b10000000, true)]
    [TestCase(0b10000001, true)]
    [TestCase(0b11111111, true)]
    public void SignBit(byte @byte, bool expected) => @byte.SignBit().Should().Equal(expected);

    [TestCase(0b00000000, "0b00000000")]
    [TestCase(0b00000001, "0b00000001")]
    [TestCase(0b00000010, "0b00000010")]
    [TestCase(0b00110000, "0b00110000")]
    [TestCase(0b10000010, "0b10000010")]
    public void ToBinaryString(byte @byte, string expected) => @byte.ToBinaryString().Should().Equal(expected);
}