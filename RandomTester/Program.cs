using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly:InternalsVisibleTo("RandomTester.Tests")]

namespace RandomTester
{
    internal static class Program
    {
        private const int numProcess = 10000000;
        private const int NumValues = 1000000;

        private static async Task Main(string[] args)
        {
            var y = 2000d;

            for (var i = 0; i < 30; i++)
            {
                y -= 25 * Math.Pow(2, i);
                if (y <= 0)
                {

                }
            }

            TestRunner.RunTest();


            var rand = new Random();
            var stopwatch = new Stopwatch();

            var distribution = new Dictionary<int, int>();

            for (var i = 0; i < NumValues; i++)
            {
                distribution[i] = 0;
            }

            var bins = new List<Bin>();

            for (var i = 0d; i < 1d; i += 0.01)
            {
                bins.Add(new Bin(i, i + 0.01));
            }

            stopwatch.Start();
            for (var i = 0; i < numProcess; i++)
            {
                var x = rand.NextDouble();
                foreach (var bin in bins)
                {
                    if (bin.SetIfIn(x)) break;
                }
                //for (var j = 0; j < 32; j++)
                //{
                //    if ((x & 1 << j) == 0) continue;

                //    distribution[j]++;
                //}
            }

            stopwatch.Stop();

            Console.WriteLine($"Took {stopwatch.Elapsed}");

            //var toPrint = distribution.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key},{kvp.Value}").Prepend("value,sys");

            var toPrint = bins.Select(b => $"{b.Start},{b.Count}").Prepend("value,sys");

            await File.WriteAllLinesAsync("results.csv", toPrint);
        }
    }

    internal class Bin
    {
        public double Start { get; }
        public double End { get; }

        public int Count { get; private set; }

        public Bin(double start, double end)
        {
            Start = start;
            End = end;
            Count = 0;
        }

        public bool SetIfIn(double num)
        {
            if (num >= Start && num < End)
            {
                Count++;
                return true;
            }

            return false;
        }
    }
}
