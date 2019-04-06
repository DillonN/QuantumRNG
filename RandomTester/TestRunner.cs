using RandomTester.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomTester.Enums;

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

            foreach (var key in Types)
            {
                results[key] = RunResult
                    .Average(CreateRuns(key, 1000000)
                        .AsParallel()
                        .Select(r => r.RunTest(256)));
            }

            return results.Values;
        }

        private static IEnumerable<Run> CreateRuns(RandomType type, int count)
        {
            for (var i = 0; i < count; i++)
            {
                if (type == RandomType.System)
                {
                    yield return new Run(new SysRandomWrapper(i));
                }
                else
                {
                    yield return new Run(new SecRandomWrapper());
                }
            }
        }
    }
}
