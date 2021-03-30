using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    public static class User
    {
        public static HashMap<Tuple<uint[],string>> HashUser(int count)
        {
            HashMap<Tuple<uint[], string>> hashMap = new HashMap<Tuple<uint[], string>>();

            for (int i = 1; i <= count; i++)
            {
                string salt = Hashing.GetSalt();
                uint[] arr = Hashing.GetPasswordHash(i.ToString() + salt);
                hashMap.AddHash(Hashing.GetHash(i.ToString()), new Tuple<uint[], string>(arr, salt));
            }

            hashMap.Serealize("HashData/file1.data");

            Console.WriteLine("Hashing end");

            return hashMap;
        }
    }
}
