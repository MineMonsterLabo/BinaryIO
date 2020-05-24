# BinaryIO
[![Nuget](https://img.shields.io/nuget/v/BinaryIO.svg)](https://www.nuget.org/packages/BinaryIO/)

endian and varint, compression support stream.

## ReleaseNotes
### Update 1.1.7
- fix WriteStringUtf8
- add L4StringUtf8

### Update 1.1.6 
- Improve performance

### Update 1.1.5 
- Zlib Compression Bug Fix!


## Version 1.1.4 Incompatible Change Guid

### Old Read / Write Guid Bytes
M - MostByte

L - LeastByte

M * 8 (Endian Reverse)

L * 8 (Endian Reverse)

### New Read / Write Guid Bytes

X - Byte

X * 16 (Endian Reverse)
