using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using BenchmarkDotNet.Running;

using Benchmarks.Benchmarks.CPU_Optimization;
using Benchmarks.Benchmarks.NET_Optimization;

namespace Benchmarks
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Для корректных и воспроизводимых резултатов добейтись максимально низкой фоновой загружености CPU и сократите кол-во процессов");
            Console.WriteLine("Суммарная продолжительность тестирования примерно 5 минут");
            Console.WriteLine("Для начала тестирования нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();

            var startBenchmarksTime = DateTime.UtcNow;

            Console.WriteLine("Оптимизации на уровне .NET");
            BenchmarkRunner.Run<UsingStructs>();
            BenchmarkRunner.Run<UsingPools>();
            BenchmarkRunner.Run<BoundsCheck>();
            BenchmarkRunner.Run<ReadonlyFields>();
            BenchmarkRunner.Run<UnsafeCode>();

            Console.WriteLine("Оптимизации на уровне CPU");
            BenchmarkRunner.Run<BranchPrediction>();
            BenchmarkRunner.Run<CacheLines>();

            Console.WriteLine($"\n\nТестирование завершено ({(DateTime.UtcNow - startBenchmarksTime).ToString()})");

            Console.ReadKey();
        }

        
    }
}
