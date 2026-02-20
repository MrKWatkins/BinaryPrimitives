namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class UInt64ExtensionsTests
{
    [TestCase(0UL, 0, false)]
    [TestCase(1UL, 0, true)]
    [TestCase(2UL, 0, false)]
    [TestCase(2UL, 1, true)]
    [TestCase(0x8000000000000000UL, 63, true)]
    public void GetBit(ulong value, int index, bool expected) => value.GetBit(index).Should().Equal(expected);


    [TestCase(0x0000000000000000UL, false)]
    [TestCase(0x7FFFFFFFFFFFFFFFUL, false)]
    [TestCase(0x8000000000000000UL, true)]
    [TestCase(0xFFFFFFFFFFFFFFFFUL, true)]
    public void LeftMostBit(ulong value, bool expected) => value.LeftMostBit().Should().Equal(expected);


    [TestCase(0b11UL, 0, 0b10UL)]
    [TestCase(0b11UL, 1, 0b01UL)]
    [TestCase(0x8000000000000000UL, 63, 0x0000000000000000UL)]
    public void ResetBit(ulong value, int index, ulong expected) => value.ResetBit(index).Should().Equal(expected);


    [TestCase(0x0000000000000000UL, false)]
    [TestCase(0x0000000000000001UL, true)]
    [TestCase(0x0000000000000002UL, false)]
    [TestCase(0x8000000000000001UL, true)]
    public void RightMostBit(ulong value, bool expected) => value.RightMostBit().Should().Equal(expected);


    [TestCase(0x0000000000000000UL, 0, 0x0000000000000001UL)]
    [TestCase(0x0000000000000001UL, 0, 0x0000000000000001UL)]
    [TestCase(0x0000000000000000UL, 63, 0x8000000000000000UL)]
    public void SetBit(ulong value, int index, ulong expected) => value.SetBit(index).Should().Equal(expected);


    [TestCase(0x0000000000000000UL, false)]
    [TestCase(0x7FFFFFFFFFFFFFFFUL, false)]
    [TestCase(0x8000000000000000UL, true)]
    [TestCase(0xFFFFFFFFFFFFFFFFUL, true)]
    public void SignBit(ulong value, bool expected) => value.SignBit().Should().Equal(expected);


    [TestCase(0x0000000000000000UL, "0b0000000000000000000000000000000000000000000000000000000000000000")]
    [TestCase(0x0102030405060708UL, "0b0000000100000010000000110000010000000101000001100000011100001000")]
    [TestCase(0xFF00000000000000UL, "0b1111111100000000000000000000000000000000000000000000000000000000")]
    [TestCase(0x000000000000FFFFUL, "0b0000000000000000000000000000000000000000000000001111111111111111")]
    public void ToBinaryString(ulong value, string expected) => value.ToBinaryString().Should().Equal(expected);
}