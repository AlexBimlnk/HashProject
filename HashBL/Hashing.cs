using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    static class Hashing
    {
        /// <summary>
        /// Циклический сдвиг влево
        /// </summary>
        private static uint LeftRotate(uint a, int b)
        { 
            b = b % 32;
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
        private static void PrintUl(ulong a)
        {
            int step = 63;
            while (step >= 0)
            {
                ulong b = Convert.ToUInt64(Math.Pow(2, step));
                Console.Write(a / b);
                a = a % b;
                step--;
                if (step % 8 == 7)
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
        private static void PrintUi(uint a)
        {
            int step = 31;
            while (step >= 0)
            {
                uint b = Convert.ToUInt32(Math.Pow(2, step));
                Console.Write(a / b);
                a = a % b;
                step--;
                if (step % 8 == 7)
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
        private static Tuple<uint, uint> UlongToUint(ulong a)
        {
            Tuple<uint, uint> ret = new Tuple<uint, uint>(Convert.ToUInt32(a >> 32), Convert.ToUInt32(a & 0x00000000ffffffff));
            return ret;
        }
        private static void PrintListUl(List<ulong> a)
        {
            foreach (var c in a)
            {
                PrintUl(c);
            }
        }
        private static void PrintListUi(List<uint> a)
        {
            foreach (var c in a)
            {
                PrintUi(c);
            }
        }
        private static string ConvertToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
        private static uint _16To10(char c)
        {
            if ('0' <= c && c <= '9')
            {
                return Convert.ToUInt16(c - '0');
            }
            else
            {
                return Convert.ToUInt16(c - 'a' + 10);
            }
        }
        private static List<ulong> MakeULList(string s)
        {
            uint len = Convert.ToUInt16(s.Length);
            s = ConvertToHex(s);
            List<ulong> a = new List<ulong>();
            ulong tmp = 0;
            for (int i = 0; i < s.Length; i++)
            {
                tmp = tmp * 16 + _16To10(s[i]);
                if (i % 16 == 15)
                {
                    a.Add(tmp);
                    tmp = 0;
                }
            }
            a.Add(tmp);

            tmp = a[a.Count - 1];

            if (tmp != 0)
            {
                int step = 0;
                while ((tmp & 0xff00000000000000) == 0)
                {
                    tmp = tmp << 8;
                    step += 8;
                }
                ulong one = 1;
                one = one << (step - 1);
                tmp = tmp + one;
            }
            else
            {
                tmp = 1;
                tmp = tmp << 63;
            }
            a[a.Count - 1] = tmp;


            while (a.Count != 7)
            {
                a.Add(0);
            }

            a.Add(len * 8);

            return a;
        }
        private static List<uint> MakeUIList(List<ulong> a)
        {
            List<uint> ret = new List<uint>();

            foreach (var b64 in a)
            {
                Tuple<uint, uint> tmp = UlongToUint(b64);
                ret.Add(tmp.Item1);
                ret.Add(tmp.Item2);
            }

            for (int i = 16; i < 80; i++)
            {
                uint tmp = ret[i - 3] ^ ret[i - 8] ^ ret[i - 14] ^ ret[i - 16];
                tmp = LeftRotate(tmp, 1);
                ret.Add(tmp);
            }
            return ret;
        }

        /// <summary>
        /// Алгоритм хеширования sha-1
        /// </summary>
        public static string GetPasswordHash(string s)
        {
            uint h0 = 0x67452301,
                 h1 = 0xEFCDAB89,
                 h2 = 0x98BADCFE,
                 h3 = 0x10325476,
                 h4 = 0xC3D2E1F0;

            List<uint> w = MakeUIList(MakeULList(s));

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

            string ret = "";

            ret += h0.ToString();
            ret += h1.ToString();
            ret += h2.ToString();
            ret += h3.ToString();
            ret += h4.ToString();

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
