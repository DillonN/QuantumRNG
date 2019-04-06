using RandomTester.Enums;
using System.Collections.Generic;
using System.Linq;

namespace RandomTester
{
    internal readonly struct TestResult
    {
        public TestType Type { get; }
        public double PValue { get; }

        public TestResult(TestType type, double pValue)
        {
            Type = type;
            PValue = pValue;
        }

        public static TestResult Average(IEnumerable<TestResult> results)
        {
            var pValue = results.Select(r => r.PValue).Average();
            return new TestResult(results.First().Type, pValue);
        }
    }
}
