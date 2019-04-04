using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RandomTester.Tests.Tests
{
    internal static class SampleData
    {
        private const string BitString = "1100 0011 1010 1110 0000 1111 0000 1111 0000 1111 0000 1111 0000";

        public static IReadOnlyList<byte> BitArray { get; }

        static SampleData()
        {
            var arr = new byte[7];
            var ba = MakeBitArray(BitString);
            ba.CopyTo(arr, 0);
            BitArray = arr;
        }

        private static BitArray MakeBitArray(string bitString)
        {
            var size = 0;
            foreach (var t in bitString)
                if (t != ' ') ++size;

            var result = new BitArray(size);
            var k = 0; // ptr into result
            foreach (var t in bitString)
            {
                if (t == ' ') continue;
                if (t == '1') result[k] = true;
                else result[k] = false;
                ++k;
            }
            return result;
        }
    }
}
