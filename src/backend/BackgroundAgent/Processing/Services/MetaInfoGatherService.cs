using System.Globalization;
using System.IO;

using BackgroundAgent.Processing.Calculators;
using BackgroundAgent.Processing.Models;

using Serilog;

namespace BackgroundAgent.Processing.Services
{
    public class MetaInfoGatherService : IMetaInfoGatherService
    {
        public MetaInfoGatherService(IFileEntropyCalculator entropyCalculator)
        {
            _entropyCalculator = entropyCalculator;
        }
        
        public FileMetaInfo Gather(string path)
        {
            _logger.Information($"Gathering meta info for {path}");
            
            var fileInfo = new FileInfo(path);
            
            using var stream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);

            return new FileMetaInfo
            {
                FileName = fileInfo.Name,
                FileSize = fileInfo.Length,
                FileType = fileInfo.Extension.ToLower(CultureInfo.InvariantCulture),
                FileEntropy = _entropyCalculator.Calculate(stream)
            };
        }

        private readonly IFileEntropyCalculator _entropyCalculator;
        

        private readonly ILogger _logger = Log.ForContext<MetaInfoGatherService>();
    }
}