using System;
using System.Collections.Generic;
using System.Linq;

namespace AStar
{
    public static class PathFinder
    {
        private static QueueNode FindPathReversed(SimpleWorld2D world, Position2D start, Position2D end)
        {
            QueueNode first = new QueueNode(start, 0, 0, null);

            MinHeap<QueueNode> openList = new MinHeap<QueueNode>();
            openList.Enqueue(first);
            HashSet<Position2D> closedList = new HashSet<Position2D>();

            //Console.Out.WriteLine((new Position2D(7, 7)).getDistance(new Position2D(7, 6)));

            while (!openList.IsEmpty())
            {
                //Console.Out.WriteLine(openList);
                QueueNode current = openList.Dequeue();

                if (current.position.getDistance(end) <= 2)
                {
                    return new QueueNode(end, current.pathCost + 1, current.cost + 1, current);
                }

                foreach (KeyValuePair<Position2D, int> s in world.getSurroundingPoint())
                {
                    Position2D sur = s.Key;
                    Position2D p = new Position2D(current.position.x + sur.x, current.position.y + sur.y);

                    //Console.Out.WriteLine("current: " + current.position.ToString());

                    if (world.IsValidPosition(p) && !closedList.Contains(p))
                    {
                        //Console.Out.WriteLine(p.ToString());
                        closedList.Add(p);
                        int pathCost = current.pathCost + s.Value;
                        int cost = pathCost + p.getDistance(end);
                        QueueNode newNode = new QueueNode(p, cost, pathCost, current);
                        openList.Enqueue(newNode);
                    }
                }
                //Console.ReadLine();
            }

            return null;
        }

        public static QueueNode FindPath(SimpleWorld2D world, Position2D start, Position2D end)
        {
            return FindPathReversed(world, end, start);
        }
    }
}