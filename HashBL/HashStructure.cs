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
        /// Constructor
        /// </summary>
        public HashStructure() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hash">Head of tree</param>
        public HashStructure(long hash)
        {
            hashTree.Push(hash);
        }

        /// <summary>
        /// Adding an item to the hash tree
        /// </summary>
        /// <param name="hash"></param>
        public void AddHash(long hash)
        {
            if(hashTree.Count < maxItem)
                hashTree.Push(hash);
            else
                throw new Exception("Достигнут лимит кол-ва элементов");
                
        }

        /// <summary>
        /// Print a list of tree nodes to the screen
        /// </summary>
        public void PrintByTransverseBypass()
        {
            hashTree.PrintByTransverseBypass();
        }


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
