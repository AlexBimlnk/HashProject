using System;
using System.Collections.Generic;
using System.Text;

namespace AppUI
{
    [Serializable]
    public class Account : IAccount
    {
        private enum Statuses
        {
            Normal,
            Banned,
            Freezed,
        };

        public int Balance { get; private set; }
        public string Login { get; private set; }
        public uint[] HashedPassword { get; private set; }
        public string Salt { get; private set; }

        private Statuses status;


        public Account(string _login, uint[] _hashedPassword, string _salt)
        {
            status = Statuses.Normal;

            Balance = 0;
            Login = _login;
            HashedPassword = _hashedPassword;
            Salt = _salt;
        }


        public string Status
        {
            get { return status.ToString(); }
        }


        public void AddMoney(int count)
        {
            if (count > 0)
                Balance += count;
        }

        public void Report()
        {

        }
    }
}
