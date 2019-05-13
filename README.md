# BinaryIO
[![Nuget](https://img.shields.io/nuget/v/BinaryIO.svg)](https://www.nuget.org/packages/BinaryIO/)

endian and varint, compression support stream.

## Version 1.1.4 Incompatible Change Guid

### Old Read / Write Guid Bytes
M - MostByte

L - LeastByte

M * 8 (Endian Reverse)

L * 8 (Endian Reverse)

### New Read / Write Guid Bytes

X - Byte

X * 16 (Endian Reverse)
