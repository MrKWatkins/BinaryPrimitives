# Streams

The library provides two [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) implementations for working with byte sequences, plus
extension methods on [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) for typed reading and writing. See the
[Reading and Writing](reading-and-writing.md) page for full details of the typed [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream)
extension methods.

## ReadOnlyListStream

[`ReadOnlyListStream`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/index.md) wraps an
[`IReadOnlyList<byte>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1) as a seekable, read-only [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream). It adapts any list of bytes for use
with APIs that consume a [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream), without copying the underlying data.

```csharp
IReadOnlyList<byte> bytes = new List<byte> { 0x01, 0x00, 0x00, 0x00 };
using var stream = new ReadOnlyListStream(bytes);

int value = stream.ReadInt32OrThrow(); // 1
```

The stream supports seeking via [`Seek`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/Seek.md) and the [`Position`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/Position.md) property. [`CanRead`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/CanRead.md) and [`CanSeek`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/CanSeek.md)
return `true` while the stream is open. [`CanWrite`](API/MrKWatkins.BinaryPrimitives/ReadOnlyListStream/CanWrite.md) always returns `false`; any attempt to
write or resize the stream throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception).

## PeekableStream

[`PeekableStream`](API/MrKWatkins.BinaryPrimitives/PeekableStream/index.md) wraps any readable
[`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) and adds the ability to inspect the next byte without consuming it. This is useful for
parsers that need to look ahead one byte before deciding how to proceed.

```csharp
using var inner = new MemoryStream(new byte[] { 0x01, 0x02, 0x03 });
using var stream = new PeekableStream(inner);

int next = stream.Peek(); // 1, position stays at 0
int read = stream.ReadByte(); // 1, position advances to 1
```

[`Peek()`](API/MrKWatkins.BinaryPrimitives/PeekableStream/Peek.md) returns the next byte as an integer in the range 0..255, or -1 if the end of the
stream has been reached. Calling [`Peek()`](API/MrKWatkins.BinaryPrimitives/PeekableStream/Peek.md) multiple times without reading returns the same byte
each time. Any read or seek operation that moves the position will reset the peeked value.

The [`EndOfStream`](API/MrKWatkins.BinaryPrimitives/PeekableStream/EndOfStream.md) property is a convenience that calls [`Peek()`](API/MrKWatkins.BinaryPrimitives/PeekableStream/Peek.md) and returns `true` if the
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

The stream must be readable; passing a non-readable stream throws [`ArgumentException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception).

[`CanRead`](API/MrKWatkins.BinaryPrimitives/PeekableStream/CanRead.md) and [`CanSeek`](API/MrKWatkins.BinaryPrimitives/PeekableStream/CanSeek.md) reflect the underlying stream's capabilities while the wrapper is
open. [`CanWrite`](API/MrKWatkins.BinaryPrimitives/PeekableStream/CanWrite.md) always returns `false`.

## StreamExtensions

[`StreamExtensions`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/index.md) adds typed
read and write methods directly to [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream), covering all the multi-byte primitive types
supported by the library. See
[Reading and Writing: Stream](reading-and-writing.md#stream) for full details and examples.

The methods available are:

**Reading (synchronous):** [`ReadByteOrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadByteOrThrow.md), [`ReadExactly`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadExactly.md), [`ReadAllBytes`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadAllBytes.md),
[`ReadInt16OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadInt16OrThrow.md), [`ReadUInt16OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt16OrThrow.md), [`ReadUInt24OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt24OrThrow.md), [`ReadInt32OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadInt32OrThrow.md),
[`ReadUInt32OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt32OrThrow.md), [`ReadInt64OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadInt64OrThrow.md), [`ReadUInt64OrThrow`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt64OrThrow.md).

**Writing (synchronous):** [`WriteInt16`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteInt16.md), [`WriteUInt16`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt16.md), [`WriteUInt24`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt24.md), [`WriteInt32`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteInt32.md),
[`WriteUInt32`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt32.md), [`WriteInt64`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteInt64.md), [`WriteUInt64`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt64.md).

**Reading (asynchronous):** [`ReadByteOrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadByteOrThrowAsync.md), [`ReadExactlyAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadExactlyAsync.md), [`ReadAllBytesAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadAllBytesAsync.md),
[`ReadInt16OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadInt16OrThrowAsync.md), [`ReadUInt16OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt16OrThrowAsync.md), [`ReadUInt24OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt24OrThrowAsync.md),
[`ReadInt32OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadInt32OrThrowAsync.md), [`ReadUInt32OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt32OrThrowAsync.md), [`ReadInt64OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadInt64OrThrowAsync.md),
[`ReadUInt64OrThrowAsync`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/ReadUInt64OrThrowAsync.md).

**Writing (asynchronous):** [`WriteInt16Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteInt16Async.md), [`WriteUInt16Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt16Async.md), [`WriteUInt24Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt24Async.md), [`WriteInt32Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteInt32Async.md),
[`WriteUInt32Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt32Async.md), [`WriteInt64Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteInt64Async.md), [`WriteUInt64Async`](API/MrKWatkins.BinaryPrimitives/StreamExtensions/WriteUInt64Async.md).
