using System;
using System.Collections.Generic;

namespace ConsoleModule
{
    class Node
    {
        long data;
        int level;
        Node R;
        Node L;
        public Node(int data)
        {
            level = 0;
            this.data = data;
            R = L = null;
        }
        public void Add(Node toAdd)
        {
            toAdd.level++;
            if (toAdd.data < this.data)
            {
                if (this.L == null)
                    this.L = toAdd;
                else
                    this.L.Add(toAdd);
            }
            else
            {
                if (this.R == null)
                    this.R = toAdd;
                else
                    this.R.Add(toAdd);
            }
        }
        public void PrintTransverseBypass()
        {
            if (this.L != null)
                L.PrintTransverseBypass();
            Console.Write(this.data);
            Console.Write('\t');
            if (this.R != null)
                R.PrintTransverseBypass();
        }
    }
    class Tree
    {
        Node Head;


        public void Push(int data)
        {
            Node tmp = new Node(data);
            if (Head == null)
                Head = tmp;
            else
                Head.Add(tmp);
        }

        public void PrintByTransverseBypass()
        {
            Head.PrintTransverseBypass();
        }

        public void PrintByLevel()
        {
            Queue<Node> tmp = new Queue<Node>();
            int l = 0;
            tmp.Enqueue(Head);

            Node t;
            Console.Write($" level {l}: ");
            while (tmp.TryPeek(out t))
            {
                
            }
        }
    }
    class Program
    {
        static long HashForText(string s)
        {
            long ans = 0;
            int r = 28;
            long rr = 1;

            for (int i = 0;i < s.Length;i++)
            {
                ans += (s[i] - 'a' + 1) * rr;
                rr *= r;
            }

            return ans;
        }
        static void Main(string[] args)
        {
            Tree test = new Tree();
            test.Push(6);
            test.Push(8);
            test.Push(5);
            test.PrintByTransverseBypass();

            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
