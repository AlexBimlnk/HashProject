using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    public static class User
    {
        public static HashMap<Account> HashUser(int count)
        {
            HashMap<Account> hashMap = new HashMap<Account>();

            //HashMap<Tuple<uint[], string>> hashMap = new HashMap<Tuple<uint[], string>>();

            Account account;

            for (int i = 1; i <= count; i++)
            {
                string salt = Hashing.GetSalt();
                uint[] arr = Hashing.GetPasswordHash(i.ToString() + salt);

                account = new Account(i.ToString(), arr, salt);
                hashMap.AddHash(Hashing.GetHash(account.Login), account);
            }

            hashMap.Serealize("HashData/file1.data");

            Console.WriteLine("Hashing end");

            return hashMap;
        }
    }
}
