using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks.CPU_Optimization
{
    [LegacyJitX64Job]
    public class CacheLines
    {
        byte[,] data;
        const int count = 1024;
        int summResult;

        [GlobalSetup]
        public unsafe void GlobalSetup()
        {
            var random = new Random();

            data = new byte[count, count];

            fixed (byte* pByteData = data)
            {
                int* pIntData = (int*)pByteData;
                var leng = (data.GetLength(0) * data.GetLength(1)) / 4;
                for (int i = 0; i < leng; i++)
                    pIntData[i] = random.Next();
            }
        }

        [Benchmark]
        public void BadAccess()
        {
            int summ = 0;
            for (int j = 0; j < count; j++)
                for (int i = 0; i < count; i++)
                    summ += data[i, j];
        }

        [Benchmark]
        public void BestAccess()
        {
            int summ = 0;
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                    summ += data[i, j];
        }
    }
}
