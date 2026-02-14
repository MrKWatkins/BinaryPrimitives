namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class BoolExtensionsTests
{
    [TestCase(false, '0')]
    [TestCase(true, '1')]
    public void ToBitChar(bool value, char expected) => value.ToBitChar().Should().Equal(expected);
}