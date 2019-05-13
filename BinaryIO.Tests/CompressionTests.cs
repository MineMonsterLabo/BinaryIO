using System.IO.Compression;
using BinaryIO;
using BinaryIO.Compression;
using NUnit.Framework;

namespace RakDotNet.Tests
{
    [TestFixture]
    public class CompressionTests
    {
        [Test]
        public void ZlibTest()
        {
            BinaryStream stream = new BinaryStream();
            stream.WriteStringUtf8("abcdefg");

            byte[] compressionZlib = CompressionManager.CompressionZlib(stream, CompressionLevel.Fastest, true);
            BinaryStream stream2 = new BinaryStream(compressionZlib);

            byte[] decompressionZlib = CompressionManager.DecompressionZlib(stream2, true);
            BinaryStream stream3 = new BinaryStream(decompressionZlib);

            Assert.True(stream3.ReadStringUtf8() == "abcdefg");
        }

        [Test]
        public void GZipTest()
        {
            BinaryStream stream = new BinaryStream();
            stream.WriteStringUtf8("abcdefg");

            byte[] compressionZlib = CompressionManager.CompressGZIP(stream, CompressionLevel.Fastest, true);
            BinaryStream stream2 = new BinaryStream(compressionZlib);

            byte[] decompressionZlib = CompressionManager.DecompressGZIP(stream2, true);
            BinaryStream stream3 = new BinaryStream(decompressionZlib);

            Assert.True(stream3.ReadStringUtf8() == "abcdefg");
        }
    }
}