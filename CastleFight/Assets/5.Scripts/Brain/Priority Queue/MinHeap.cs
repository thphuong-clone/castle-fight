using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder
{
    public sealed class MinHeap<T> where T : SearchNode
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
            try
            {
                T first = head;
                head = (T)head.nextInQueue;
                return first;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public T GetFirst()
        {
            return head;
        }

        public void Clear()
        {
            this.head = null;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            T node = head;

            builder.Append(head.position.ToString() + ": " + node.cost + "; ");

            while (node.nextInQueue != null)
            {
                builder.Append(node.position.ToString() + ": " + node.cost);
                node = (T)node.nextInQueue;
                if (node.nextInQueue != null)
                    builder.Append("; ");
            }

            return builder.ToString();
        }
    }
}
