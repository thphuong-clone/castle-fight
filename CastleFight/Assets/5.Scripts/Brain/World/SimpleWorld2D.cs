using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    public class SimpleWorld2D
    {
        public int width;
        public int height;
        private bool[,] gridMap; //just free or not right now, consider adding cost later

        public SimpleWorld2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            gridMap = new bool[width, height];
        }

        public void SetPosition(Position2D position, bool status)
        {
            if (position.x < 0 || position.y < 0 || position.x >= width || position.y >= height)
                return;
            gridMap[position.x, position.y] = status;
        }

        public bool IsValidPosition(Position2D position)
        {
            if (position.x < 0 || position.y < 0 || position.x >= width || position.y >= height)
                return false;
            return !gridMap[position.x, position.y];
        }

        public Dictionary<Position2D, int> GetNeighbors()
        {
            return neighbors;
        }

        private static readonly Dictionary<Position2D, int> neighbors = new Dictionary<Position2D, int>
        {
            {new Position2D(-1, 1), 2}, {new Position2D(0, 1), 1}, {new Position2D(1,1), 2},
            {new Position2D(-1, 0), 1}, {new Position2D(1, 0), 1},
            {new Position2D(-1, -1), 2}, {new Position2D(0, -1), 1}, {new Position2D(1, -1), 2}
        };
    }

}