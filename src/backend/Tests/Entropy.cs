using BackgroundAgent.Processing.Calculators;

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
        }

        private IFileEntropyCalculator _entropyCalculator;
    }
}