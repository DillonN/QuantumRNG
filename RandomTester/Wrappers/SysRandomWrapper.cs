using System;
using System.Collections.Generic;
using System.Text;
using RandomTester.Enums;

namespace RandomTester.Wrappers
{
    internal class SysRandomWrapper : RandomWrapper
    {
        public override RandomType Type => RandomType.System;

        private readonly Random _r;

        public SysRandomWrapper(int seed)
        {
            _r = new Random(seed);
        }

        protected override IReadOnlyList<byte> InitData(int samples)
        {
            //if (samples % 4 != 0)
            //    throw new ArgumentOutOfRangeException();

            var bytes = new byte[samples];
            //var numInts = samples / 4;

            //for (var i = 0; i < numInts; i++)
            //{
            //    var sample = _r.Next(int.MinValue, int.MaxValue);
            //    var b = BitConverter.GetBytes(sample);
            //    b.CopyTo(bytes, i * 4);
            //}

            _r.NextBytes(bytes);

            return bytes;
        }
    }
}
