using System;
using System.IO;
using HashBL;
using System.Collections.Generic;

namespace ConsoleModule
{
    class Program
    {
        public static ulong HashForText(string s)
        {
            ulong answer = 0;
            short primeNumber = 67;

            for (int i = 0; i < s.Length; i++)
            {
                answer += (ulong)(s[i] - '0' + 1) * (ulong)Math.Pow(primeNumber, i + 1);
            }

            return answer;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine()
            Console.WriteLine(HashForText("lo"));
            Console.WriteLine(HashForText("gin"));
            Console.WriteLine(HashForText("login"));
            Console.WriteLine(HashForText("l"));
            Console.WriteLine(HashForText("0"));
            Console.WriteLine(HashForText("1"));
            Console.WriteLine(HashForText("3"));

            //int count = 0;
            //for(char i = '0'; i<='z'; i++)
            //{
            //    Console.WriteLine(i);
            //    count++;
            //}

            //Console.Write("{");
            //for (int i = 1; i <= 11; i++)
            //    Console.Write($"{(ulong)Math.Pow(67, i)},");

            //Console.WriteLine("}");

            //ulong ryr = (ulong)Math.Pow(67, 10);

            //Console.WriteLine($"RAZ = {ulong.MaxValue - ryr}");

            //Console.WriteLine(count);
            //Console.WriteLine(ulong.MaxValue);
            //Console.WriteLine(long.MaxValue);

            //Console.WriteLine(HashForText("login"));

            //Console.WriteLine((int)'A');
            //Console.WriteLine((int)'z');
            //Console.WriteLine((int)'0');
            //Console.WriteLine((int)'9');

            //string folder = "HashData";
            //if (!Directory.Exists(folder))
            //    Directory.CreateDirectory(folder);


            //Tree test = new Tree();
            //test.Push(6);
            //test.Push(8);
            //test.Push(5);
            //test.PrintByTransverseBypass();
            //Console.WriteLine();


            //int countFile = 1;
            //HashStructure table = new HashStructure();
            //for(int i = 1; i<=10; i++)
            //{
            //    try
            //    {
            //        table.AddHash(i);
            //    }
            //    catch (Exception)
            //    {
            //        table.PrintByTransverseBypass();
            //        Console.WriteLine();
            //        table.Serealize($"{folder}/file{countFile}");
            //        table = new HashStructure(i);
            //        countFile++;
            //    }
            //}



            //Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
