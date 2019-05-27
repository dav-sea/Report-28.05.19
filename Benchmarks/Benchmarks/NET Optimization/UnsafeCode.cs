using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks.NET_Optimization
{
    [LegacyJitX64Job]
    public class UnsafeCode
    {
        byte[,] data;
        Random random;

        [GlobalSetup]
        public void GlobalSetup()
        {
            random = new Random(100500);
            data = new byte[1024 * 512, 2];
        }

        [Benchmark(Baseline = true)]
        public unsafe void ProcessUnsafe()
        {
            fixed (byte* pByteData = data)
            {
                int* pIntData = (int*)pByteData;
                var leng = (data.GetLength(0) * data.GetLength(1)) / 4;
                for (int i = 0; i < leng; i++)
                    pIntData[i] = random.Next();
            }
        }

        [Benchmark]
        public void ProcessSafe()
        {
            var count = data.GetLength(0);

            for (int i = 0; i < count; i++)
            {
                data[i, 0] = (byte)random.Next();
                data[i, 1] = (byte)random.Next();
            }
        }

        [Benchmark]
        public void ProcessSafeOptimized()
        {
            var count = data.GetLength(0) - 1;

            for(int i = 0; i < count; i += 2)
            {
                var rnd = random.Next();
                data[i, 0]     = (byte)(rnd);
                data[i, 1]     = (byte)(rnd & 0x0000FF00);
                data[i + 1, 0] = (byte)(rnd & 0x00FF0000);
                data[i + 1, 1] = (byte)(rnd >> 24);
            }
        }
    }
}
