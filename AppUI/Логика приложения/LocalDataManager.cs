using System;
using System.IO;
using HashBL;
using System.Collections.Generic;
using System.Text;

namespace AppUI
{
    class LocalDataManager : IDataManager
    {
        public string PathData { get; private set; }

        private string nowFileName;
        private HashMap<Account> hashMap = new HashMap<Account>();

        public LocalDataManager()
        {
            LoadData("HashData");
        }


        public void SaveData()
        {
            throw new NotImplementedException();
        }

        public bool SearchUser(ulong key, out Account value)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Загружает данные в ОЗУ.
        /// </summary>
        /// <param name="folderPath"> Папка с данными. </param>
        /// <param name="fileDataType"> Расширение файлов с данными (например data) </param>
        public void LoadData(string folderPath, string fileDataType = "data")
        {
            PathData = folderPath;

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
    }
}
