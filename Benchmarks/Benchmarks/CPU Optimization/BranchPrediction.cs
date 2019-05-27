using System;

using BenchmarkDotNet.Attributes;

namespace Benchmarks.Benchmarks.CPU_Optimization
{
    [LegacyJitX64Job]

    public class BranchPrediction
    {
        byte[] array;
        byte[] arraySorted;
        int resultCount;

        /// <summary>
        /// Вызывается в начале бенчмарка
        /// </summary>
        [GlobalSetup]
        public void GlobalSetup()
        {
           var  random = new Random();

            //Создаем два массива на 2 МБ
            array = new byte[1024 * 1024 * 2];
            arraySorted = new byte[1024 * 1024 * 2];

            //Заполняем массив array случайными числами
            random.NextBytes(array);

            //Копируем array в arraySorted и сортируем
            array.CopyTo(arraySorted, 0);
            Array.Sort(arraySorted);
        }

        [Benchmark]
        public void ArrayProcess()
        {
            int count = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] > 127)
                    count++;
            resultCount = count;
        }

        [Benchmark]
        public void ArrayProcessSorted()
        {
            int count = 0;
            for (int i = 0; i < arraySorted.Length; i++)
                if (arraySorted[i] > 127)
                    count++;
            resultCount = count;
        }

        [Benchmark]
        public void ArrayProcessNoBranches()
        {
            int count = 0;
            for (int i = 0; i < array.Length; i++)
                count += array[i] >> 7;
            resultCount = count;
        }
        [Benchmark(Baseline = true)]
        public void ArrayProcessNoBranchesSorted()
        {
            int count = 0;
            for (int i = 0; i < arraySorted.Length; i++)
                count += arraySorted[i] >> 7;
            resultCount = count;
        }
    }
}
