using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    [Serializable]
    public class Tree
    {
        private Node Head;
        private int count = 0;

        /// <summary>
        /// Добавление узлов в дерево
        /// </summary>
        /// <param name="data">Данные, которые будут храниться в узле</param>
        public void Push(long data)
        {
            Node tmp = new Node(data);
            if (Head == null)
                Head = tmp;
            else
                Head.Add(tmp);

            count++;
        }


        /// <summary>
        /// Выводит дерево узлов на экран
        /// </summary>
        public void PrintByTransverseBypass()
        {
            Head.PrintTransverseBypass();
        }

        /// <summary>
        /// ??
        /// </summary>
        public void PrintByLevel()
        {
            Queue<Node> tmp = new Queue<Node>();
            int level = 0;
            tmp.Enqueue(Head);

            Node temp;
            Console.Write($" level {level}: ");
            while (tmp.TryPeek(out temp))
            {

            }
        }

        public int Count { get { return count; } }
    }
}
