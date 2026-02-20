namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class Int32ExtensionsTests
{
    [TestCase(0b00000000, 0, false)]
    [TestCase(0b00000001, 0, true)]
    [TestCase(0b00000010, 0, false)]
    [TestCase(0b00000010, 1, true)]
    [TestCase(0b10000010, 7, true)]
    public void GetBit(int @byte, int index, bool expected) => @byte.GetBit(index).Should().Equal(expected);


    [TestCase(0b00000000, 0, 3, 0b0000)]
    [TestCase(0b00001111, 0, 3, 0b1111)]
    [TestCase(0b00001111, 2, 3, 0b11)]
    [TestCase(0b11110000, 4, 7, 0b1111)]
    [TestCase(0b01010101, 0, 7, 0b01010101)]
    [TestCase(0b10101010, 1, 6, 0b010101)]
    public void GetBits(int value, int startInclusive, int endInclusive, int expected) =>
        value.GetBits(startInclusive, endInclusive).Should().Equal(expected);

    [Test]
    public void GetBits_InvalidRange()
    {
        AssertThat.Invoking(() => 0.GetBits(-1, 3)).Should().Throw<ArgumentOutOfRangeException>();
        AssertThat.Invoking(() => 0.GetBits(0, 32)).Should().Throw<ArgumentOutOfRangeException>();
        AssertThat.Invoking(() => 0.GetBits(5, 3)).Should().Throw<ArgumentOutOfRangeException>();
    }


    [TestCase(0b00000000, 0, 0b00000000)]
    [TestCase(0b00000001, 0, 0b00000000)]
    [TestCase(0b00000011, 1, 0b00000001)]
    [TestCase(0b10000000, 7, 0b00000000)]
    public void ResetBit(int value, int index, int expected) => value.ResetBit(index).Should().Equal(expected);


    [TestCase(0b00000000, 0, 0b00000001)]
    [TestCase(0b00000001, 0, 0b00000001)]
    [TestCase(0b00000001, 1, 0b00000011)]
    [TestCase(0b00000000, 7, 0b10000000)]
    public void SetBit(int value, int index, int expected) => value.SetBit(index).Should().Equal(expected);


    [TestCase(0b00000000, 0b1111, 0, 3, 0b00001111)]
    [TestCase(0b11111111, 0b0000, 0, 3, 0b11110000)]
    [TestCase(0b00000000, 0b1111, 4, 7, 0b11110000)]
    [TestCase(0b11111111, 0b0101, 0, 3, 0b11110101)]
    public void SetBits(int value, int bits, int startInclusive, int endInclusive, int expected) =>
        value.SetBits(bits, startInclusive, endInclusive).Should().Equal(expected);

    [Test]
    public void SetBits_InvalidRange()
    {
        AssertThat.Invoking(() => 0.SetBits(0, -1, 3)).Should().Throw<ArgumentOutOfRangeException>();
        AssertThat.Invoking(() => 0.SetBits(0, 0, 32)).Should().Throw<ArgumentOutOfRangeException>();
        AssertThat.Invoking(() => 0.SetBits(0, 5, 3)).Should().Throw<ArgumentOutOfRangeException>();
    }


    [TestCase(0x00000000, false)]
    [TestCase(0x7FFFFFFF, false)]
    [TestCase(unchecked((int)0x80000000), true)]
    [TestCase(unchecked((int)0xFFFFFFFF), true)]
    public void SignBit(int value, bool expected) => value.SignBit().Should().Equal(expected);


    [TestCase(0x00000000, false)]
    [TestCase(0x7FFFFFFF, false)]
    [TestCase(unchecked((int)0x80000000), true)]
    [TestCase(unchecked((int)0xFFFFFFFF), true)]
    public void LeftMostBit(int value, bool expected) => value.LeftMostBit().Should().Equal(expected);


    [TestCase(0b00000000, false)]
    [TestCase(0b00000001, true)]
    [TestCase(0b00000010, false)]
    [TestCase(0b00000011, true)]
    public void RightMostBit(int value, bool expected) => value.RightMostBit().Should().Equal(expected);


    [TestCase(0x00000000, "0b00000000000000000000000000000000")]
    [TestCase(0x01020304, "0b00000001000000100000001100000100")]
    [TestCase(unchecked((int)0xFF000000), "0b11111111000000000000000000000000")]
    [TestCase(0x0000FFFF, "0b00000000000000001111111111111111")]
    public void ToBinaryString(int value, string expected) => value.ToBinaryString().Should().Equal(expected);
}