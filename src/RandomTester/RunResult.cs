﻿using System;
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

        public int Count { get; }

        public double Bps => Count / InitTime.TotalSeconds;
        public double Mbps => Count / InitTime.TotalSeconds / 1000000;

        public RunResult(IReadOnlyDictionary<TestType, double> results, RandomType type, TimeSpan time, int count)
        {
            TestResults = results;
            Type = type;
            InitTime = time;
            Count = count;
        }

        public static RunResult Average(IEnumerable<RunResult> results)
        {
            var time = TimeSpan.Zero;

            var tests = new Dictionary<TestType, double>();

            RandomType type = default;

            var count = 0;
            var bitCount = 0;

            foreach (var result in results)
            {
                count++;

                type = result.Type;
                bitCount = result.Count;

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

            var avgTests = new Dictionary<TestType, double>();

            foreach (var key in tests.Keys)
            {
                avgTests[key] = tests[key] / count;
            }

            return new RunResult(avgTests, type, time / count, bitCount);
        }
    }
}
