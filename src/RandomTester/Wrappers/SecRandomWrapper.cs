using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using RandomTester.Enums;

namespace RandomTester.Wrappers
{
    internal class SecRandomWrapper : RandomWrapper
    {
        public override RandomType Type => RandomType.Security;

        private readonly RandomNumberGenerator _r;

        public SecRandomWrapper()
        {
            _r = new RNGCryptoServiceProvider();
        }

        protected override IReadOnlyList<byte> InitData(int samples)
        {
            var bytes = new byte[samples];

            _r.GetBytes(bytes);

            return bytes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _r.Dispose();

            base.Dispose(disposing);
        }
    }
}
