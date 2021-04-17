using System.Diagnostics;
using HashBL;

namespace AppUI
{
    public static class User
    {
        public static HashMap<Account> HashUser(int count)
        {
            HashMap<Account> hashMap = new HashMap<Account>();

            Account account;

            for (int i = 1; i <= count; i++)
            {
                string salt = Hashing.GetSalt();
                uint[] arr = Hashing.GetShaHash(i.ToString() + salt);

                account = new Account(i.ToString(), arr, salt);
                hashMap.AddHash(Hashing.GetHash(account.Login), account);
            }

            hashMap.Serealize("HashData/file1.data");

            Debug.WriteLine("Hashing end");

            return hashMap;
        }
    }
}
