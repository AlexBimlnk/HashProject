using System;
using System.IO;
using HashBL;

namespace AppUI
{
    /// <summary>
    /// Класс для локального сохранения данных
    /// </summary>
    class LocalDataManager : IDataManager
    {
        public string PathData { get; private set; }

        private string nowFileName;
        private string fileDataType = "data";
        private HashMap<Account> hashMap = new HashMap<Account>();

        public LocalDataManager()
        {
            LoadData("HashData");
        }


        public bool SearchUser(ulong key, out Account value)
        {
            value = hashMap.Search(key);

            //Если в ОЗУ нет хеша
            if (value == null)
            {
                HashMap<Account> tempHashMap = new HashMap<Account>();
                foreach (var i in Directory.GetFiles(PathData, "*.data"))
                {
                    tempHashMap.Deserialize(i);
                    value = tempHashMap.Search(key);

                    //Если нашли пользователя
                    if (value != null)
                        return true;
                }
            }

            return value == null ? false : true;
        }

        /// <summary>
        /// Загружает данные в ОЗУ.
        /// </summary>
        /// <param name="folderPath"> Папка с данными. </param>
        /// <param name="fileDataType"> Расширение файлов с данными (например data). </param>
        public void LoadData(string folderPath, string fileType = "data")
        {
            PathData = folderPath;
            fileDataType = fileType;

            // Создаем папку для хранения данных, если таковая отсутствует 
            if (!Directory.Exists(PathData))
                Directory.CreateDirectory(PathData);

            string[] fileNames = Directory.GetFiles(PathData, $"*.{fileDataType}");
            if (fileNames.Length > 0)
            {
                hashMap.Deserialize(fileNames[fileNames.Length - 1]);
                nowFileName = $@"{PathData}\file{fileNames.Length - 1}.{fileDataType}";
            }
            else
                nowFileName = $@"{PathData}\file{fileNames.Length}.{fileDataType}";
        }

        public void SaveData()
        {
            hashMap.Serealize(nowFileName);
        }

        public void AddData(string login, string password)
        {
            string salt = Hashing.GetSalt();
            try
            {
                hashMap.AddHash(Hashing.GetHash(login), new Account(login, Hashing.GetShaHash(password + salt), salt));
            }
            catch (OverflowException)
            {
                hashMap.Serealize(nowFileName);
                nowFileName = $@"{PathData}\file{Directory.GetFiles(PathData, $"*.{fileDataType}").Length}.{fileDataType}";
                hashMap.AddHash(Hashing.GetHash(login), new Account(login, Hashing.GetShaHash(password + salt), salt));
                throw new OverflowException();
            }
        }

        public void UpdateData(Account value)
        {
            ulong key = Hashing.GetHash(value.Login);
            hashMap.UpdateValue(key, value);
        }
    }
}
