# Streams

The library provides two `Stream` implementations for working with byte sequences, plus
extension methods on `Stream` for typed reading and writing. See the
[Reading and Writing](reading-and-writing.md) page for full details of the typed `Stream`
extension methods.

## ReadOnlyListStream

[`ReadOnlyListStream`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/index.md) wraps an
`IReadOnlyList<byte>` as a seekable, read-only `Stream`. It adapts any list of bytes for use
with APIs that consume a `Stream`, without copying the underlying data.

```csharp
IReadOnlyList<byte> bytes = new List<byte> { 0x01, 0x00, 0x00, 0x00 };
using var stream = new ReadOnlyListStream(bytes);

int value = stream.ReadInt32OrThrow(); // 1
```

The stream supports seeking via `Seek` and the `Position` property. `CanRead` and `CanSeek`
return `true` while the stream is open. `CanWrite` always returns `false`; any attempt to
write or resize the stream throws `NotSupportedException`.

## PeekableStream

[`PeekableStream`](API/MrKWatkins.BinaryPrimitives/PeekableStream/index.md) wraps any readable
`Stream` and adds the ability to inspect the next byte without consuming it. This is useful for
parsers that need to look ahead one byte before deciding how to proceed.

```csharp
using var inner = new MemoryStream(new byte[] { 0x01, 0x02, 0x03 });
using var stream = new PeekableStream(inner);

int next = stream.Peek(); // 1, position stays at 0
int read = stream.ReadByte(); // 1, position advances to 1
```

`Peek()` returns the next byte as an integer in the range 0..255, or -1 if the end of the
stream has been reached. Calling `Peek()` multiple times without reading returns the same byte
each time. Any read or seek operation that moves the position will reset the peeked value.

The `EndOfStream` property is a convenience that calls `Peek()` and returns `true` if the
result is -1:

```csharp
while (!stream.EndOfStream)
{
    byte b = stream.ReadByteOrThrow();
    // process b
}
```

### Construction and Ownership

The constructor accepts a `leaveOpen` parameter (default `true`) that controls whether the
underlying stream is disposed when the `PeekableStream` is disposed. Set it to `false` to
transfer ownership:

```csharp
// The underlying stream will be disposed when the PeekableStream is disposed.
using var stream = new PeekableStream(inner, leaveOpen: false);
```

The stream must be readable; passing a non-readable stream throws `ArgumentException`.

`CanRead` and `CanSeek` reflect the underlying stream's capabilities while the wrapper is
open. `CanWrite` always returns `false`.

## StreamExtensions

[`StreamExtensions`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/index.md) adds typed
read and write methods directly to `Stream`, covering all the multi-byte primitive types
supported by the library. See
[Reading and Writing: Stream](reading-and-writing.md#stream) for full details and examples.

The methods available are:

**Reading (synchronous):** `ReadByteOrThrow`, `ReadExactly`, `ReadAllBytes`,
`ReadInt16OrThrow`, `ReadUInt16OrThrow`, `ReadUInt24OrThrow`, `ReadInt32OrThrow`,
`ReadUInt32OrThrow`, `ReadInt64OrThrow`, `ReadUInt64OrThrow`.

**Writing (synchronous):** `WriteInt16`, `WriteUInt16`, `WriteUInt24`, `WriteInt32`,
`WriteUInt32`, `WriteInt64`, `WriteUInt64`.

**Reading (asynchronous):** `ReadByteOrThrowAsync`, `ReadExactlyAsync`, `ReadAllBytesAsync`,
`ReadInt16OrThrowAsync`, `ReadUInt16OrThrowAsync`, `ReadUInt24OrThrowAsync`,
`ReadInt32OrThrowAsync`, `ReadUInt32OrThrowAsync`, `ReadInt64OrThrowAsync`,
`ReadUInt64OrThrowAsync`.

**Writing (asynchronous):** `WriteInt16Async`, `WriteUInt16Async`, `WriteUInt24Async`,
`WriteInt32Async`, `WriteUInt32Async`, `WriteInt64Async`, `WriteUInt64Async`.
