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
            var bytes = new byte[samples];

            _r.NextBytes(bytes);

            return bytes;
        }
    }
}
