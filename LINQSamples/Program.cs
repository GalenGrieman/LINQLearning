using System;
using System.IO;

namespace LINQSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\windows";
            Console.WriteLine("Sorting without linq.  Assumes there are atleast 5 files in directory");
            LinqSamplesCode.ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("\nSorting with linq.");
            LinqSamplesCode.ShowLargeFilesWithLinq(path);
            Console.ReadLine();
        }
    }
}
