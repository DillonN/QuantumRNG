using System;
using System.Collections.Generic;
using System.Text;

namespace RandomTester.Tests
{
    internal static class Helpers
    {
        public static double ErrorFuncCompl(double x)
        {
            const double p = 0.3275911;
            const double a1 = 0.254829592;
            const double a2 = -0.284496736;
            const double a3 = 1.421413741;
            const double a4 = -1.453152027;
            const double a5 = 1.061405429;
            var t = 1.0 / (1.0 + p * x);
            var err = 1.0 - ((((a5 * t + a4) * t + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);
            return 1 - err;
        }
    }
}
