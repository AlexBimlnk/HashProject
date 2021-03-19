using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace HashBL
{
    [Serializable]
    public class HashStructure
    {
        private const int maxItem = 2;

        private Tree hashTree = new Tree();


        public HashStructure() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="hash">Head of tree</param>
        public HashStructure(ulong hash)
        {
            hashTree.Push(hash);
        }

        /// <summary>
        /// Конструктор для создания данных с парой данных ключ-значение
        /// </summary>
        public HashStructure(ValueTuple<ulong, ulong> tuple)
        {
            hashTree.Push(tuple);
        }



        /// <summary>
        /// Добавление элемента в хеш-дерево
        /// </summary>
        /// <param name="hash">Хеш, который нужно добавить</param>
        public void AddHash(ulong hash)
        {
            if(hashTree.Count < maxItem)
                hashTree.Push(hash);
            else
                throw new Exception("Достигнут лимит кол-ва элементов");   
        }

        /// <summary>
        /// Добавление элемента в хеш-дерево с парой данных
        /// </summary>
        public void AddHash(ValueTuple<ulong, ulong> tuple)
        {
            if (hashTree.Count < maxItem)
                hashTree.Push(tuple);
            else
                throw new Exception("Достигнут лимит кол-ва элементов");
        }



        public bool Search(ulong hash)
        {
            bool find = false;


            return find;
        }

        /// <summary>
        /// Алгоритм хеширования данных
        /// </summary>
        /// <param name="data">Данные, которые нужно захешировать</param>
        /// <returns>Возвращает хеш данных</returns>
        public ulong HashData(string data)
        {
            ulong hash = 0;


            return hash;
        }


        /// <summary>
        /// Выводит на экран список узлов дерева
        /// </summary>
        public void PrintByTransverseBypass()
        {
            hashTree.PrintByTransverseBypass();
        }

        /// <summary>
        /// Сериализует объект класса в бинарный файл
        /// </summary>
        /// <param name="path">Путь к создаваемому файлу</param>
        public void Serealize(string path)
        {
            BinaryFormatter format = new BinaryFormatter();

            using (FileStream file = new FileStream($@"{path}.data", FileMode.Create))
            {
                format.Serialize(file, this);
            }
        }
    }
}
