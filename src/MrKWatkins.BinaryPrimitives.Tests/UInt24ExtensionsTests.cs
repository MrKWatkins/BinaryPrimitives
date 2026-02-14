namespace MrKWatkins.BinaryPrimitives.Tests;

[SuppressMessage("ReSharper", "UseUtf8StringLiteral")]
public sealed class UInt24ExtensionsTests
{
    [TestCase(Endian.Big, 0x12, 0x34, 0x56, 0x00123456)]
    [TestCase(Endian.Little, 0x12, 0x34, 0x56, 0x00563412)]
    public void ToUInt24(Endian endian, byte msb, byte mid, byte lsb, int expected) =>
        endian.ToUInt24(msb, mid, lsb).Should().Equal(expected);

    [Test]
    public void GetUInt24_Array()
    {
        byte[] bytes = [0x78, 0x56, 0x34, 0x12];

        bytes.GetUInt24(0).Should().Equal(0x345678);
        bytes.GetUInt24(1).Should().Equal(0x123456);
    }

    [Test]
    public void GetUInt24_Array_Endian()
    {
        byte[] bytes = [0x78, 0x56, 0x34, 0x12];

        bytes.GetUInt24(0, Endian.Little).Should().Equal(0x345678);
        bytes.GetUInt24(1, Endian.Little).Should().Equal(0x123456);
        bytes.GetUInt24(0, Endian.Big).Should().Equal(0x785634);
        bytes.GetUInt24(1, Endian.Big).Should().Equal(0x563412);
    }

    [Test]
    public void SetUInt24_Array()
    {
        byte[] bytes = [0x00, 0x00, 0x00, 0x00];

        bytes.SetUInt24(1, 0x123456);
        bytes.Should().SequenceEqual([0x00, 0x56, 0x34, 0x12]);

        bytes.SetUInt24(0, 0x78654321);
        bytes.Should().SequenceEqual([0x21, 0x43, 0x65, 0x12]);
    }

    [Test]
    public void SetUInt24_Array_Endian()
    {
        byte[] bytes = [0x00, 0x00, 0x00, 0x00];

        bytes.SetUInt24(1, 0x123456, Endian.Little);
        bytes.Should().SequenceEqual([0x00, 0x56, 0x34, 0x12]);

        bytes.SetUInt24(0, 0x78654321, Endian.Little);
        bytes.Should().SequenceEqual([0x21, 0x43, 0x65, 0x12]);

        bytes.SetUInt24(1, 0x123456, Endian.Big);
        bytes.Should().SequenceEqual([0x21, 0x12, 0x34, 0x56]);

        bytes.SetUInt24(0, 0x78654321, Endian.Big);
        bytes.Should().SequenceEqual([0x65, 0x43, 0x21, 0x56]);
    }
}