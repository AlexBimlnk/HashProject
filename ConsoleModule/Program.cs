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

            //Console.WriteLine(HashForText("lo"));
            //Console.WriteLine(HashForText("gin"));
            //Console.WriteLine(HashForText("login"));
            //Console.WriteLine(HashForText("l"));
            //Console.WriteLine(HashForText("0"));
            //Console.WriteLine(HashForText("1"));
            //Console.WriteLine(HashForText("3"));

            //Console.WriteLine();

            User.HashUser(1);

            Console.ReadLine();
        }
    }
}
