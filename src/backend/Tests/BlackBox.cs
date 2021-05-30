using BackgroundAgent.Processing.Calculators;

using NUnit.Framework;

namespace Tests
{
    [Category("BlackBox")]
    public class BlackBoxTests
    {
        [SetUp]
        public void Setup()
        {
            _entropyCalculator = new FileEntropyCalculator();
        }
        
        [Test]
        public void FileCreationTest()
        {
            Assert.IsTrue(true);
        }
        
        [Test]
        public void FileDeletionTest()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void FileChangingTest()
        {
            Assert.IsTrue(true);
        }
        
        private IFileEntropyCalculator _entropyCalculator;
    }
}