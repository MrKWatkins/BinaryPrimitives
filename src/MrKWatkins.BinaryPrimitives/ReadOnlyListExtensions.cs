using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

public static class ReadOnlyListExtensions
{
    public static void CopyTo(this IReadOnlyList<byte> source, Span<byte> destination, int start) => source.CopyTo(destination[start..]);

    public static void CopyTo(this IReadOnlyList<byte> source, Span<byte> destination)
    {
        if (source.Count > destination.Length)
        {
            throw new ArgumentException("Value does not have enough space to copy {nameof(source)}.", nameof(destination));
        }

        ref var reference = ref MemoryMarshal.GetReference(destination);
        foreach (var @byte in source)
        {
            reference = @byte;
            reference = ref Unsafe.Add(ref reference, sizeof(byte));
        }
    }
}