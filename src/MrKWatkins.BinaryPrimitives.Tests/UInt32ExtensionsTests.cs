namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class UInt32ExtensionsTests
{
    [TestCase(0u, 0, false)]
    [TestCase(1u, 0, true)]
    [TestCase(2u, 0, false)]
    [TestCase(2u, 1, true)]
    [TestCase(0x80000000u, 31, true)]
    public void GetBit(uint value, int index, bool expected) => value.GetBit(index).Should().Equal(expected);


    [TestCase(0x00000000u, false)]
    [TestCase(0x7FFFFFFFu, false)]
    [TestCase(0x80000000u, true)]
    [TestCase(0xFFFFFFFFu, true)]
    public void LeftMostBit(uint value, bool expected) => value.LeftMostBit().Should().Equal(expected);


    [TestCase(0b00000011u, 0, 0b00000010u)]
    [TestCase(0b00000011u, 1, 0b00000001u)]
    [TestCase(0x80000000u, 31, 0x00000000u)]
    public void ResetBit(uint value, int index, uint expected) => value.ResetBit(index).Should().Equal(expected);


    [TestCase(0x00000000u, false)]
    [TestCase(0x00000001u, true)]
    [TestCase(0x00000002u, false)]
    [TestCase(0x80000001u, true)]
    public void RightMostBit(uint value, bool expected) => value.RightMostBit().Should().Equal(expected);


    [TestCase(0x00000000u, 0, 0x00000001u)]
    [TestCase(0x00000001u, 0, 0x00000001u)]
    [TestCase(0x00000000u, 31, 0x80000000u)]
    public void SetBit(uint value, int index, uint expected) => value.SetBit(index).Should().Equal(expected);


    [TestCase(0x00000000u, false)]
    [TestCase(0x7FFFFFFFu, false)]
    [TestCase(0x80000000u, true)]
    [TestCase(0xFFFFFFFFu, true)]
    public void SignBit(uint value, bool expected) => value.SignBit().Should().Equal(expected);


    [TestCase(0x00000000u, "0b00000000000000000000000000000000")]
    [TestCase(0x01020304u, "0b00000001000000100000001100000100")]
    [TestCase(0xFF000000u, "0b11111111000000000000000000000000")]
    [TestCase(0x0000FFFFu, "0b00000000000000001111111111111111")]
    public void ToBinaryString(uint value, string expected) => value.ToBinaryString().Should().Equal(expected);
}