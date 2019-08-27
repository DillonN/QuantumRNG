using System;
using System.Collections.Generic;
using System.Diagnostics;
using RandomTester.Enums;

namespace RandomTester.Wrappers
{
    internal abstract class RandomWrapper : IDisposable
    {
        public abstract RandomType Type { get; }

        protected abstract IReadOnlyList<byte> InitData(int samples);

        public IReadOnlyList<byte> TimeAndInitData(int samples, out TimeSpan time)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var data = InitData(samples);

            stopwatch.Stop();

            Debug.Assert(data.Count == samples);

            time = stopwatch.Elapsed;

            return data;
        }

        protected virtual void Dispose(bool disposing)
        { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
