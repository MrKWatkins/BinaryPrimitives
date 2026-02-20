namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteByteTupleExtensionsTests
{
    [Test]
    public void ToUInt16()
    {
        ((byte)0x12, (byte)0x34).ToUInt16().Should().Equal(0x3412);
        ((byte)0x12, (byte)0x34).ToUInt16(Endian.Big).Should().Equal(0x1234);
    }
}