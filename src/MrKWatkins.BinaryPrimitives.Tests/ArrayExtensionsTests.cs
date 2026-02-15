using System.Buffers;

namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class ArrayExtensionsTests
{
    [Test]
    public void CreateWrappedSequence_Array()
    {
        byte[] bytes = [1, 2];
        var wrap = bytes.CreateWrappedSequence();

        var reader = new SequenceReader<byte>(wrap);
        reader.TryRead(out var result).Should().BeTrue();
        result.Should().Equal(1);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(2);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(1);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(2);
        reader.TryRead(out _).Should().BeFalse();
    }

    [Test]
    public void CreateWrappedSequence_Array_StartIndex()
    {
        byte[] bytes = [1, 2, 3];
        var wrap = bytes.CreateWrappedSequence(2);

        var reader = new SequenceReader<byte>(wrap);
        reader.TryRead(out var result).Should().BeTrue();
        result.Should().Equal(3);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(1);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(2);
        reader.TryRead(out result).Should().BeTrue();
        result.Should().Equal(3);
        reader.TryRead(out _).Should().BeFalse();
    }
}