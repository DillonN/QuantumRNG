using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomTester.Tests;

namespace RandomTester
{
    internal static class TestRunner
    {
        public static void RunTest()
        {
            var rand = new Random();

            var bytes = new byte[1000000000];

            rand.NextBytes(bytes);

            var tests = new List<ITest>
            {
                new FrequencyTest(),
                new BlockTest(),
                new RunsTest()
            };

            var results = tests.Select(t => t.RunTest(bytes, (ulong) bytes.Length * 8)).ToList();
        }
    }
}
