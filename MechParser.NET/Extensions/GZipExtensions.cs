using System.IO;
using System.IO.Compression;
using System.Text;

namespace MechParser.NET.Extensions
{
    public static class GZipExtensions
    {
        public static byte[] Compress(Stream stream)
        {
            using var memoryOut = new MemoryStream();
            using var zipStream = new GZipStream(memoryOut, CompressionMode.Compress);

            stream.CopyTo(zipStream);

            return memoryOut.ToArray();
        }

        public static string Decompress(Stream stream)
        {
            using var memoryOut = new MemoryStream();
            using var zipStream = new GZipStream(stream, CompressionMode.Decompress);

            zipStream.CopyTo(memoryOut);

            return Encoding.UTF8.GetString(memoryOut.ToArray());
        }
    }
}
