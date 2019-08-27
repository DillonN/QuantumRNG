using RandomTester.Enums;
using RandomTester.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace RandomTester
{
    internal static class TestRunner
    {
        private static readonly IReadOnlyList<RandomType> Types = new List<RandomType>
        {
            RandomType.System,
            RandomType.Security
        };

        public static IEnumerable<RunResult> RunTest()
        {
            var results = new Dictionary<RandomType, RunResult>();
            var rand = new Random(1);
            var seed = rand.Next();

            foreach (var key in Types)
            {
                results[key] = RunResult
                    .Average(CreateRuns(key, 1000, seed)
                        .AsParallel()
                        .Select(r => r.RunTest(1000)));
            }

            return results.Values;
        }

        public static Dictionary<RandomType, Dictionary<int, double>> RunTimeTest()
        {
            var results = new Dictionary<RandomType, Dictionary<int, double>>();
            var sys = new Random();
            var sec = RandomNumberGenerator.Create();

            foreach (var key in Types)
            {
                results[key] = new Dictionary<int, double>();
                for (var i = 1; i <= 10000; i += 50)
                {
                    const int count = 100000;
                    //var i1 = i;
                    //var ticks = CreateRuns(key, count, 10000)
                    //    //.AsParallel()
                    //    .Select(r => r.RunTest(i1, true).InitTime.Ticks)
                    //    .Sum();

                    var stopwatch = new Stopwatch();
                    var bytes = new byte[i];
                    stopwatch.Start();
                    for (var j = 0; j < count; j++)
                    {
                        if (key == RandomType.System)
                        {
                            sys.NextBytes(bytes);
                        }
                        else
                        {
                            sec.GetBytes(bytes);
                        }
                    }

                    stopwatch.Stop();
                    var ticks = stopwatch.ElapsedTicks;

                    results[key][i] = i * 8 / TimeSpan.FromTicks(ticks).TotalSeconds / 1000000 * count;
                }
            }

            return results;
        }

        private static IEnumerable<Run> CreateRuns(RandomType type, int count, int randSeed)
        {
            for (var i = 0; i < count; i++)
            {
                if (type == RandomType.System)
                {
                    yield return new Run(new SysRandomWrapper(randSeed + i));
                }
                else
                {
                    yield return new Run(new SecRandomWrapper());
                }
            }
        }
    }
}
