using System;
using System.Collections.Generic;
using System.Text;

namespace RandomTester.Tests
{
    internal interface ITest
    {
        TestResults RunTest(IEnumerable<byte> bytes, ulong numBits);
    }
}
