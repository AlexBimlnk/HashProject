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
        private Dictionary<ulong, Tuple<uint[], string>> _hashMap = new Dictionary<ulong, Tuple<uint[], string>>(maxItems);

        private BinaryFormatter format = new BinaryFormatter();

        public HashMap() { }

        
        public HashMap(ulong loginHash, uint[] passwordHashData, string salt)
        {
            Tuple<uint[], string> Value = new Tuple<uint[], string>(passwordHashData, salt);
            _hashMap.Add(loginHash, Value);
        }

        /// <summary>
        /// Добавление элемента в хеш-словарь
        /// </summary>
        /// <param name="loginHash"> Ключ </param>
        /// <param name="passwordHashData"> Значение </param>
        /// <exception cref="OverflowException"> Возникает когда достигнуто макс. кол-во элементов </exception>
        /// <exception cref="ArgumentException"> Возникает при попытке добавить одинаковых пользователей </exception>
        public void AddHash(ulong loginHash, uint[] passwordHashData, string salt)
        {
            if (Search(loginHash))
                throw new ArgumentException("Такой логин уже есть");
            else
            {
                if (_hashMap.Count < maxItems)
                {
                    Tuple<uint[], string> Value = new Tuple<uint[], string>(passwordHashData, salt);
                    _hashMap[loginHash] = Value;
                }
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
            return _hashMap.ContainsKey(key);
        }
        public bool SearchWithData(ulong key, ref Tuple<uint[], string> Value)
        {
            if (_hashMap.ContainsKey(key))
            {
                Value = _hashMap[key];
                return true;
            }
            return false;
        }
        public Tuple<uint[], string> GetData(ulong key)
        {
            Tuple<uint[], string> Value = null;
            if (_hashMap.ContainsKey(key))
            {
                Value = _hashMap[key];
            }
            return Value;
        }

        /// <summary>
        /// Сериализует хеш-словарь класса в бинарный файл
        /// </summary>
        /// <param name="path">Путь к создаваемому файлу</param>
        public void Serealize(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                format.Serialize(file, this._hashMap);
                _hashMap = new Dictionary<ulong, Tuple<uint[], string>>(maxItems);
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
                    _hashMap = (Dictionary<ulong, Tuple<uint[], string>>)format.Deserialize(file);
                }
            }
            else
                throw new Exception("Такого файла не существует.");
        }

        public Dictionary<ulong, Tuple<uint[], string>> GetDict
        {
            get { return _hashMap; }
            set { _hashMap = value; }
        }
    }
}