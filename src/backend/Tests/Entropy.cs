using BackgroundAgent.Processing.Logic.Calculators;

using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            _entropyCalculator = new FileEntropyCalculator();
        }
        
        [Test]
        public void EntopyTest()
        {
            _entropyCalculator.Calculate("./testfile.txt");
        }

        private IFileEntropyCalculator _entropyCalculator;
    }
}