using System.Collections.Generic;
using RandomTester.Enums;

namespace RandomTester.Tests
{
    internal interface ITest
    {
        TestType Type { get; }
        double RunTest(IEnumerable<byte> bytes, ulong numBits);
    }
}
