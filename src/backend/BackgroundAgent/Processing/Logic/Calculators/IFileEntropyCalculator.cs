using System.IO;

namespace BackgroundAgent.Processing.Logic.Calculators
{
    public interface IFileEntropyCalculator
    {
        double Calculate(FileStream stream);
    }
}