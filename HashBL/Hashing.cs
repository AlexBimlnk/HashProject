using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    /// <summary>
    /// Класс реализующий разные алгоритмы хеширования
    /// </summary>
    public static class Hashing
    {
        private static void PrintUi(uint a)
        {
            int step = 31;
            while (step >= 0)
            {
                uint b = Convert.ToUInt32(Math.Pow(2, step));
                Console.Write(a / b);
                a %= b;
                step--;
                if (step % 8 == 7)
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
        private static void PrintListUi(List<uint> a)
        {
            foreach (var c in a)
            {
                PrintUi(c);
            }
        }
        private static void PrintUiArr(uint[] a)
        {
            for (int i = 0; i < 5; i++)
                PrintUi(a[i]);
        }

        /// <summary>
        /// Циклический сдвиг влево
        /// </summary>
        private static uint LeftRotate(uint a, int b)
        {
            b %= 32;
            if (b == 0)
                return a;
            uint mask = 0;
            int count = b;

            while (count != 0)
            {
                mask = mask << 1;
                mask++;
                count--;
            }

            mask = mask << (32 - b);

            mask = mask & a;
            mask = mask >> (32 - b);

            a = a << b;
            a = a | mask;
            return a;
        }
        private static List<uint> MakeUIList(string s)
        {
            List<uint> ret = new List<uint>();
            uint tmp = 0;

            for (int i = 0; i < s.Length; i++)
            {
                tmp = tmp << 8;
                tmp += (Convert.ToUInt32(s[i]));

                if (i % 4 == 3)
                {
                    ret.Add(tmp);
                    tmp = 0;
                }
            }
            ret.Add(tmp);

            tmp = ret[ret.Count - 1];

            if (tmp != 0)
            {
                int step = 0;
                while ((tmp & 0xff000000) == 0)
                {
                    tmp = tmp << 8;
                    step += 8;
                }
                uint one = 1;
                one = one << (step - 1);
                tmp = tmp + one;
            }
            else
            {
                tmp = 1;
                tmp = tmp << 31;
            }

            ret[ret.Count - 1] = tmp;

            while (ret.Count != 15)
            {
                ret.Add(0);
            }

            ret.Add(Convert.ToUInt32(s.Length * 8));

            //PrintListUi(ret);

            for (int i = 16; i < 80; i++)
            {
                tmp = ret[i - 3] ^ ret[i - 8] ^ ret[i - 14] ^ ret[i - 16];
                tmp = LeftRotate(tmp, 1);
                ret.Add(tmp);
                tmp = 0;
            }


            //PrintListUi(ret);

            return ret;
        }

        /// <summary>
        /// Алгоритм хеширования sha-1
        /// </summary>
        public static uint[] GetPasswordHash(string s)
        {
            uint h0 = 0x67452301,
                 h1 = 0xEFCDAB89,
                 h2 = 0x98BADCFE,
                 h3 = 0x10325476,
                 h4 = 0xC3D2E1F0;

            List<uint> w = MakeUIList(s);

            uint a = h0,
                 b = h1,
                 c = h2,
                 d = h3,
                 e = h4;

            uint k = 0, temp = 0, f = 0;
            for (int i = 0; i < 80; i++)
            {
                if (0 <= i && i <= 19)
                {
                    f = (b & c) | ((~b) & d);
                    k = 0x5A827999;
                }
                else if (20 <= i && i <= 39)
                {
                    f = b ^ c ^ d;
                    k = 0x6ED9EBA1;
                }
                else if (40 <= i && i <= 59)
                {
                    f = (b & c) | (b & d) | (c & d);
                    k = 0x8F1BBCDC;
                }
                else if (60 <= i && i <= 79)
                {
                    f = b ^ c ^ d;
                    k = 0xCA62C1D6;
                }

                temp = LeftRotate(a, 5) + f + e + k + w[i];

                e = d;
                d = c;
                c = LeftRotate(b, 30);
                b = a;
                a = temp;
            }
            h0 += a;
            h1 += b;
            h2 += c;
            h3 += d;
            h4 += e;

            uint[] ret = { h0, h1, h2, h3, h4 };

            return ret;
        }
        /// <summary>
        /// Алгоритм хеширования через простое число в степени
        /// </summary>
        public static ulong GetHash(string s)
        {
            ulong answer = 0;
            short primeNumber = 67;

            for (int i = 0; i < s.Length; i++)
            {
                answer += (ulong)(s[i] - '0' + 1) * (ulong)Math.Pow(primeNumber, i + 1);
            }

            return answer;
        }
    }
}
