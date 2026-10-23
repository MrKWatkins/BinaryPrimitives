# BinaryPrimitives

[![Build Status](https://github.com/MrKWatkins/BinaryPrimitives/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/BinaryPrimitives/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/MrKWatkins.BinaryPrimitives)](https://www.nuget.org/packages/MrKWatkins.BinaryPrimitives)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MrKWatkins.BinaryPrimitives)](https://www.nuget.org/packages/MrKWatkins.BinaryPrimitives)

> Extension methods on primitives types for various binary operations, such as reading and writing values from bytes and calculating parity.

The name is based on the [BinaryPrimitives](https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives) class in the .NET Framework which provides similar methods.

Full documentation is available at [mrkwatkins.github.io/BinaryPrimitives](https://mrkwatkins.github.io/BinaryPrimitives/). Source code and issue tracking are on [GitHub](https://github.com/MrKWatkins/BinaryPrimitives).

## Installation

```bash
dotnet add package MrKWatkins.BinaryPrimitives
```

## Usage

### Reading and Writing

Read and write multi-byte integers from `byte[]`, `Span<byte>`, `Stream`, and other byte
container types. All methods support an optional `Endian` parameter (defaulting to
`Endian.Little`):

```csharp
byte[] data = new byte[8];
data.SetInt32(0, 42);
data.SetUInt16(4, 1000, Endian.Big);

int    a = data.GetInt32(0);              // 42
ushort b = data.GetUInt16(4, Endian.Big); // 1000
```

Collections can have values appended directly:

```csharp
var buffer = new List<byte>();
buffer.AddUInt32(0xDEADBEEF);
buffer.AddInt16(-1, Endian.Big);
```

Streams support typed reading and writing, with async overloads for all methods:

```csharp
stream.WriteInt32(42);
int value = stream.ReadInt32OrThrow();
```

### Bit Operations

Get, set, and clear individual bits or ranges of bits on `byte`, `ushort`, `int`, `uint`,
`long`, and `ulong`:

```csharp
byte b = 0b00001010;
bool set   = b.GetBit(1);     // true
byte with0 = b.SetBit(0);     // 0b00001011
byte clear = b.ResetBit(1);   // 0b00001000
byte range = b.GetBits(1, 3); // bits 1..3, shifted to position 0
```

`byte` also provides nibble access, parity, and CPU flag detection:

```csharp
byte high = b.HighNibble(); // bits 4-7
bool even = b.Parity();     // true if an even number of bits are set
```

All supported integer types can be formatted as a binary string:

```csharp
string s = ((byte)0x5A).ToBinaryString(); // "0b01011010"
```

### Streams

`ReadOnlyListStream` wraps an `IReadOnlyList<byte>` as a seekable, read-only `Stream`:

```csharp
IReadOnlyList<byte> bytes = LoadBytes();
using var stream = new ReadOnlyListStream(bytes);
```

`PeekableStream` wraps any readable `Stream` to add one-byte lookahead without consuming the
byte:

```csharp
using var stream = new PeekableStream(inner);
if (stream.Peek() == 0xFF)
{
    stream.ReadByteOrThrow(); // consume it
}
```

## Use of AI

My general rule is I'll write the interesting bits and use AI for the boring bits. I wrote the bulk of the code as part of my OakEmu project before
forking it out into its own project. The main use of AI has been to generate the documentation and help with test coverage.

## Licencing

Licensed under GPL v3.0.

## Building documentation

Handwritten pages live in `docs/content`; `docs/site.json` controls navigation and API assembly inputs.
The shared Docker image supplies VitePress, Vale and Sesharp. Generated API pages and site output stay outside the source documentation.

Run `python3 docs/preview.py` with Docker and the .NET 10 SDK installed, or select **Preview documentation** in a JetBrains IDE.
The preview is available at `http://localhost:5173/BinaryPrimitives/`. Restart after editing source pages.

The short project workflows delegate builds and documentation validation to the shared `dotnet/project` composite action. API project paths are read from `docs/site.json`.
Normal builds compile, test and validate the complete documentation. The **Release** workflow defaults to publishing both packages and docs. Leave the version blank to increment the latest stable tag’s minor version (for example, `1.7.0` → `1.8.0`), or supply an explicit version. It prepares packages and docs, then publishes the
validated NuGet artifacts, creates the release and deploys the validated Pages artifact in the same job. NuGet's policy must allow `release.yml` for user
`MrKWatkins`. The job remains in this repository to preserve its NuGet OIDC workflow identity. Configure **Settings → Pages → Source → GitHub Actions** before the first deployment.

For a docs-only update, run **Release** with **publish-packages** off, **publish-docs** on and the version blank. This updates docs without publishing a package or creating a tag. It uses handwritten pages from the selected branch and API
assemblies built from the highest stable release tag in a separate checkout. The header keeps that release's version. The optional `api-ref` input selects an explicit API
source override for XML comment corrections; the workflow reports the chosen commits and warns that an override may differ from the published package.
Turning **publish-docs** off skips deployment while package builds still validate docs. Turning both options off runs validation without publishing.
Historical API builds disable NuGet auditing so an advisory in an old build dependency does not prevent rebuilding released documentation; normal builds retain auditing.

Release and docs-only deployments share the same Pages concurrency group and reject artifacts for a version older than the latest release. Validated site artifacts
are retained for 14 days. If deployment fails after NuGet publication, run **Release** with **publish-packages** off; no new package is needed.

For the initial cutover, use `api-ref: main` when publishing docs for 1.7.0: its original XML comments fail the new Vale rules. This migration corrects those comments
without changing the API implementation. Once a package release includes the corrections, omit the override to use its released source by default.
