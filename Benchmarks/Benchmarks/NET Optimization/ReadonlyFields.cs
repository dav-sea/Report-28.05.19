using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks.NET_Optimization
{
    [LegacyJitX64Job]
    public class ReadonlyFields
    {
        Int256 int256 = new Int256(3456345634,423423465,677535234,656772342);
        readonly Int256 int256ro = new Int256(3456345634, 423423465, 677535234, 656772342);


        [Benchmark(Baseline =true)] public long GetValue() => int256.Bits0 + int256.Bits1+ int256.Bits2 + int256.Bits3;
        [Benchmark] public long GetValueReadOnly() => int256ro.Bits0 + int256ro.Bits1 + int256ro.Bits2 + int256ro.Bits3;

        public struct Int256
        {
            private readonly long bits0, bits1, bits2, bits3;

            public Int256(long bits0, long bits1, long bits2, long bits3)
            {
                this.bits0 = bits0;
                this.bits1 = bits1;
                this.bits2 = bits2;
                this.bits3 = bits3;
            }

            public long Bits0 => bits0;
            public long Bits1 => bits1;
            public long Bits2 => bits2;
            public long Bits3 => bits3;
        }

    }
}
