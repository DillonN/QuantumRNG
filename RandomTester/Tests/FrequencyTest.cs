using System;
using System.Collections.Generic;
using System.Linq;
using RandomTester.Enums;

namespace RandomTester.Tests
{
    internal class FrequencyTest : ITest
    {
        public TestType Type => TestType.Frequency;

        public double RunTest(IEnumerable<byte> bytes, ulong numBits)
        {
            var sum = bytes
                //.AsParallel()
                .Sum(b => BitCount.BitTable[b] - 4);

            var stat = Math.Abs(sum) / Math.Sqrt(numBits / 8d);
            var pValue = Helpers.ErrorFuncCompl(stat / Math.Pow(2, 0.5));

            return pValue;
        }
    }
}
