using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandomTester.Tests.Tests
{
    [TestClass]
    public class RunsTestTest
    {
        [TestMethod]
        public void RunsShouldMatch()
        {
            var rt = new RunsTest();
            var p = rt.RunTest(SampleData.BitArray, 52);

            Assert.AreEqual(0.0057, p.PValue, 0.00005);
        }
    }
}
