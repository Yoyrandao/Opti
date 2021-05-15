using System;
using System.IO;
using System.IO.Compression;

using BackgroundAgent.Processing.Calculators;
using BackgroundAgent.Processing.Services;

namespace CompressionRatioViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                var filePath = arg;
                var metaInfoService = new MetaInfoGatherService(new FileEntropyCalculator());

                var metaInfoBeforeCompression = metaInfoService.Gather(filePath);
            
                var fileInfo = new FileInfo(filePath);
                var compressedFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{fileInfo.Name}.compressed");

                using (var originFileStream = fileInfo.OpenRead())
                {
                    using (var compressedFileStream = File.Create(compressedFilePath))
                    {
                        using (var compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originFileStream.CopyTo(compressionStream);
                        }
                    }
                }

                var compressedFileInfo = new FileInfo(compressedFilePath);
            
                Console.WriteLine($"{metaInfoBeforeCompression.FileName}:");
                Console.WriteLine($"\tCR: {(float)compressedFileInfo.Length / (float)metaInfoBeforeCompression.FileSize}");
                Console.WriteLine();
            }
        }
    }
}