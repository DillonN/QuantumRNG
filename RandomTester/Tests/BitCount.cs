using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RandomTester.Tests
{
    internal static class BitCount
    {
        public static readonly IReadOnlyDictionary<byte, byte> BitTable;

        static BitCount()
        {
            var bitTable = new Dictionary<byte, byte>();
            for (var i = 0; i <= byte.MaxValue; i++)
            {
                byte sum = 0;
                for (var j = 0; j < 8; j++)
                {
                    if ((i & 1 << j) != 0) sum++;
                }

                Debug.Assert(sum <= 8);

                bitTable[(byte)i] = sum;
            }

            BitTable = bitTable;
        }
    }
}
