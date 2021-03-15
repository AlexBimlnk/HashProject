using System;
using System.IO;
using HashBL;
using System.Collections.Generic;

namespace ConsoleModule
{
    class Program
    {
        static long HashForText(string s)
        {
            long ans = 0;
            int r = 28;
            long rr = 1;

            for (int i = 0;i < s.Length;i++)
            {
                ans += (s[i] - 'a' + 1) * rr;
                rr *= r;
            }

            return ans;
        }
        static void Main(string[] args)
        {
            Directory.CreateDirectory("HashData");

            Tree test = new Tree();
            test.Push(6);
            test.Push(8);
            test.Push(5);
            test.PrintByTransverseBypass();
            Console.WriteLine();

            int countFile = 1;
            HashStructure table = new HashStructure(6);
            for(int i = 1; i<=4; i++)
            {
                try
                {
                    table.AddHash(i);
                }
                catch (Exception)
                {
                    table.PrintByTransverseBypass();
                    Console.WriteLine();
                    table.Serealize($"file{countFile}");
                    table = new HashStructure(i);
                    countFile++;
                }
            }



            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
