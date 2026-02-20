namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class EndianExtensionsTests
{
    [TestCase(Endian.Big, 0x12, 0x34, 0x56, 0x00123456)]
    [TestCase(Endian.Little, 0x12, 0x34, 0x56, 0x00563412)]
    public void ToUInt24(Endian endian, byte byte0, byte byte1, byte byte2, int expected) =>
        endian.ToUInt24(byte0, byte1, byte2).Should().Equal(expected);


    [Test]
    public void ToUInt16()
    {
        Endian.Little.ToUInt16(0x12, 0x34).Should().Equal(0x3412);
        Endian.Big.ToUInt16(0x12, 0x34).Should().Equal(0x1234);
    }
}