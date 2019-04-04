using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomTester.Tests
{
    internal class BlockTest : ITest
    {
        public TestResults RunTest(IEnumerable<byte> bytes, ulong numBits)
        {
            var sum = bytes
                .AsParallel()
                .Take((int) (numBits / 8))
                .Sum(b =>
                {
                    var prop = BitCount.BitTable[b] / 8d - 0.5;
                    return prop * prop;
                });

            var chiSq = 4 * 8 * sum;
            var a = numBits / 8 / 2;
            var x = chiSq / 2;
            var pValue = GammaUpper(a, x);

            return new TestResults(nameof(BlockTest), pValue);
        }

        private static double LogGamma(double x)
        {
            var coef = new[] 
            {
                76.18009172947146,
                -86.50532032941677,
                24.01409824083091,
                -1.231739572450155,
                0.1208650973866179E-2,
                -0.5395239384953E-5
            };

            const double logSqrtTwoPi = 0.91893853320467274178;
            var denom = x + 1;
            var y = x + 5.5;
            var series = 1.000000000190015;
            for (var i = 0; i < 6; ++i)
            {
                series += coef[i] / denom;
                denom += 1.0;
            }
            return logSqrtTwoPi + (x + 0.5) * Math.Log(y) - y + Math.Log(series / x);
        }

        private static double GammaLowerSer(double a, double x)
        {
            // Incomplete GammaLower (computed by series expansion)
            if (x < 0.0)
                throw new Exception("x param less than 0.0 in GammaLowerSer");
            var gln = LogGamma(a);
            var ap = a;
            var del = 1.0 / a;
            var sum = del;
            for (var n = 1; n <= 100000; ++n)
            {
                ++ap;
                del *= x / ap;
                sum += del;
                if (Math.Abs(del) < Math.Abs(sum) * 3.0E-7) // Close enough?
                    return sum * Math.Exp(-x + a * Math.Log(x) - gln);
            }

            throw new Exception("Unable to compute GammaLowerSer to desired accuracy");
        }

        private static double GammaUpperCont(double a, double x)
        {
            // Incomplete GammaUpper computed by continuing fraction
            if (x < 0.0)
                throw new Exception("x param less than 0.0 in GammaUpperCont");
            var gln = LogGamma(a);
            var b = x + 1.0 - a;
            var c = 1.0 / 1.0E-30; // Div by close to double.MinValue
            var d = 1.0 / b;
            var h = d;
            for (var i = 1; i <= 100000; ++i)
            {
                var an = -i * (i - a);
                b += 2.0;
                d = an * d + b;
                if (Math.Abs(d) < 1.0E-30) d = 1.0E-30; // Got too small?
                c = b + an / c;
                if (Math.Abs(c) < 1.0E-30) c = 1.0E-30;
                d = 1.0 / d;
                var del = d * c;
                h *= del;
                if (Math.Abs(del - 1.0) < 3.0E-7)
                    return Math.Exp(-x + a * Math.Log(x) - gln) * h;  // Close enough?
            }
            throw new Exception("Unable to compute GammaUpperCont to desired accuracy");
        }

        public static double GammaUpper(double a, double x)
        {
            // Incomplete Gamma 'Q' (upper)
            if (x < 0.0 || a <= 0.0)
                throw new Exception("Bad args in GammaUpper");

            if (x < a + 1)
                return 1.0 - GammaLowerSer(a, x); // Indirect is faster

            return GammaUpperCont(a, x);
        }
    }
}
