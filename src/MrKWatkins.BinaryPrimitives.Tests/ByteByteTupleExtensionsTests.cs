namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteByteTupleExtensionsTests
{
    [Test]
    public void ToWord()
    {
        ((byte)0x12, (byte)0x34).ToWord().Should().Equal(0x3412);
        ((byte)0x12, (byte)0x34).ToWord(Endian.Big).Should().Equal(0x1234);
    }
}