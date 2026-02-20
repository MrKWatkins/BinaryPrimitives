using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace MrKWatkins.BinaryPrimitives;

// Writes the binary string representation of bytes into a span using SIMD,
// with one lane per bit processed in parallel. Three widths are supported:
//   WriteByteChars   — 1 byte  →  8 chars  (Vector128)
//   WriteUInt16Chars — 2 bytes → 16 chars  (Vector256)
//   WriteUInt32Chars — 4 bytes → 32 chars  (Vector512)
internal static class BinaryStringHelper
{
    // One lane per bit, with each lane holding the single-bit mask for that bit position,
    // ordered from MSB (bit 7) to LSB (bit 0) so the output reads left-to-right.
    private static readonly Vector128<ushort> Masks = Vector128.Create(
        (ushort)0b10000000, 0b01000000, 0b00100000, 0b00010000,
        0b00001000, 0b00000100, 0b00000010, 0b00000001);

    // Same pattern extended to 16 lanes (bits 15 to 0) for writing a ushort in one go.
    private static readonly Vector256<ushort> Masks256 = Vector256.Create(
        0x8000, 0x4000, 0x2000, 0x1000, 0x0800, 0x0400, 0x0200, 0x0100,
        0x0080, 0x0040, 0x0020, 0x0010, 0x0008, 0x0004, 0x0002, 0x0001);

    // Pattern repeated across 32 lanes for writing a uint in one go. The first 16 lanes
    // handle the upper 16 bits; the second 16 lanes handle the lower 16 bits.
    private static readonly Vector512<ushort> Masks512 = Vector512.Create(Masks256, Masks256);

    // '0' broadcast to all lanes; adding 0 or 1 to each lane yields '0' or '1'.
    private static readonly Vector128<ushort> Zeroes = Vector128.Create((ushort)'0');
    private static readonly Vector256<ushort> Zeroes256 = Vector256.Create((ushort)'0');
    private static readonly Vector512<ushort> Zeroes512 = Vector512.Create((ushort)'0');

    // Writes 8 chars for a single byte (Vector128, one lane per bit).
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteByteChars(Span<char> chars, byte value)
    {
        // Broadcast the byte value to all 8 lanes.
        var vector = Vector128.Create((ushort)value);

        // AND each lane with its mask: non-zero means that bit is set.
        var isolated = Vector128.BitwiseAnd(vector, Masks);

        // Compare each lane to its mask: lanes where the bit is set become 0xFFFF, others 0x0000.
        var compared = Vector128.Equals(isolated, Masks);

        // Shift each 0xFFFF right by 15 to get 1, and 0x0000 stays 0.
        var shifted = Vector128.ShiftRightLogical(compared, 15);

        // Add to '0' to get '0' or '1' in each lane, then write the 8 chars in one go.
        Vector128.Add(Zeroes, shifted).CopyTo(MemoryMarshal.Cast<char, ushort>(chars));
    }

    // Writes 16 chars for a ushort (Vector256, one lane per bit).
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteUInt16Chars(Span<char> chars, ushort value)
    {
        // Broadcast the ushort value to all 16 lanes.
        var vector = Vector256.Create(value);

        // Same algorithm as WriteByteChars, now operating on all 16 bits in parallel.
        var isolated = Vector256.BitwiseAnd(vector, Masks256);
        var compared = Vector256.Equals(isolated, Masks256);
        var shifted = Vector256.ShiftRightLogical(compared, 15);
        Vector256.Add(Zeroes256, shifted).CopyTo(MemoryMarshal.Cast<char, ushort>(chars));
    }

    // Writes 32 chars for a uint (Vector512, one lane per bit).
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void WriteUInt32Chars(Span<char> chars, uint value)
    {
        // Split the uint into two 16-bit halves: upper bits fill the first 16 lanes (producing
        // the first 16 chars), lower bits fill the last 16 lanes (the remaining 16 chars).
        var vector = Vector512.Create(
            Vector256.Create((ushort)(value >> 16)),
            Vector256.Create((ushort)value));

        // Same algorithm as WriteByteChars, now operating on all 32 bits in parallel.
        var isolated = Vector512.BitwiseAnd(vector, Masks512);
        var compared = Vector512.Equals(isolated, Masks512);
        var shifted = Vector512.ShiftRightLogical(compared, 15);
        Vector512.Add(Zeroes512, shifted).CopyTo(MemoryMarshal.Cast<char, ushort>(chars));
    }
}