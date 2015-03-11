using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    public class SearchNode
    {
        public Position2D position;
        //estimated cost to reach goal
        public int cost;
        //cost so far
        public int pathCost;
        //parent
        public SearchNode next;
        public SearchNode nextInQueue;
        public bool closed;

        public SearchNode(Position2D position, int cost, int pathCost, SearchNode next)
        {
            this.position = position;
            this.cost = cost;
            this.pathCost = pathCost;
            this.next = next;
            //closed = false;
            this.nextInQueue = null;
        }
    } 
}
