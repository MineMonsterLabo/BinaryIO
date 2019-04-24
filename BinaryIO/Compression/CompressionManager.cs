using System;
using System.IO;
using System.IO.Compression;

namespace BinaryIO.Compression
{
    public static class CompressionManager
    {
        public static byte ZLIB_HEADER = 0x78;

        public static byte[] DecompressionZlib(BinaryStream stream, bool oldStreamClose = false)
        {
            byte header = stream.ReadByte();
            if (header != ZLIB_HEADER)
            {
                throw new FormatException(nameof(header));
            }

            stream.ReadByte(); // Compression Level

            using (ZlibStream zlib =
                new ZlibStream(stream, CompressionMode.Decompress, false))
            {
                using (MemoryStream dc = new MemoryStream())
                {
                    zlib.CopyTo(dc);

                    if (oldStreamClose)
                        stream.Close();

                    return dc.ToArray();
                }
            }
        }

        public static byte[] CompressionZlib(BinaryStream stream, CompressionLevel compressionLevel,
            bool oldStreamClose = false)
        {
            using (MemoryStream compressed = new MemoryStream())
            {
                compressed.WriteByte(ZLIB_HEADER);
                compressed.WriteByte(GetCompressionLevelByte(compressionLevel));

                using (ZlibStream zlib = new ZlibStream(compressed, compressionLevel, false))
                {
                    byte[] buffer = stream.ToArray();
                    zlib.Write(buffer, 0, buffer.Length);

                    if (oldStreamClose)
                        stream.Close();

                    int sum = zlib.Checksum;
                    byte[] sumBytes = BitConverter.GetBytes(sum);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(sumBytes);
                    }

                    compressed.Write(sumBytes, 0, sumBytes.Length);
                }

                return compressed.ToArray();
            }
        }

        public static byte[] DecompressGZIP(BinaryStream stream, bool oldStreamClose)
        {
            using (GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress, false))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    gzip.CopyTo(ms);

                    if (oldStreamClose)
                        stream.Close();

                    return ms.ToArray();
                }
            }
        }

        public static byte[] CompressGZIP(BinaryStream stream, CompressionLevel compressionLevel,
            bool oldStreamClose)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, compressionLevel, false))
                {
                    byte[] buffer = stream.ToArray();
                    gzip.Write(buffer, 0, buffer.Length);

                    if (oldStreamClose)
                        stream.Close();
                }

                return ms.ToArray();
            }
        }

        private static byte GetCompressionLevelByte(CompressionLevel level)
        {
            switch (level)
            {
                case CompressionLevel.NoCompression:
                    return 0x01;
                case CompressionLevel.Fastest:
                    return 0x9c;
                case CompressionLevel.Optimal:
                    return 0xda;

                default:
                    return 0x00;
            }
        }
    }
}