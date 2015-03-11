using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    public static class PathFinder
    {
        public static SearchNode FindPath(SimpleWorld2D world, Position2D start, Position2D end)
        {
            return FindPathReversed(world, end, start);
        }

        private static SearchNode FindPathReversed(SimpleWorld2D world, Position2D start, Position2D end)
        {
            SearchNode first = new SearchNode(start, 0, 0, null);

            MinHeap<SearchNode> openList = new MinHeap<SearchNode>();
            openList.Enqueue(first);
            HashSet<Position2D> closedList = new HashSet<Position2D>();


            int i = 0;
            while (!openList.IsEmpty())
            {
                SearchNode current = openList.Dequeue();
                Position2D currentPosition = current.position;

                if (currentPosition.getDistance(end) <= 2)
                {
                    if (current.next != null)
                    {
                        if (world.HasLineOfSight(current.next.position, end))
                        {
                            int pathCost = current.next.pathCost + current.next.position.getDistance(end);
                            int cost = pathCost + current.next.position.getDistance(end);
                            return new SearchNode(end, pathCost, cost, current.next);
                        }
                        return new SearchNode(end, current.pathCost + 1, current.cost + 1, current);
                    }
                    else
                    {
                        return new SearchNode(end, current.pathCost + 1, current.cost + 1, current);
                    }
                }

                foreach (KeyValuePair<Position2D, int> s in SimpleWorld2D.neighbors)
                {
                    Position2D sur = s.Key;

                    //prevent cutting corner
                    if (s.Value == 2)
                    {
                        if (!world.IsWalkable(currentPosition.x + sur.x, currentPosition.y)
                            || !world.IsWalkable(currentPosition.x, currentPosition.y + sur.y))
                            continue;
                    }

                    Position2D p = new Position2D(currentPosition.x + sur.x, currentPosition.y + sur.y);


                    if (world.IsWalkable(p) && !closedList.Contains(p))
                    {
                        closedList.Add(p);
                        SearchNode newNode;
                        if (current.next != null)
                        {
                            if (world.HasLineOfSight(current.next.position, p))
                            {
                                int pathCost = current.next.pathCost + current.next.position.getDistance(p);
                                int cost = pathCost + p.getDistance(end);
                                newNode = new SearchNode(p, cost, pathCost, current.next);
                            }
                            else
                            {
                                Console.Out.WriteLine("No LOS from " + current.next.position.ToString() + " to " + p);
                                int pathCost = current.pathCost + s.Value;
                                int cost = pathCost + p.getDistance(end);
                                newNode = new SearchNode(p, cost, pathCost, current);
                            }
                        }
                        else
                        {
                            int pathCost = current.pathCost + s.Value;
                            int cost = pathCost + p.getDistance(end);
                            newNode = new SearchNode(p, cost, pathCost, current);
                        }
                        openList.Enqueue(newNode);
                    }
                }

                i++;
            }

            return null;
        }

    }
}