using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("RandomTester.Tests")]

namespace RandomTester
{
    internal static class Program
    {
        private const int numProcess = 10000000;
        private const int NumValues = 1000000;

        private static async Task Main(string[] args)
        {
            //var y = 2000d;

            //for (var i = 0; i < 30; i++)
            //{
            //    y -= 25 * Math.Pow(2, i);
            //    if (y <= 0)
            //    {

            //    }
            //}

            var rand = new Random();

            using (var bmap = new Bitmap(512, 512))
            {
                for (var x = 0; x < bmap.Width; x++)
                {
                    for (var y = 0; y < bmap.Height; y++)
                    {
                        var set = rand.Next() % 2 == 0;
                        bmap.SetPixel(x, y, set ? Color.White : Color.Black);
                    }
                }

                bmap.Save("bmap.bmp");
            }

            var results = TestRunner.RunTimeTest();

            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key}:");
                foreach (var count in result.Value.Keys)
                {
                    Console.WriteLine($"\t{count}: {result.Value[count]}");
                }
            }

            var csv = new List<string>();
            var header = "count";
            foreach (var type in results.Keys)
            {
                header += $",{type}";
            }

            csv.Add(header);

            foreach (var count in results.First().Value.Keys)
            {
                var line = count.ToString();
                foreach (var type in results.Keys)
                {
                    line += $",{results[type][count]}";
                }
                csv.Add(line);
            }

            await File.WriteAllLinesAsync("results.csv", csv);

            //var results = TestRunner.RunTest();

            //foreach (var result in results)
            //{
            //    Console.WriteLine($"{result.Type}: ");
            //    Console.WriteLine($"\tInit time: {result.InitTime}");
            //    Console.WriteLine("\tResults:");
            //    foreach (var (key, value) in result.TestResults)
            //    {
            //        Console.WriteLine($"\t\t{key}: {value:F6}");
            //    }
            //}


            //var rand = new Random();
            //var stopwatch = new Stopwatch();

            //var distribution = new Dictionary<int, int>();

            //for (var i = 0; i < NumValues; i++)
            //{
            //    distribution[i] = 0;
            //}

            //var bins = new List<Bin>();

            //for (var i = 0d; i < 1d; i += 0.01)
            //{
            //    bins.Add(new Bin(i, i + 0.01));
            //}

            //stopwatch.Start();
            //for (var i = 0; i < numProcess; i++)
            //{
            //    var x = rand.NextDouble();
            //    foreach (var bin in bins)
            //    {
            //        if (bin.SetIfIn(x)) break;
            //    }
            //    //for (var j = 0; j < 32; j++)
            //    //{
            //    //    if ((x & 1 << j) == 0) continue;

            //    //    distribution[j]++;
            //    //}
            //}

            //stopwatch.Stop();

            //Console.WriteLine($"Took {stopwatch.Elapsed}");

            ////var toPrint = distribution.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Key},{kvp.Value}").Prepend("value,sys");

            //var toPrint = bins.Select(b => $"{b.Start},{b.Count}").Prepend("value,sys");

            //await File.WriteAllLinesAsync("results.csv", toPrint);
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
