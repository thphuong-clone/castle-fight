using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AStar
{
    public sealed class MinHeap<T> where T : QueueNode
    {
        private T head;

        public MinHeap()
        {
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        //use cost for priority for now
        public void Enqueue(T node)
        {
            if (head == null)
            {
                head = node;
            }
            else if (node.cost <= head.cost)
            {
                node.nextInQueue = head;
                this.head = node;
            }
            else
            {
                T parent = head;
                while (parent.nextInQueue != null && parent.nextInQueue.cost < node.cost)
                {
                    parent = (T)parent.nextInQueue;
                }
                node.nextInQueue = parent.nextInQueue;
                parent.nextInQueue = node;
            }
        }

        public T Dequeue()
        {
            T first = head;
            head = (T)head.nextInQueue;
            return first;
        }

        public override string ToString()
        {
            string s = "";

            T node = head;

            while (node.nextInQueue != null)
            {
                Console.Write(node.position.ToString() + ": " + node.cost + "; ");
                node = (T)node.nextInQueue;
            }

            return s;
        }
    }
}
