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
                string salt = Hashing.GetSalt();
                uint[] arr = Hashing.GetPasswordHash(i.ToString() + salt);
                hashMap.AddHash(Hashing.GetHash(i.ToString()), arr, salt);
            }

            hashMap.Serealize("HashData/file1.data");

            //Console.WriteLine(Hashing.GetPasswordHash("sha"));
            Console.WriteLine("Hashing end");

            return hashMap;
        }
    }
}
