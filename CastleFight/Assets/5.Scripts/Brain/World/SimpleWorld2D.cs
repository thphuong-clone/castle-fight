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

        public bool IsWalkable(Position2D position)
        {
            if (position.x < 0 || position.y < 0 || position.x >= width || position.y >= height)
                return false;
            return !gridMap[position.x, position.y];
        }

        public bool IsWalkable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return false;
            return !gridMap[x, y];
        }

        public bool HasLineOfSight(Position2D start, Position2D end)
        {
            int x0 = start.x;
            int y0 = start.y;
            int x1 = end.x;
            int y1 = end.y;
            int dx = x1 - x0;
            int dy = y1 - y0;

            int sx;
            int sy;

            if (dx < 0)
            {
                dx = -dx;
                sx = -1;
            }
            else
            {
                sx = 1;
            }
            if (dy < 0)
            {
                dy = -dy;
                sy = -1;
            }
            else
            {
                sy = 1;
            }

            int ix = 0;
            int iy = 0;
            while (ix < dx || iy < dy)
            {
                if ((0.5 + ix) / dx == (0.5 + iy) / dy)
                {
                    if (!IsWalkable(x0 + sx, y0) || !IsWalkable(x0, y0 + sy))
                        return false;
                    x0 += sx;
                    y0 += sy;
                    if (!IsWalkable(x0, y0))
                        return false;
                    ix++;
                    iy++;
                }
                else if ((0.5 + ix) / dx < (0.5 + iy) / dy)
                {
                    x0 += sx;
                    if (!this.IsWalkable(x0, y0))
                        return false;
                    ix++;
                }
                else
                {
                    y0 += sy;
                    if (!this.IsWalkable(x0, y0))
                        return false;
                    iy++;
                }
            }

            return true;
        }


        public static readonly Dictionary<Position2D, int> neighbors = new Dictionary<Position2D, int>
        {
            {new Position2D(-1, 1), 2}, {new Position2D(0, 1), 1}, {new Position2D(1,1), 2},
            {new Position2D(-1, 0), 1}, {new Position2D(1, 0), 1},
            {new Position2D(-1, -1), 2}, {new Position2D(0, -1), 1}, {new Position2D(1, -1), 2}
        };
    }

}