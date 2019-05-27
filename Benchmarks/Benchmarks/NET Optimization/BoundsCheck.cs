using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks.NET_Optimization
{
    public class BoundsCheck
    {
        char[] array;

        [GlobalSetup]
        public void GlobalSetup()
        {
            array = new char[11 * 1024];
        }


        [Benchmark]
        public void FillWithBadBoundChecks()
        {
            for (int i = 0; i < array.Length; i+=11)
                BadBoundChecks(array,i);
        }

        [Benchmark]
        public void FillWithBestBoundChecks()
        {
            for (int i = 0; i < array.Length; i += 11)
                BestBoundCheck(array, i);
        }

        void BadBoundChecks(char[] array, int index)
        {
            array[index + 0] = 'B';
            array[index + 1] = 'O';
            array[index + 2] = 'U';
            array[index + 3] = 'N';
            array[index + 4] = 'D';
            array[index + 5] = ' ';
            array[index + 6] = 'C';
            array[index + 7] = 'H';
            array[index + 8] = 'E';
            array[index + 9] = 'C';
            array[index + 10] = 'K';
        }

        void BestBoundCheck(char[] array, int index)
        {
            if (array.Length < index + 10)
            {
                array[index + 0] = 'B';
                array[index + 1] = 'O';
                array[index + 2] = 'U';
                array[index + 3] = 'N';
                array[index + 4] = 'D';
                array[index + 5] = ' ';
                array[index + 6] = 'C';
                array[index + 7] = 'H';
                array[index + 8] = 'E';
                array[index + 9] = 'C';
                array[index + 10] = 'K';
            }
        }
    }
}
