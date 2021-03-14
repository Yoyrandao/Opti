using System.Globalization;
using System.IO;

using BackgroundAgent.Processing.Logic.Calculators;
using BackgroundAgent.Processing.Models;

namespace BackgroundAgent.Processing.Logic
{
    public class FileMetaInformationProvider : IFileMetaInformationProvider
    {
        public FileMetaInformationProvider(IFileEntropyCalculator entropyCalculator)
        {
            _entropyCalculator = entropyCalculator;
        }
        
        public FileMetaInfo GetInformation(string path)
        {
            var fileInfo = new FileInfo(path);

            return new FileMetaInfo
            {
                FileName = fileInfo.Name,
                FileSize = fileInfo.Length,
                FileType = fileInfo.Extension.ToLower(CultureInfo.InvariantCulture),
                FileEntropy = _entropyCalculator.Calculate(path)
            };
        }

        private readonly IFileEntropyCalculator _entropyCalculator;
    }
}