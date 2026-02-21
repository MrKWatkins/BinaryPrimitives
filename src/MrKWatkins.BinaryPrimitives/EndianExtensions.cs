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
        /// Composes a <see cref="UInt24" /> from three bytes.
        /// </summary>
        /// <param name="byte0">The first byte.</param>
        /// <param name="byte1">The second byte.</param>
        /// <param name="byte2">The third byte.</param>
        /// <returns>The composed <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 ToUInt24(byte byte0, byte byte1, byte byte2)
        {
            if (endian == Endian.Little)
            {
                return new UInt24((uint)(byte0 | byte1 << 8 | byte2 << 16));
            }

            return new UInt24((uint)(byte2 | byte1 << 8 | byte0 << 16));
        }

        /// <summary>
        /// Composes a <see cref="ushort" /> (UInt16) from two bytes.
        /// </summary>
        /// <param name="byte0">The first byte.</param>
        /// <param name="byte1">The second byte.</param>
        /// <returns>The composed <see cref="ushort" />.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort ToUInt16(byte byte0, byte byte1) =>
            endian == Endian.Little
                ? (ushort)(byte0 | byte1 << 8)
                : (ushort)(byte1 | byte0 << 8);
    }
}