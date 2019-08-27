using System;
using RandomTester.Tests;
using RandomTester.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomTester.Enums;

namespace RandomTester
{
    internal class Run : IDisposable
    {
        private readonly RandomWrapper _r;

        public Run(RandomWrapper r)
        {
            _r = r;
        }

        public RunResult RunTest(int samples, bool justTime = false)
        {
            var data = _r.TimeAndInitData(samples, out var initTime);

            var tests = new List<ITest>
            {
                new FrequencyTest(),
                new BlockTest(),
                new RunsTest(),
                new EvenTest()
            };

            if (justTime) tests.Clear();

            //var results = tests.Select(t => t.RunTest(data, (ulong) samples * 8)).ToList();

            var results = new Dictionary<TestType, double>();

            foreach (var test in tests)
            {
                results[test.Type] = test.RunTest(data, (ulong) samples * 8);
            }

            return new RunResult(results, _r.Type, initTime, samples * 8);
        }

        public void Dispose()
        {
            _r.Dispose();
        }
    }
}
