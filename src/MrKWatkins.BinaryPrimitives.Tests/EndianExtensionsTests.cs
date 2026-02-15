namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class EndianExtensionsTests
{
    [TestCase(Endian.Big, 0x12, 0x34, 0x56, 0x00123456)]
    [TestCase(Endian.Little, 0x12, 0x34, 0x56, 0x00563412)]
    public void ToUInt24(Endian endian, byte msb, byte mid, byte lsb, int expected) =>
        endian.ToUInt24(msb, mid, lsb).Should().Equal(expected);


    [Test]
    public void ToWord()
    {
        Endian.Little.ToWord(0x12, 0x34).Should().Equal(0x3412);
        Endian.Big.ToWord(0x12, 0x34).Should().Equal(0x1234);
    }
}