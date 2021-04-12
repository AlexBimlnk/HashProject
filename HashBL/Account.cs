using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    public class Account: IAccount
    {
        private enum Statuses
        {
            Normal,
            Banned,
            Freezed,
            Deleted
        };

        private int balance;
        private string login;
        private string salt;
        private uint[] hashedpassword;
        private Statuses status;

        public Account(string newlogin, uint[] newhashedpassword, string newsalt)
        {
            login = newlogin;
            hashedpassword = newhashedpassword;
            salt = newsalt;
            balance = 0;
            status = Statuses.Normal;

            Balance = balance;
            Login = login;
            HashedPassword = hashedpassword;
            Salt = salt;
        }

        public int Balance 
        { 
            get { return balance; }
            private set { balance = value; }
        }
        public string Login
        {
            get { return login; }
            private set { login = value; }
        }
        public uint[] HashedPassword
        {
            get { return hashedpassword; }
            private set { hashedpassword = value; }
        }
        public string Salt
        {
            get { return salt; }
            private set { salt = value; }
        }

        public void AddMoney(int a)
        {
            if (a > 0)
                balance += a;
        }

        public void Report()
        {

        }
    }
}
