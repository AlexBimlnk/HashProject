using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace HashBL
{
    public class HashMap
    {
        private const int maxItems = 2;
        private Dictionary<ulong, uint[]> hashMap = new Dictionary<ulong, uint[]>(maxItems);
        private BinaryFormatter format = new BinaryFormatter();

        public HashMap() { }

        public HashMap(ulong loginHash, uint[] passwordHashData)
        {
            hashMap.Add(loginHash, passwordHashData);
        }



        /// <summary>
        /// Добавление элемента в хеш-словарь
        /// </summary>
        /// <param name="loginHash"> Ключ </param>
        /// <param name="passwordHashData"> Значение </param>
        /// <exception cref="OverflowException"> Возникает когда достигнуто макс. кол-во элементов </exception>
        /// <exception cref="ArgumentException"> Возникает при попытке добавить одинаковых пользователей </exception>
        public void AddHash(ulong loginHash, uint[] passwordHashData)
        {
            if (Search(loginHash))
                throw new ArgumentException("Такой логин уже есть");
            else
            {
                if (hashMap.Count < maxItems)
                    hashMap[loginHash] = passwordHashData;
                else
                    throw new OverflowException("Достигнуто максимальное кол-во элементов");
            }
        }

        /// <summary>
        /// Поиск в хеш-словаре по хеш-ключу
        /// </summary>
        /// <param name="key"> Ключ искомого значения </param>
        /// <returns> true если ключ найден </returns>
        public bool Search(ulong key)
        {
            return hashMap.ContainsKey(key);
        }

        /// <summary>
        /// Сериализует хеш-словарь класса в бинарный файл
        /// </summary>
        /// <param name="path">Путь к создаваемому файлу</param>
        public void Serealize(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                format.Serialize(file, this.hashMap);
                hashMap = new Dictionary<ulong, uint[]>(maxItems);
            }
        }

        /// <summary>
        /// Десериализует бинарный файл в хеш-словарь
        /// </summary>
        /// <param name="path"> Путь к десериализуемому файлу </param>
        /// <exception cref="FileNotFoundException"> Путь не указывает на существующий файл  </exception>
        public void Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    hashMap = (Dictionary<ulong, uint[]>)format.Deserialize(file);
                }
            }
            else
                throw new Exception("Такого файла не существует.");
        }

        public Dictionary<ulong, uint[]> GetDict
        {
            get { return hashMap; }
            set { hashMap = value; }
        }
    }
}