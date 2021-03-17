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

        /// <summary>
        /// Конструктор
        /// </summary>
        public HashStructure() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="hash">Head of tree</param>
        public HashStructure(long hash)
        {
            hashTree.Push(hash);
        }

        /// <summary>
        /// Добавление элемента в хеш-дерево
        /// </summary>
        /// <param name="hash">Хеш, который нужно добавить</param>
        public void AddHash(long hash)
        {
            if(hashTree.Count < maxItem)
                hashTree.Push(hash);
            else
                throw new Exception("Достигнут лимит кол-ва элементов");
                
        }

        /// <summary>
        /// Алгоритм хеширования данных
        /// </summary>
        /// <param name="data">Данные, которые нужно захешировать</param>
        /// <returns>Возвращает хеш данных</returns>
        public long HashData(string data)
        {
            long hash = -1;


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
