using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    //should be wraper class, just leave it like this for now
    public class SearchNode
    {
        public Position2D position;
        public int cost;
        public int pathCost;
        //public QueueNode prev;
        public SearchNode next;
        public SearchNode nextInQueue;
        public bool closed;

        public SearchNode(Position2D position, int cost, int pathCost, SearchNode next)
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
