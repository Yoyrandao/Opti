using BackgroundAgent.Processing.Calculators;

using NUnit.Framework;

namespace Tests
{
    [Category("Unit")]
    public class EntropyTests
    {
        [SetUp]
        public void Setup()
        {
            _entropyCalculator = new FileEntropyCalculator();
        }
        
        [Test]
        public void EntopyTest()
        {
            Assert.IsTrue(true);
        }
        
        [Test]
        public void Entopy_NoFileTest()
        {
            Assert.IsTrue(true);
        }

        private IFileEntropyCalculator _entropyCalculator;
    }
}