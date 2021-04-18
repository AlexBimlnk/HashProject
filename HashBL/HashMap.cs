using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HashBL
{
    public class HashMap <TValue> : IHashTable <TValue>
    {
        public int Count => hashMap.Count;
        public int MaxItemCount { get; private set; } = 200000;


        private Dictionary<ulong, TValue> hashMap;
        private BinaryFormatter format = new BinaryFormatter();


        public HashMap() 
        {
            hashMap = new Dictionary<ulong, TValue>(MaxItemCount);
        }

        public HashMap(int maxItemCount)
        {
            MaxItemCount = maxItemCount;
            hashMap = new Dictionary<ulong, TValue>(MaxItemCount);
        }


        public void AddHash(ulong loginHash, TValue value)
        {
            if (Contains(loginHash))
                throw new ArgumentException("Такой логин уже есть");

            else if(Count < MaxItemCount)
                hashMap[loginHash] = value;
            else
                throw new OverflowException("Достигнуто максимальное кол-во элементов");
        }

        public bool Contains(ulong key)
        {
            return hashMap.ContainsKey(key);
        }

        public TValue GetValueByKey(ulong key)
        {
            if (hashMap.ContainsKey(key))
                return hashMap[key];

            return default;
        }

        public void Serealize(string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                format.Serialize(file, hashMap);
                hashMap = new Dictionary<ulong, TValue>(MaxItemCount);
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