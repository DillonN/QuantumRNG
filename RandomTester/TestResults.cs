using System;
using System.Collections.Generic;
using System.Text;

namespace RandomTester
{
    internal class TestResults
    {
        public string Name { get; }
        public double PValue { get; }

        public TestResults(string name, double pValue)
        {
            Name = name;
            PValue = pValue;
        }
    }
}
