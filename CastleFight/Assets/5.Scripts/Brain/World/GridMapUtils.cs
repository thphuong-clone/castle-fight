using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinder
{
    public class GridMapUtils
    {
        private static float tileHeight = 0.5f;
        private static float tileWidth = 0.5f;

        //
        //NO custom grid map size for now, just use 9:16
        //Everything is preset
        //Cell size is 1
        //Top left corner is x = -4.5; y = 8
        //
        public static Position2D GetTile(float x, float y)
        {
            //convert zero point centric map to zero point top left map
            float topLeftX = (x - (-4.5f))/tileHeight;
            float topLeftY = (8 - y)/tileWidth;

            //note: grid map is upside down
            //note2: no longer upside down


            int intX = (int)topLeftX;
            int intY = (int)topLeftY;

            if (intX >= 17)
                intX = 17;

            if (intY >= 31)
                intY = 31;

            return new Position2D(intX, intY);
        }

        public static Position2D GetTile(Vector2 position)
        {
            //convert zero point centric map to zero point top left map
            float topLeftX = (position.x - (-4.5f))/tileHeight;
            float topLeftY = (8 - position.y)/tileWidth;

            int intX = (int)topLeftX;
            int intY = (int)topLeftY;

            if (intX == 9)
                intX = 8;

            if (intY == 16)
                intY = 15;

            return new Position2D(intX, intY);
        }

        public static Vector2 GetRealPosition(Position2D position)
        {

            float top = 8 - position.y*tileHeight;
            float bottom = top - 1*tileHeight;
            float left = position.x*tileWidth + (-4.5f);
            float right = left + 1*tileWidth;
            return new Vector2(((left + right) / 2), ((top + bottom) / 2));
        }

        public static Vector2 GetRealPosition(int x, int y)
        {
            float top = 8 - y*tileHeight;
            float bottom = top - 1*tileHeight;
            float left = x*tileWidth + (-4.5f);
            float right = left + 1*tileWidth;
            return new Vector2(((left + right) / 2)/tileHeight, ((top + bottom) / 2))/tileWidth;
        }

        public static SimpleWorld2D MakeWorld()
        {
            SimpleWorld2D world = new SimpleWorld2D(18, 32);

            foreach (List<Building> lb in PlayerController.p1_buildingList)
            {
                foreach (Building b in lb)
                {
                    //Debug.Log(b.gameObject.transform.localPosition.x + " " + b.gameObject.transform.localPosition.y);
                    //Debug.Log(GetTile(b.transform.position.x, b.transform.position.y));
                    Position2D buildingPosition = GetTile(b.transform.position.x, b.transform.position.y);
                    world.SetPosition(buildingPosition, b.GetOccupyingGrid(), true);
                    //Debug.Log("end " + b.name);
                }
            }

            //Debug.Log("end red");

            foreach (List<Building> lb in PlayerController.p2_buildingList)
            {
                foreach (Building b in lb)
                {
                    //Debug.Log(GetTile(b.transform.position.x, b.transform.position.y));
                    Position2D buildingPosition = GetTile(b.transform.position.x, b.transform.position.y);
                    world.SetPosition(buildingPosition, b.GetOccupyingGrid(), true);
                    //Debug.Log("end " + b.name);
                }
            }

            //Debug.Log("end blue");

            return world;
        }

        public static SimpleWorld2D MakeWorld(SimpleWorld2D persistentWorld)
        {
            if (persistentWorld != null)
            {
                SimpleWorld2D world = new SimpleWorld2D(persistentWorld);

                foreach (List<Building> lb in PlayerController.p1_buildingList)
                {
                    foreach (Building b in lb)
                    {
                        Position2D buildingPosition = GetTile(b.transform.position.x, b.transform.position.y);
                        world.SetPosition(buildingPosition, b.GetOccupyingGrid(), true);
                    }
                }

                foreach (List<Building> lb in PlayerController.p2_buildingList)
                {
                    foreach (Building b in lb)
                    {
                        Position2D buildingPosition = GetTile(b.transform.position.x, b.transform.position.y);
                        world.SetPosition(buildingPosition, b.GetOccupyingGrid(), true);
                    }
                }

                return world;
            }
            else
                return MakeWorld();
        }

        public static bool IsInsideGridCell(Vector2 position, Position2D cellPosition)
        {
            float top = 8 - cellPosition.y;
            float bottom = top - 1;
            float left = cellPosition.x + (-4.5f);
            float right = left + 1;
            return (position.x >= left && position.x <= right && position.y <= top && position.y >= bottom);
        }

        public static void PrintMap(SimpleWorld2D world)
        {
            bool[,] map = world.GetGridMap();

            int rowLength = map.GetLength(0);
            int colLength = map.GetLength(1);

            System.Text.StringBuilder mapStringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (map[i, j])
                        mapStringBuilder.Append("x ");
                    else
                        mapStringBuilder.Append(". ");
                }
                mapStringBuilder.Append("\n");
            }

            Debug.Log(mapStringBuilder.ToString());
        }

        private class GridCell
        {
            public float top;
            public float bottom;
            public float left;
            public float right;
            public Vector2 center;

            public GridCell(Position2D position)
            {
                top = position.y;
                bottom = position.y + 1;
                left = position.x;
                right = position.x + 1;
                center = new Vector2((left + right) / 2, (top + bottom) / 2);
            }
        }
    }
}
