using System.IO;
using System.IO.Compression;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using EnsureThat;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class DecompressionProcessor : BasicProcessor
    {
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            var decompressedFileLocation =
                Path.Combine(FsLocation.ApplicationTempData, snapshot.BaseFileName + ".decompressed");

            var fileInfo = new FileInfo(snapshot.DecryptedPath);

            using (var originFileStream = fileInfo.OpenRead())
            {
                using (var decompressedFileStream = File.Create(decompressedFileLocation))
                {
                    using (var decompressionStream = new DeflateStream(originFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }

            Successor?.Process(snapshot);
        }
    }
}