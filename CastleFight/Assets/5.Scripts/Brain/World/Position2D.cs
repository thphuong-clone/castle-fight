using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    public class Position2D
    {
        public int x;
        public int y;

        public Position2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getDistance(Position2D other)
        {
            int dx = this.x - other.x;
            int dy = this.y - other.y;
            return dx * dx + dy * dy;
        }

        public float OctileHeuristic(Position2D other)
        {
            int dx = Math.Abs(this.x - other.x);
            int dy = Math.Abs(this.y - other.y);

            return (float)(Math.Min(dx, dy) * Math.Sqrt(2) + Math.Max(dx, dy) - Math.Min(dx, dy));
        }

        public override int GetHashCode()
        {
            return (this.x + " " + this.y).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (this.x == ((Position2D) obj).x && this.y == ((Position2D)obj).y);
        }

        public override string ToString()
        {
            return this.x + ";" + this.y;
        }
    } 
}
