using System.IO;

namespace BackgroundAgent.Processing.Calculators
{
    public interface IFileEntropyCalculator
    {
        double Calculate(FileStream stream);
    }
}