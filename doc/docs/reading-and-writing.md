# Reading and Writing Primitive Values

The library provides extension methods for reading and writing multi-byte primitive values from a
variety of C# byte container types. All methods use a consistent naming convention and support
configurable byte ordering via the [`Endian`](API/MrKWatkins.BinaryPrimitives/Endian/index.md)
enum.

## Supported Types

The following primitive types can be read and written:

| Method Suffix | .NET Type | Size |
| ------------- | --------- | ---- |
| `Int16`       | `short`   | 2 bytes |
| `UInt16`      | `ushort`  | 2 bytes |
| `UInt24`      | `int`     | 3 bytes (value is stored in the low 24 bits) |
| `Int32`       | `int`     | 4 bytes |
| `UInt32`      | `uint`    | 4 bytes |
| `Int64`       | `long`    | 8 bytes |
| `UInt64`      | `ulong`   | 8 bytes |

## Endianness

All multi-byte methods accept an optional `Endian` parameter. The default is `Endian.Little`
(least significant byte first). Pass `Endian.Big` to use big-endian (most significant byte first)
byte ordering.

```csharp
byte[] bytes = [0x01, 0x02, 0x03, 0x04];

int littleEndian = bytes.GetInt32(0);            // 0x04030201
int bigEndian    = bytes.GetInt32(0, Endian.Big); // 0x01020304
```

## Byte Containers

### byte[] and Span&lt;byte&gt;

[`ByteArrayExtensions`](API/MrKWatkins.BinaryPrimitives/ByteArrayExtensions/index.md) and
[`ByteSpanExtensions`](API/MrKWatkins.BinaryPrimitives/ByteSpanExtensions/index.md) support
both reading (`Get*`) and writing (`Set*`). All methods take a zero-based index indicating
where in the array or span to read from or write to.

```csharp
byte[] data = new byte[8];

data.SetInt32(0, 42);
data.SetUInt16(4, 1000, Endian.Big);

int value    = data.GetInt32(0);              // 42
ushort word  = data.GetUInt16(4, Endian.Big); // 1000
```

### ReadOnlySpan&lt;byte&gt;

[`ByteReadOnlySpanExtensions`](API/MrKWatkins.BinaryPrimitives/ByteReadOnlySpanExtensions/index.md)
supports reading only. The index parameter specifies the start position.

```csharp
ReadOnlySpan<byte> span = stackalloc byte[] { 0xFF, 0x00, 0x00, 0x00 };
int value = span.GetInt32(0); // -1 (little-endian signed int32)
```

### IList&lt;byte&gt; and List&lt;byte&gt;

[`ByteIListExtensions`](API/MrKWatkins.BinaryPrimitives/ByteIListExtensions/index.md) and
[`ByteListExtensions`](API/MrKWatkins.BinaryPrimitives/ByteListExtensions/index.md) support
both `Get*` and `Set*` methods. `List<byte>` has optimised overloads that avoid virtual
dispatch.

```csharp
var list = new List<byte> { 0x0A, 0x00, 0x00, 0x00 };
int value = list.GetInt32(0); // 10
```

### IReadOnlyList&lt;byte&gt;

[`ByteIReadOnlyListExtensions`](API/MrKWatkins.BinaryPrimitives/ByteIReadOnlyListExtensions/index.md)
supports reading only. It also provides a `CopyTo` method for copying the entire list into a
`Span<byte>`.

```csharp
IReadOnlyList<byte> source = new List<byte> { 1, 2, 3, 4 };
uint value = source.GetUInt32(0);

Span<byte> destination = stackalloc byte[4];
source.CopyTo(destination);
```

### ICollection&lt;byte&gt;

[`ByteICollectionExtensions`](API/MrKWatkins.BinaryPrimitives/ByteICollectionExtensions/index.md)
provides `Add*` methods that append the bytes of a value to the collection in the specified byte
order. These are useful for building up byte sequences.

```csharp
var buffer = new List<byte>();
buffer.AddUInt16(0x0102);             // appends 0x02, 0x01 (little-endian)
buffer.AddUInt16(0x0102, Endian.Big); // appends 0x01, 0x02 (big-endian)
buffer.AddInt32(-1);                  // appends 0xFF, 0xFF, 0xFF, 0xFF
```

### ReadOnlyMemory&lt;byte&gt;

[`ReadOnlyMemoryExtensions`](API/MrKWatkins.BinaryPrimitives/ReadOnlyMemoryExtensions/index.md)
supports reading only, with the same `Get*` methods and index parameter as other read-only
containers.

```csharp
ReadOnlyMemory<byte> memory = new byte[] { 0x78, 0x56, 0x34, 0x12 };
int value = memory.GetInt32(0); // 0x12345678
```

### Stream

[`StreamExtensions`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/index.md) provides
`Read*OrThrow` and `Write*` methods for sequential reading and writing. Read methods throw
`EndOfStreamException` if the stream ends before enough bytes are available. All methods have
asynchronous counterparts suffixed with `Async`.

```csharp
using var stream = new MemoryStream();
stream.WriteInt32(42);
stream.WriteUInt16(1000, Endian.Big);

stream.Position = 0;
int    a = stream.ReadInt32OrThrow();
ushort b = stream.ReadUInt16OrThrow(Endian.Big);
```

Asynchronous versions follow the same pattern:

```csharp
await stream.WriteInt32Async(42);
int value = await stream.ReadInt32OrThrowAsync();
```

Additional helper methods are available for reading raw bytes:

- `ReadByteOrThrow()` reads a single byte or throws `EndOfStreamException`.
- `ReadExactly(length)` reads a specific number of bytes or throws `EndOfStreamException`.
- `ReadAllBytes()` reads all remaining bytes into a new array.

## Composing Values from Individual Bytes

[`EndianExtensions`](API/MrKWatkins.BinaryPrimitives/EndianExtensions/index.md) provides
methods for composing values from individual bytes when working byte-by-byte:

```csharp
byte b0 = 0x01, b1 = 0x02;
ushort value = Endian.Little.ToUInt16(b0, b1); // 0x0201
ushort big   = Endian.Big.ToUInt16(b0, b1);    // 0x0102
```

[`UInt16Extensions`](API/MrKWatkins.BinaryPrimitives/UInt16Extensions/index.md) provides
`ToBytes()` for decomposing a `ushort` into its most and least significant bytes, and
[`ByteByteTupleExtensions`](API/MrKWatkins.BinaryPrimitives/ByteByteTupleExtensions/index.md)
provides `ToUInt16()` for the reverse:

```csharp
ushort value = 0x1234;
(byte msb, byte lsb) = value.ToBytes(); // msb = 0x12, lsb = 0x34

ushort reconstructed = (msb, lsb).ToUInt16(); // 0x1234
```
