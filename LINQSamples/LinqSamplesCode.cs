using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace LINQSamples
{
    class LinqSamplesCode
    {
        public static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            for (int i=0; i <5; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name, -20}: {file.Length, 10:N0}");
            }
        }

        public static void ShowLargeFilesWithLinq(string path)
        {
            var query = new DirectoryInfo(path).GetFiles().OrderByDescending(f => f.Length).Take(5);

            foreach (var file in query)
            {
                Console.WriteLine($"{file.Name,-20}: {file.Length,10:N0}");
            }
        }
    }
}
