using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    public class Account
    {
        private string nickname;
        private string password;
        private long balace;
        private long hashCode;

        public Account(string _nickname, string _password, long _balance)    //Можно запихать данные через какой нибудь список или картеж
        {
            nickname = _nickname;
            password = _password;
            balace = _balance;
            HashData();
        }

        //TODO
        private void HashData()
        {
            long hash = -1;


            this.hashCode = hash;
        }

        public long GetHash { get { return hashCode; } }
    }
}
