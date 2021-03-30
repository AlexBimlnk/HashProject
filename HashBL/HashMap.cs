using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HashBL
{
    public class HashMap <TValue> : IHashTable <TValue>
    {
        private const int maxItems = 200000;
        private Dictionary<ulong, TValue> hashMap = new Dictionary<ulong, TValue>(maxItems);
        private BinaryFormatter format = new BinaryFormatter();

        public int Count => hashMap.Count;

        public HashMap() { }
        
        public HashMap(ulong loginHash, TValue value)
        {
            hashMap.Add(loginHash, value);
        }


        public void AddHash(ulong loginHash, TValue value)
        {
            if (Search(loginHash) != null)
                throw new ArgumentException("Такой логин уже есть");

            else if(hashMap.Count < maxItems)
                hashMap[loginHash] = value;
            else
                throw new OverflowException("Достигнуто максимальное кол-во элементов");
        }

        public TValue Search(ulong key)
        {
            if (hashMap.ContainsKey(key))
                return hashMap[key];

            return default;
        }

        public void Serealize(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                format.Serialize(file, this.hashMap);
                hashMap = new Dictionary<ulong, TValue>(maxItems);
            }
        }

        public void Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    hashMap = (Dictionary<ulong, TValue>)format.Deserialize(file);
                }
            }
            else
                throw new Exception("Такого файла не существует.");
        }

        public Dictionary<ulong, TValue> GetHashDict
        {
            get { return hashMap; }
            set { hashMap = value; }
        }
    }
}