namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ByteICollectionExtensionsTests
{
    [Test]
    public void AddWord()
    {
        List<byte> bytes = [0x01];

        bytes.AddWord(0x1234);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12);

        bytes.AddWord(0x5678, Endian.Big);
        bytes.Should().SequenceEqual(0x01, 0x34, 0x12, 0x56, 0x78);
    }
}