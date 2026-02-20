# Bit Operations

The library provides extension methods for common bit-level operations on primitive integer
types. These cover individual bit access, bit ranges, nibble manipulation, parity, overflow
detection, and binary string formatting.

## Supported Types

Bit operations are available on the following types:

| Type     | Bit Width | API Reference |
| -------- | --------- | ------------- |
| `byte`   | 8         | [`ByteExtensions`](API/MrKWatkins.BinaryPrimitives/ByteExtensions/index.md) |
| `ushort` | 16        | [`UInt16Extensions`](API/MrKWatkins.BinaryPrimitives/UInt16Extensions/index.md) |
| `int`    | 32        | [`Int32Extensions`](API/MrKWatkins.BinaryPrimitives/Int32Extensions/index.md) |
| `uint`   | 32        | [`UInt32Extensions`](API/MrKWatkins.BinaryPrimitives/UInt32Extensions/index.md) |
| `long`   | 64        | [`Int64Extensions`](API/MrKWatkins.BinaryPrimitives/Int64Extensions/index.md) |
| `ulong`  | 64        | [`UInt64Extensions`](API/MrKWatkins.BinaryPrimitives/UInt64Extensions/index.md) |

## Individual Bit Access

`GetBit(index)` returns `true` if the bit at the given zero-based index is set.
`SetBit(index)` returns a new value with that bit set.
`ResetBit(index)` returns a new value with that bit cleared.

Bits are numbered from 0 (least significant) to N-1 (most significant).

```csharp
byte b = 0b00001010; // bits 1 and 3 are set

bool bit1 = b.GetBit(1); // true
bool bit0 = b.GetBit(0); // false

byte set    = b.SetBit(0);   // 0b00001011
byte reset  = b.ResetBit(1); // 0b00001000
```

## Bit Ranges

`GetBits(startInclusive, endInclusive)` extracts a contiguous range of bits, shifting the
result right so that the start bit is at position 0. `SetBits(value, startInclusive,
endInclusive)` replaces a range of bits with the low bits of the supplied value.

Both methods validate that the range indices are within bounds and that `endInclusive` is not
less than `startInclusive`.

```csharp
byte b = 0b01101100;

byte nibble = b.GetBits(2, 5); // bits 2..5 = 0b1011, shifted to 0b1011 (11)

byte updated = b.SetBits(0b1111, 2, 5); // sets bits 2..5 to 1111 -> 0b01111100
```

## Convenience Bit Properties

Three convenience methods provide named access to commonly used bit positions:

- `LeftMostBit()` returns the value of the most significant bit.
- `RightMostBit()` returns the value of the least significant bit (bit 0).
- `SignBit()` returns the value of the sign bit (same as `LeftMostBit()`).

```csharp
byte b = 0b10000001;

bool left  = b.LeftMostBit();  // true  (bit 7)
bool right = b.RightMostBit(); // true  (bit 0)
bool sign  = b.SignBit();       // true  (bit 7)
```

## Nibble Operations (byte only)

`HighNibble()` returns bits 4-7 of a byte shifted down to bits 0-3.
`LowNibble()` returns bits 0-3 of a byte.
`SetHighNibble(nibble)` returns a new byte with the upper four bits replaced.
`SetLowNibble(nibble)` returns a new byte with the lower four bits replaced.

```csharp
byte b = 0xAB;

byte high = b.HighNibble();           // 0x0A
byte low  = b.LowNibble();            // 0x0B

byte updated = b.SetHighNibble(0x05); // 0x5B
```

## Copying Bits with a Mask (byte only)

[`CopyBitsFrom(toCopyFrom, mask)`](API/MrKWatkins.BinaryPrimitives/ByteExtensions/CopyBitsFrom.md)
copies bits from one byte to another selectively. Bits where the mask is set are taken from
`toCopyFrom`; bits where the mask is clear are taken from the receiver.

```csharp
byte target = 0b11110000;
byte source = 0b10101010;
byte mask   = 0b00001111;

byte result = target.CopyBitsFrom(source, mask); // 0b11101010
```

## Parity (byte only)

[`Parity()`](API/MrKWatkins.BinaryPrimitives/ByteExtensions/Parity.md) returns `true` if the
number of set bits in the byte is even (even parity).

```csharp
byte b = 0b00001111; // 4 set bits
bool even = b.Parity(); // true

byte c = 0b00000111; // 3 set bits
bool odd = c.Parity(); // false
```

## Byte Decomposition (ushort only)

`MostSignificantByte()` returns the high byte, and `LeastSignificantByte()` returns the low
byte. `ToBytes()` returns both as a `(byte Msb, byte Lsb)` tuple.

```csharp
ushort value = 0x1234;

byte msb = value.MostSignificantByte(); // 0x12
byte lsb = value.LeastSignificantByte(); // 0x34

(byte m, byte l) = value.ToBytes(); // m = 0x12, l = 0x34
```

## Binary String Representation

`ToBinaryString()` converts any supported integer value to a human-readable binary string
prefixed with `"0b"`.

| Type     | Output length | Example                              |
| -------- | ------------- | ------------------------------------ |
| `byte`   | 10 chars      | `"0b01011010"`                       |
| `ushort` | 18 chars      | `"0b0001001000110100"`               |
| `int`    | 34 chars      | `"0b00000000000000000000000001011010"` |
| `uint`   | 34 chars      | `"0b00000000000000000000000001011010"` |
| `long`   | 66 chars      | (64 digits prefixed with `"0b"`)     |
| `ulong`  | 66 chars      | (64 digits prefixed with `"0b"`)     |

```csharp
byte b = 0x5A;
string s = b.ToBinaryString(); // "0b01011010"

int i = 90;
string t = i.ToBinaryString(); // "0b00000000000000000000000001011010"
```

[`BoolExtensions.ToBitChar()`](API/MrKWatkins.BinaryPrimitives/BoolExtensions/ToBitChar.md)
converts a boolean to the character `'1'` for `true` or `'0'` for `false`. This is useful when
building binary string representations character by character.

```csharp
char c = true.ToBitChar();  // '1'
char d = false.ToBitChar(); // '0'
```

## Arithmetic Overflow Detection

These methods detect whether an arithmetic operation on two values produced an overflow or a
half-carry/half-borrow. They are primarily useful when emulating processors that expose status
flags, such as Z80 or 6502 CPUs.

### Addition

`DidAdditionHalfCarry(left, right)` is called on the sum and returns `true` if the addition of
the low nibble (for `byte`) or low byte (for `ushort`) produced a carry into the next nibble or
byte. On `byte`, this is a carry from bit 3 to bit 4. On `ushort`, it is a carry from bit 11 to
bit 12.

`DidAdditionOverflow(left, right)` is called on the sum and returns `true` if a signed addition
overflowed, meaning the result has the wrong sign relative to both operands.

```csharp
byte left  = 120;
byte right = 20;
byte sum   = (byte)(left + right); // 140, wraps in signed interpretation

bool overflow  = sum.DidAdditionOverflow(left, right);  // true (sign changed)
bool halfCarry = sum.DidAdditionHalfCarry(left, right); // depends on nibbles
```

### Subtraction

`DidSubtractionHalfBorrow(left, right)` is called on the difference and returns `true` if a
borrow occurred from the upper nibble into the lower nibble (for `byte`), or from the upper byte
into the lower byte (for `ushort`).

`DidSubtractionOverflow(left, right)` is called on the difference and returns `true` if a signed
subtraction overflowed.

```csharp
byte left       = 10;
byte right      = 20;
byte difference = (byte)(left - right); // 246 (wraps)

bool overflow   = difference.DidSubtractionOverflow(left, right);    // true
bool halfBorrow = difference.DidSubtractionHalfBorrow(left, right);  // depends on nibbles
```
