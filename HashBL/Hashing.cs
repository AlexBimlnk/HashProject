using System;
using System.Collections.Generic;

namespace HashBL
{
    /// <summary>
    /// Класс реализующий разные алгоритмы хеширования
    /// </summary>
    public static class Hashing
    {
        /// <summary>
        /// Циклический сдвиг влево.
        /// </summary>
        private static uint LeftRotate(uint a, int b)
        {
            b %= 32;
            if (b == 0)
                return a;

            uint x = a >> (32 - b);
            a = a << b;
            a = a | x;
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

            for (int i = 16; i < 80; i++)
            {
                tmp = ret[i - 3] ^ ret[i - 8] ^ ret[i - 14] ^ ret[i - 16];
                tmp = LeftRotate(tmp, 1);
                ret.Add(tmp);
                tmp = 0;
            }

            return ret;
        }

        /// <summary>
        /// Алгоритм хеширования sha-1.
        /// </summary>
        public static uint[] GetShaHash(string s)
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
        /// Алгоритм хеширования через простое число в степени.
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

        /// <summary>
        /// Генерация соли.
        /// </summary>
        public static string GetSalt(int n = 12)
        {
            string ret = "";

            Random GenChar = new Random();
            Random GenReg = new Random();

            for (int i = 0; i < n; i++)
            {
                char c;
                if (GenReg.Next() % 2 == 0)
                    c = 'a';
                else
                    c = 'A';

                c = Convert.ToChar(c + GenChar.Next() % 26);

                ret += c;
            }

            return ret;
        }
    }
}
