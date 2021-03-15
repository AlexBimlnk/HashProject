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
        [NonSerialized]
        private const int maxItem = 2;

        private const string path = "HashData";
        private Tree hashTree = new Tree();

        public HashStructure(long hash)
        {
            hashTree.Push(hash);
        }

        public void AddHash(long hash)
        {
            if(hashTree.Count < maxItem)
                hashTree.Push(hash);
            else
                throw new Exception("Достигнут лимит кол-ва элементов");
        }

        public void PrintByTransverseBypass()
        {
            hashTree.PrintByTransverseBypass();
        }

        public void Serealize(string nameFile)
        {
            BinaryFormatter format = new BinaryFormatter();

            FileStream file = new FileStream($@"{path}/{nameFile}.data", FileMode.Create);

            format.Serialize(file, this);
        }
    }
}
