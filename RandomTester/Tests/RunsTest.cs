using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomTester.Tests
{
    internal class RunsTest : ITest
    {
        public TestResults RunTest(IEnumerable<byte> bytes, ulong numBits)
        {
            var numOnes = bytes
                .AsParallel()
                .Sum(b => (double) BitCount.BitTable[b]);

            var prop = numOnes / numBits;

            var runs = 0ul;
            bool? last = null;
            foreach (var b in bytes)
            {
                for (var i = 0; i < 8; i++)
                {
                    var current = (b & 1 << i) != 0;
                    if (current != last) runs++;
                    last = current;
                }
            }

            var num = Math.Abs(runs - 2 * numBits * prop * (1 - prop));
            var denom = 2 * Math.Sqrt(2d * numBits) * prop * (1 - prop);
            var pValue = Helpers.ErrorFuncCompl(num / denom);

            return new TestResults(nameof(RunsTest), pValue);
        }
    }
}
