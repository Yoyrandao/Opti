using System;
using System.IO;
using System.Linq;

using RestSharp.Validation;

namespace BackgroundAgent.Processing.Logic.Calculators
{
    public class FileEntropyCalculator : IFileEntropyCalculator
    {
        public double Calculate(FileStream stream)
        {
            byte[] buffer;
            try
            {
                var length = (int) stream.Length;
                var sum = 0;

                int count;

                buffer = new byte[length];

                while ((count = stream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;
            }
            finally
            {
                stream.Close();
            }

            if (buffer.Length == 0)
            {
                return 0;
            }
            
            var map = buffer.GroupBy(b => b).Select(b => new
            {
                Value = b.Key,
                Probability = (double)buffer.LongCount(x => x == b.Key) / (double)buffer.Length
            });
            var entropy = -1 * map.Select(e => e.Probability * Math.Log(e.Probability)).Sum();

            return entropy; 
        }
    }
}