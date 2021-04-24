using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Tasks.Processors;

using NUnit.Framework;

using Utils.Hashing;

namespace Tests
{
    public class Splitting
    {
        [SetUp]
        public void Setup()
        {
            _sliceProcessor = new SliceProcessor(new Sha256HashProvider());
        }

        [Test]
        public void SliceTest()
        {
            _sliceProcessor.Process(new FileSnapshot
            {
                BaseFileName = "testfile.txt"
            });
        }

        private BasicProcessor _sliceProcessor;
    }
}