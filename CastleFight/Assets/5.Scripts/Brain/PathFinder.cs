using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    public static class PathFinder
    {

        private static SearchNode FindPathReversed(SimpleWorld2D world, Position2D start, Position2D end)
        {
            SearchNode first = new SearchNode(start, 0, 0, null);

            MinHeap<SearchNode> openList = new MinHeap<SearchNode>();
            openList.Enqueue(first);
            HashSet<Position2D> closedList = new HashSet<Position2D>();


            while (!openList.IsEmpty())
            {
                SearchNode current = openList.Dequeue();

                if (current.position.getDistance(end) <= 2)
                {
                    return new SearchNode(end, current.pathCost + 1, current.cost + 1, current);
                }

                foreach (KeyValuePair<Position2D, int> s in world.GetNeighbors())
                {
                    Position2D sur = s.Key;
                    Position2D p = new Position2D(current.position.x + sur.x, current.position.y + sur.y);


                    if (world.IsValidPosition(p) && !closedList.Contains(p))
                    {
                        closedList.Add(p);
                        int pathCost = current.pathCost + s.Value;
                        int cost = pathCost + p.getDistance(end);
                        SearchNode newNode = new SearchNode(p, cost, pathCost, current);
                        openList.Enqueue(newNode);
                    }
                }
            }

            return null;
        }

        public static SearchNode FindPath(SimpleWorld2D world, Position2D start, Position2D end)
        {
            return FindPathReversed(world, end, start);
        }
    }
}