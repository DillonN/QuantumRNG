using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RandomTester.Tests.Tests
{
    [TestClass]
    public class FrequencyTestTest
    {
        [TestMethod]
        public void ShouldMatch()
        {
            var ft = new FrequencyTest();
            var p = ft.RunTest(SampleData.BitArray.Take(6).Prepend((byte) 0b_11000000), 52);

            Assert.AreEqual(0.7815, p, 0.00005);
        }

        [TestMethod]
        public void Nist_ShouldMatch()
        {
            var ft = new FrequencyTest();
            var p = ft.RunTest(new byte[] { 0b_1011_0101, 0b_01010101 }, 10);

            Assert.AreEqual(0.527089, p, 0.0000005);
        }
    }
}
