using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomTester.Tests
{
    internal class FrequencyTest : ITest
    {
        public TestResults RunTest(IEnumerable<byte> bytes, ulong numBits)
        {
            var sum = bytes
                .AsParallel()
                .Sum(b => BitCount.BitTable[b] - 4);

            var stat = Math.Abs(sum) / Math.Sqrt(numBits / 8d);
            var pValue = Helpers.ErrorFuncCompl(stat / Math.Pow(2, 0.5));

            return new TestResults(nameof(FrequencyTest), pValue);
        }
    }
}
