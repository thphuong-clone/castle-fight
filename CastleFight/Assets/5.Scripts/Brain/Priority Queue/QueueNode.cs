using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    //should be wraper class, just leave it like this for now
    public class QueueNode
    {
        public Position2D position;
        public int cost;
        public int pathCost;
        //public QueueNode prev;
        public QueueNode next;
        public QueueNode nextInQueue;
        public bool closed;

        public QueueNode(Position2D position, int cost, int pathCost, QueueNode next)
        {
            this.position = position;
            this.cost = cost;
            this.pathCost = pathCost;
            this.next = next;
            closed = false;
            this.nextInQueue = null;
        }
    } 
}
