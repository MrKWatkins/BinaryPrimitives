namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class Int64ExtensionsTests
{
    [TestCase(0L, 0, false)]
    [TestCase(1L, 0, true)]
    [TestCase(2L, 0, false)]
    [TestCase(2L, 1, true)]
    [TestCase(unchecked((long)0x8000000000000000), 63, true)]
    public void GetBit(long value, int index, bool expected) => value.GetBit(index).Should().Equal(expected);


    [TestCase(0x0000000000000000L, false)]
    [TestCase(0x7FFFFFFFFFFFFFFFL, false)]
    [TestCase(unchecked((long)0x8000000000000000), true)]
    [TestCase(-1L, true)]
    public void LeftMostBit(long value, bool expected) => value.LeftMostBit().Should().Equal(expected);


    [TestCase(0b11L, 0, 0b10L)]
    [TestCase(0b11L, 1, 0b01L)]
    [TestCase(unchecked((long)0x8000000000000000), 63, 0x0000000000000000L)]
    public void ResetBit(long value, int index, long expected) => value.ResetBit(index).Should().Equal(expected);


    [TestCase(0x0000000000000000L, false)]
    [TestCase(0x0000000000000001L, true)]
    [TestCase(0x0000000000000002L, false)]
    [TestCase(unchecked((long)0x8000000000000001), true)]
    public void RightMostBit(long value, bool expected) => value.RightMostBit().Should().Equal(expected);


    [TestCase(0x0000000000000000L, 0, 0x0000000000000001L)]
    [TestCase(0x0000000000000001L, 0, 0x0000000000000001L)]
    [TestCase(0x0000000000000000L, 63, unchecked((long)0x8000000000000000))]
    public void SetBit(long value, int index, long expected) => value.SetBit(index).Should().Equal(expected);


    [TestCase(0x0000000000000000L, false)]
    [TestCase(0x7FFFFFFFFFFFFFFFL, false)]
    [TestCase(unchecked((long)0x8000000000000000), true)]
    [TestCase(-1L, true)]
    public void SignBit(long value, bool expected) => value.SignBit().Should().Equal(expected);


    [TestCase(0x0000000000000000L, "0b0000000000000000000000000000000000000000000000000000000000000000")]
    [TestCase(0x0102030405060708L, "0b0000000100000010000000110000010000000101000001100000011100001000")]
    [TestCase(unchecked((long)0xFF00000000000000), "0b1111111100000000000000000000000000000000000000000000000000000000")]
    [TestCase(0x000000000000FFFFL, "0b0000000000000000000000000000000000000000000000001111111111111111")]
    public void ToBinaryString(long value, string expected) => value.ToBinaryString().Should().Equal(expected);
}