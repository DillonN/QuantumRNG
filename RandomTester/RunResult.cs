using System;
using System.Collections.Generic;
using System.Linq;
using RandomTester.Enums;

namespace RandomTester
{
    internal class RunResult
    {
        public IReadOnlyDictionary<TestType, double> TestResults { get; }
        public RandomType Type { get; }
        public TimeSpan InitTime { get; }

        public RunResult(IReadOnlyDictionary<TestType, double> results, RandomType type, TimeSpan time)
        {
            TestResults = results;
            Type = type;
            InitTime = time;
        }

        public static RunResult Average(IReadOnlyList<RunResult> results)
        {
            if (results.Count <= 0)
                throw new ArgumentException();

            var time = TimeSpan.Zero;
            var type = results.First().Type;

            var tests = new Dictionary<TestType, double>();

            foreach (var result in results)
            {
                if (result.Type != type)
                    throw new ArgumentException();

                time += result.InitTime;

                foreach (var key in result.TestResults.Keys)
                {
                    if (!tests.ContainsKey(key))
                    {
                        tests[key] = result.TestResults[key];
                    }
                    else
                    {
                        tests[key] += result.TestResults[key];
                    }
                }
            }

            foreach (var key in tests.Keys)
            {
                tests[key] /= results.Count;
            }

            return new RunResult(tests, type, time / results.Count);
        }
    }
}
