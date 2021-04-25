using System.Globalization;
using System.IO;
using System.Threading;

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
            Thread.Sleep(1000);
            using var stream = fileInfo.OpenRead();

            return new FileMetaInfo
            {
                FileName = fileInfo.Name,
                FileSize = fileInfo.Length,
                FileType = fileInfo.Extension.ToLower(CultureInfo.InvariantCulture),
                FileEntropy = _entropyCalculator.Calculate(stream)
            };
        }

        private readonly IFileEntropyCalculator _entropyCalculator;
    }
}