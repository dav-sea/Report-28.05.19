using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.Benchmarks.NET_Optimization
{
    [LegacyJitX64Job]
    [MemoryDiagnoser]
    public class UsingStructs
    {
        byte[] image;
        ImageOnClasses imageOnClasses;
        ImageOnSturcts imageOnSturcts;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var random = new Random();

            image = new byte[3 * 512 * 512];

            random.NextBytes(image);

            imageOnClasses = new ImageOnClasses();
            imageOnSturcts = new ImageOnSturcts();
        }

        [Benchmark]
        public void LoadImageClasses()
        {
            imageOnClasses.Load(image);
        }

        [Benchmark]
        public void LoadImageStructs()
        {
            imageOnSturcts.Load(image);
        }

        sealed class ImageOnClasses
        {
            Color[] matrix;

            public void Load(byte[] bytes)
            {
                matrix = new Color[bytes.Length / 3];
                for (int i = 0; i < bytes.Length; i += 3)
                    matrix[i / 3] = new Color(bytes[i], bytes[i + 1], bytes[i + 2]);
            }

            sealed class Color
            {
                public readonly byte R, G, B;

                public Color(byte r, byte g, byte b)
                {
                    R = r;
                    G = g;
                    B = b;
                }
            }
        }

        class ImageOnSturcts 
        {
            Color[] matrix;

            public void Load(byte[] bytes)
            {
                matrix = new Color[bytes.Length / 3];
                for (int i = 0; i < bytes.Length; i += 3)
                    matrix[i / 3] = new Color(bytes[i], bytes[i + 1], bytes[i + 2]);
            }

            struct Color
            {
                public readonly byte R, G, B;

                public Color(byte r, byte g, byte b)
                {
                    R = r;
                    G = g;
                    B = b;
                }
            }

        }

    }
}
