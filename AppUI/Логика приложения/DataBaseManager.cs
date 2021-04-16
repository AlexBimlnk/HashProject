using System;

namespace AppUI
{
    /// <summary>
    /// Класс для сохранения данных в БД
    /// </summary>
    class DataBaseManager : IDataManager
    {
        public string PathData => throw new NotImplementedException();

        public DataBaseManager()
        {
            //TODO: 
        }

        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public bool SearchUser(ulong key, out Account value)
        {
            throw new NotImplementedException();
        }

        public void AddData(string login, string password)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(Account value)
        {

        }
    }
}
