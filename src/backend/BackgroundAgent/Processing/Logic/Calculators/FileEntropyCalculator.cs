using System;
using System.IO;
using System.Linq;

using RestSharp.Validation;

namespace BackgroundAgent.Processing.Logic.Calculators
{
    public class FileEntropyCalculator : IFileEntropyCalculator
    {
        public double Calculate(string path)
        {
            Ensure.NotNull(path, nameof(path));
            
            var fileContent = File.ReadAllBytes(path);

            if (fileContent.Length == 0)
            {
                return 0;
            }
            
            var map = fileContent.GroupBy(b => b).Select(b => new
            {
                Value = b.Key,
                Probability = (double)fileContent.LongCount(x => x == b.Key) / (double)fileContent.Length
            });
            var entropy = -1 * map.Select(e => e.Probability * Math.Log(e.Probability)).Sum();

            return entropy; 
        }
    }
}