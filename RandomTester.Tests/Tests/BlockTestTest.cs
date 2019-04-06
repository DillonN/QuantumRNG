using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandomTester.Tests.Tests
{
    [TestClass]
    public class BlockTestTest
    {
        [TestMethod]
        public void BlockTest_ShouldMatch()
        {

            var bt = new BlockTest();
            var p = bt.RunTest(SampleData.BitArray, 52);

            Assert.AreEqual(0.9978, p, 0.00005);
        }
    }
}
