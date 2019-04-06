using RandomTester.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RandomTester.Enums;

namespace RandomTester
{
    internal static class TestRunner
    {
        public static IEnumerable<RunResult> RunTest()
        {
            var runs = new Dictionary<RandomType, List<Run>>
            {
                { RandomType.System, new List<Run>() },
                { RandomType.Security, new List<Run>() }
            };

            for (var i = 0; i < 1000000; i++)
            {
                runs[RandomType.System].Add(new Run(new SysRandomWrapper(i + 10)));
                runs[RandomType.Security].Add(new Run(new SecRandomWrapper()));
            }

            var results = new Dictionary<RandomType, RunResult>();

            foreach (var key in runs.Keys)
            {
                results[key] = RunResult.Average(runs[key].AsParallel().Select(r => r.RunTest(256)).ToList());
            }

            return results.Values;
        }
    }
}
