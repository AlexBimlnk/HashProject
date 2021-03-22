using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    public static class User
    {
        public static HashMap HashUser(int count)
        {
            HashMap hashMap = new HashMap();

            for (int i = 1; i <= count; i++)
            {
                uint[] arr = Hashing.GetPasswordHash(i.ToString());
                hashMap.AddHash(Hashing.GetHash(i.ToString()), arr);
            }

            hashMap.Serealize("HashData/file1.data");

            //Console.WriteLine(Hashing.GetPasswordHash("sha"));
            Console.WriteLine("Hashing end");

            return hashMap;
        }
    }
}
