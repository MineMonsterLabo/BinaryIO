using System.IO;
using System.IO.Compression;

namespace BinaryIO
{
    public class ZlibStream : DeflateStream
    {
        private const int mod = 65521;

        private uint adler32 = 1;

        public int Checksum => (int) adler32;

        private uint Update(uint adler, byte[] s, int offset, int count)
        {
            uint l = (ushort) adler;
            ulong h = adler >> 16;
            int p = 0;
            for (; p < (count & 7); ++p)
            {
                l += s[offset + p];
                h += l;
            }

            for (; p < count; p += 8)
            {
                int idx = offset + p;
                for (int c = 0; c < 8; c++)
                {
                    l += s[idx + c];
                    h += l;
                }
            }

            return (uint) (((h % mod) << 16) | (l % mod));
        }

        public ZlibStream(Stream stream, CompressionLevel level, bool leaveOpen) : base(stream, level, leaveOpen)
        {
        }

        public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen) : base(stream, mode, leaveOpen)
        {
        }

        public override void Write(byte[] array, int offset, int count)
        {
            adler32 = Update(adler32, array, offset, count);
            base.Write(array, offset, count);
        }
    }
}