namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class IntExtensionsTests
{
    [TestCase(0b00000000, 0, false)]
    [TestCase(0b00000001, 0, true)]
    [TestCase(0b00000010, 0, false)]
    [TestCase(0b00000010, 1, true)]
    [TestCase(0b10000010, 7, true)]
    public void GetBit(int @byte, int index, bool expected) => @byte.GetBit(index).Should().Equal(expected);
}