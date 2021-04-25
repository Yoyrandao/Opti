using System.IO;
using System.IO.Compression;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using EnsureThat;

using Serilog;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class CompressionProcessor : BasicProcessor
    {
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            if (!snapshot.Compressed)
            {
                _logger.Information($"Skipping compression process for {snapshot.BaseFileName}.");
                Successor?.Process(snapshot);

                return;
            }
            
            _logger.Information($"Running compression process for {snapshot.BaseFileName}.");

            var compressedFileLocation = Path.Combine(FsLocation.ApplicationTempData, snapshot.BaseFileName + ".compressed");
            var fileInfo = new FileInfo(snapshot.Path);

            using (var originFileStream = fileInfo.OpenRead())
            {
                using (var compressedFileStream = File.Create(compressedFileLocation))
                {
                    using (var compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originFileStream.CopyTo(compressionStream);
                    }
                }
            }
            
            _logger.Information($"Compression process complete ({snapshot.BaseFileName}).");

            snapshot.CompressedPath = compressedFileLocation;
            Successor?.Process(snapshot);
        }

        private readonly ILogger _logger = Log.ForContext<CompressionProcessor>();
    }
}