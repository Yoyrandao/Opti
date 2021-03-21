using System.IO;
using System.IO.Compression;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using EnsureThat;

using Serilog;
using Serilog.Core;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class DecompressionProcessor : BasicProcessor
    {
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            if (!snapshot.Compressed)
            {
                _logger.Information($"Skipping decompression process for {snapshot.BaseFileName}");
                Successor?.Process(snapshot);

                return;
            }

            _logger.Information($"Running decompression process for {snapshot.BaseFileName}");

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

        private readonly ILogger _logger = Log.ForContext<DecompressionProcessor>();
    }
}