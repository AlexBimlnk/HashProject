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
        private const int maxItems = 5;
        private Dictionary<ulong, List<ulong>> hashMap = new Dictionary<ulong, List<ulong>>(maxItems);


        public HashMap() { }

        public HashMap(ValueTuple<ulong, ulong> tuple)
        {
            hashMap.Add(tuple.Item1, new List<ulong>() { tuple.Item2 });
        }



        /// <summary>
        /// Добавление элемента в хеш-дерево с парой данных
        /// </summary>
        public void AddHash(ValueTuple<ulong, ulong> tuple)
        {
            if (hashMap.Count < maxItems)
            {
                //Если хеш-ключ уже существует сравниваем со списком и добавляем новое значение
                if (hashMap.ContainsKey(tuple.Item1))
                    if (!hashMap[tuple.Item1].Contains(tuple.Item2))
                        hashMap[tuple.Item1].Add(tuple.Item2);
                else
                    hashMap.Add(tuple.Item1, new List<ulong>() { tuple.Item2 });
            }
            else
                throw new Exception("Достигнут лимит кол-ва элементов");
        }

        /// <summary>
        /// Поиск в хеш-словаре по хеш-ключу
        /// </summary>
        /// <param name="key"> Ключ искомого значения </param>
        public void Search(ulong key)
        {

        }

        /// <summary>
        /// Сериализует хеш-словарь класса в бинарный файл
        /// </summary>
        /// <param name="path">Путь к создаваемому файлу</param>
        public void Serealize(string path)
        {
            BinaryFormatter format = new BinaryFormatter();

            using (FileStream file = new FileStream($@"{path}.data", FileMode.Create))
            {
                format.Serialize(file, this.hashMap);
            }
        }


        /// <summary>
        /// Алгоритм хеширования данных
        /// </summary>
        /// <param name="data"> Данные, которые нужно захешировать </param>
        /// <returns> Хеш данных </returns>
        public ulong GetHashCode(string data)
        {
            ulong hash = 0;



            return hash;
        }
    }
}