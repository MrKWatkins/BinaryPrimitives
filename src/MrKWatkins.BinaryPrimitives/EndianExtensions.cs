using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="Endian" />.
/// </summary>
public static class EndianExtensions
{
    /// <param name="endian">The endianness to use.</param>
    extension(Endian endian)
    {
        /// <summary>
        /// Composes an unsigned 24-bit integer from three bytes.
        /// </summary>
        /// <param name="byte0">The first byte.</param>
        /// <param name="byte1">The second byte.</param>
        /// <param name="byte2">The third byte.</param>
        /// <returns>The composed 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ToUInt24(byte byte0, byte byte1, byte byte2)
        {
            if (endian == Endian.Little)
            {
                return byte0 | byte1 << 8 | byte2 << 16;
            }

            return byte2 | byte1 << 8 | byte0 << 16;
        }

        /// <summary>
        /// Composes a word from two bytes.
        /// </summary>
        /// <param name="byte0">The first byte.</param>
        /// <param name="byte1">The second byte.</param>
        /// <returns>The composed word.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort ToWord(byte byte0, byte byte1) =>
            endian == Endian.Little
                ? (ushort)(byte0 | byte1 << 8)
                : (ushort)(byte1 | byte0 << 8);
    }
}