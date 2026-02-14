namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Specifies the byte ordering of multi-byte values.
/// </summary>
public enum Endian
{
    /// <summary>
    /// Least significant byte first.
    /// </summary>
    Little,

    /// <summary>
    /// Most significant byte first.
    /// </summary>
    Big
}