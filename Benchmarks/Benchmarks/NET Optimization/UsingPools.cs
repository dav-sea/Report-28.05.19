using BenchmarkDotNet.Attributes;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks.NET_Optimization
{
    [LegacyJitX64Job]
    [MemoryDiagnoser]
    public class UsingPools
    {
        byte[] data;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random();

            data = new byte[8192];

            random.NextBytes(data);
        }

        [Params(8192)]
        public int Count { set; get; }      
        
        [Benchmark]
        public void BufferFromPool()
        {
            var count = Count;
            var buffer = ArrayPool<byte>.Shared.Rent(count);
            Array.Copy(data, buffer, count);

            for (int i = 0; i < count; i++)
                buffer[i] *= buffer[i];

            ArrayPool<byte>.Shared.Return(buffer);
        }

        [Benchmark]
        public void BufferInstatiate()
        {
            var count = Count;
            var buffer = new byte[count];
            Array.Copy(data, buffer, count);

            for (int i = 0; i < count; i++)
                buffer[i] *= buffer[i];
        }
    }
}
