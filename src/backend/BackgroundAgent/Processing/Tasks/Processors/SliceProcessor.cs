using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using EnsureThat;

using Serilog;

using Utils.Hashing;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class SliceProcessor : BasicProcessor
    {
        public SliceProcessor(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }
        
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            snapshot.Parts = new List<FilePart>();
            
            _logger.Information($"Running slicing process for {snapshot.BaseFileName}");

            var fileToSliceLocation = snapshot.EncryptedPath;
            var buffer = new byte[BUFFER_SIZE];

            using (var source = File.OpenRead(fileToSliceLocation))
            {
                var index = 0;

                while (source.Position < source.Length)
                {
                    var partName = snapshot.BaseFileName + $".{index}";
                    var partPath = Path.Combine(FsLocation.ApplicationTempData, partName);

                    using (var target = File.Create(partPath))
                    {
                        var remaining = SLICE_LENGTH;
                        int bytesRead;

                        while (remaining > 0
                               && (bytesRead = source.Read(buffer, 0, Math.Min(remaining, BUFFER_SIZE))) > 0)
                        {
                            target.Write(buffer, 0, bytesRead);
                            remaining -= bytesRead;
                        }
                    }
                    
                    snapshot.Parts.Add(new FilePart
                    {
                        Path = partPath,
                        PartName = partName,
                        Hash = _hashProvider.Hash(File.ReadAllText(partPath))
                    });

                    index++;
                    Thread.Sleep(300);
                }
            }
            
            File.Delete(fileToSliceLocation);
        }

        private const int SLICE_LENGTH = 128 * 1024;
        private const int BUFFER_SIZE = 20 * 1024;

        private readonly IHashProvider _hashProvider;

        private readonly ILogger _logger = Log.ForContext<SliceProcessor>();
    }
}