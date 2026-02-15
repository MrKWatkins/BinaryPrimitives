namespace MrKWatkins.BinaryPrimitives.Tests;

[TestOf(typeof(ByteIReadOnlyListExtensions))]
public sealed class ReadOnlyListExtensionsTests
{
    [Test]
    public void CopyTo_Span()
    {
        IReadOnlyList<byte> source = [1, 2, 3, 4, 5];
        var destination = new byte[10];
        source.CopyTo(destination);
        destination.Should().SequenceEqual(1, 2, 3, 4, 5, 0, 0, 0, 0, 0);
    }

    [Test]
    public void CopyTo_Span_NotEnoughSpace()
    {
        IReadOnlyList<byte> source = [1, 2, 3, 4, 5];
        var destination = new byte[1];
        source.Invoking(s => s.CopyTo(destination)).Should().Throw<ArgumentException>();
    }

    [Test]
    public void CopyTo_Span_Int()
    {
        IReadOnlyList<byte> source = [1, 2, 3, 4, 5];
        var destination = new byte[10];
        source.CopyTo(destination, 3);
        destination.Should().SequenceEqual(0, 0, 0, 1, 2, 3, 4, 5, 0, 0);
    }

    [Test]
    public void CopyTo_Span_NotEnoughSpace_Int()
    {
        IReadOnlyList<byte> source = [1, 2, 3, 4, 5];
        var destination = new byte[6];
        source.Invoking(s => s.CopyTo(destination, 3)).Should().Throw<ArgumentException>();
    }
}