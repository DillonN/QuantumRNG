using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomTester.Enums;

namespace RandomTester.Tests
{
    internal class EvenTest : ITest
    {
        public TestType Type => TestType.Even;

        public double RunTest(IEnumerable<byte> bytes, ulong numBits)
        {
            var numEven = bytes.Sum(b => 0.5 - b % 2);
            var stat = Math.Abs(numEven) / Math.Sqrt(numBits / 8d);
            return Helpers.ErrorFuncCompl(stat / Math.Sqrt(2));
        }
    }
}
