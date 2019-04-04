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

            var runs = RunCheckEnum(bytes)
                .AsParallel()
                .LongCount(b => b);
            //bool? last = null;
            //foreach (var b in bytes)
            //{
            //    for (var i = 0; i < 8; i++)
            //    {
            //        var current = (b & 1 << i) != 0;
            //        if (current != last) runs++;
            //        last = current;
            //    }
            //}

            var num = Math.Abs(runs - 2 * numBits * prop * (1 - prop));
            var denom = 2 * Math.Sqrt(2d * numBits) * prop * (1 - prop);
            var pValue = Helpers.ErrorFuncCompl(num / denom);

            return new TestResults(nameof(RunsTest), pValue);
        }

        private static IEnumerable<bool> RunCheckEnum(IEnumerable<byte> bytes)
        {
            using (var e1 = bytes.GetEnumerator())
            {
                if (!e1.MoveNext()) throw new ArgumentException();

                yield return true;

                while (true)
                {
                    for (var i = 0; i < 8; i++)
                    {
                        var first = (e1.Current & 1 << i) != 0;

                        bool second;
                        if (i < 7)
                        {
                            second = (e1.Current & 1 << (i + 1)) != 0;
                        }
                        else
                        {
                            if (!e1.MoveNext()) yield break;

                            second = (e1.Current & 1) != 0;
                        }

                        yield return first != second;
                    }
                }
            }
        }
    }
}
