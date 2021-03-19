using System;

namespace HashBL
{
    [Serializable]
    internal class Node
    {
        private ValueTuple<ulong, ulong> tuple;
        private ulong data;
        private int level = 0;
        private Node rightNode;
        private Node leftNode;

        /// <summary>
        /// Конструктор с реализацией хранения данных в кортеже
        /// </summary>
        public Node(ValueTuple<ulong, ulong> _tuple)
        {
            this.tuple = _tuple;
        }
        public Node(ulong _data)
        {
            data = _data;
        }


        public void Add(Node toAdd)
        {
            toAdd.level++;
            if (toAdd.data < this.data)
            {
                if (this.leftNode == null)
                    this.leftNode = toAdd;
                else
                    this.leftNode.Add(toAdd);
            }
            else
            {
                if (this.rightNode == null)
                    this.rightNode = toAdd;
                else
                    this.rightNode.Add(toAdd);
            }
        }

        public void PrintTransverseBypass()
        {
            if (this.leftNode != null)
                leftNode.PrintTransverseBypass();
            Console.Write($"{this.data}\t");
            if (this.rightNode != null)
                rightNode.PrintTransverseBypass();
        }

        //public long Data { get { return data; } }
    }
}
