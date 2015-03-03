using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinder
{
    public class GridMapUtils
    {
        //
        //NO custom grid map size for now, just use 9:16
        //Everything is preset
        //Cell size is 1
        //Top left corner is x = -4.5; y = 8
        //
        public static Position2D GetTile(float x, float y)
        {
            //convert zero point centric map to zero point top left map
            float topLeftX = x - (-4.5f);
            float topLeftY = 8 - y;

            //note: grid map is upside down
            //note2: no longer upside down


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
            float top = 8 - position.y;
            float bottom = top - 1;
            float left = position.x + (-4.5f);
            float right = left + 1;
            return new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public static SimpleWorld2D MakeWorld()
        {
            SimpleWorld2D world = new SimpleWorld2D(9, 16);

            foreach (List<Building> lb in PlayerController.p1_buildingList)
            {
                foreach (Building b in lb)
                {
                    //Debug.Log(b.gameObject.transform.localPosition.x + " " + b.gameObject.transform.localPosition.y);
                    Debug.Log(GetTile(b.transform.position.x, b.transform.position.y));
                    Position2D buildingPosition = GetTile(b.transform.position.x, b.transform.position.y);
                    world.SetPosition(buildingPosition, true);
                }
            }
            foreach (List<Building> lb in PlayerController.p2_buildingList)
            {
                foreach (Building b in lb)
                {
                    //Debug.Log(GetTile(b.transform.position.x, b.transform.position.y));
                    Position2D buildingPosition = GetTile(b.transform.position.x, b.transform.position.y);
                    world.SetPosition(buildingPosition, true);
                }
            }

            return world;
        }

        public static bool IsInsideGridCell(Vector2 position, Position2D cellPosition)
        {
            float top = 8 - cellPosition.y;
            float bottom = top - 1;
            float left = cellPosition.x + (-4.5f);
            float right = left + 1;
            return (position.x >= left && position.x <= right && position.y <= top && position.y >= bottom);
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
