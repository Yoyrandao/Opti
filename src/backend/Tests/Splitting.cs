using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Tasks.Processors;

using NUnit.Framework;

using Utils.Hashing;

namespace Tests
{
    [Category("Unit")]
    public class SplittingTests
    {
        [SetUp]
        public void Setup()
        {
            _sliceProcessor = new SliceProcessor(new Sha256HashProvider());
        }

        [Test]
        public void SliceTest()
        {
            Assert.IsTrue(true);
        }
        
        [Test]
        public void Slice_NoFileTest()
        {
            Assert.IsTrue(true);
        }

        private BasicProcessor _sliceProcessor;
    }
}