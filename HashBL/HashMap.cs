using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HashBL
{
    public class HashMap : IHashTable
    {
        public const int maxItems = 2;
        private Dictionary<ulong, Tuple<uint[], string>> hashMap = new Dictionary<ulong, Tuple<uint[], string>>(maxItems);

        private BinaryFormatter format = new BinaryFormatter();

        public HashMap() { }
        
        public HashMap(ulong loginHash, uint[] passwordHashData, string salt)
        {
            Tuple<uint[], string> Value = new Tuple<uint[], string>(passwordHashData, salt);
            hashMap.Add(loginHash, Value);
        }


        public void AddHash(ulong loginHash, uint[] passwordHashData, string salt)
        {
            if (Search(loginHash) != null)
                throw new ArgumentException("Такой логин уже есть");
            else
            {
                if (hashMap.Count < maxItems)
                {
                    Tuple<uint[], string> Value = new Tuple<uint[], string>(passwordHashData, salt);
                    hashMap[loginHash] = Value;
                }
                else
                    throw new OverflowException("Достигнуто максимальное кол-во элементов");
            }
        }

        public Tuple<uint[], string> Search(ulong key)
        {
            if (hashMap.ContainsKey(key))
                return hashMap[key];

            return null;
        }

        public void Serealize(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                format.Serialize(file, this.hashMap);
                hashMap = new Dictionary<ulong, Tuple<uint[], string>>(maxItems);
            }
        }

        public void Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    hashMap = (Dictionary<ulong, Tuple<uint[], string>>)format.Deserialize(file);
                }
            }
            else
                throw new Exception("Такого файла не существует.");
        }

        public Dictionary<ulong, Tuple<uint[], string>> GetDict
        {
            get { return hashMap; }
            set { hashMap = value; }
        }
    }
}