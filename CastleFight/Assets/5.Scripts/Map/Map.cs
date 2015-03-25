using UnityEngine;
using System;
using System.Collections.Generic;
using PathFinder;

public class Map : MonoBehaviour
{
    private const int width = 18;
    private const int height = 32;

    public List<Obstacle> obstacles;
    public bool[,] gridMap;

    void Awake()
    {
        if (obstacles != null && obstacles.Count > 0)
        {
            gridMap = GetGridMap();
        }
    }

    public bool [,] GetGridMap()
    {
        bool[,] returnMap = new bool [width,height];

        foreach (Obstacle o in obstacles)
        {
            for (int i = 0; i < o.width; i++)
            {
                for (int j = 0; j < o.height; j++ )
                {
                    returnMap[o.topLeft.x + i, o.topLeft.y + j] = true;
                }
            }
        }

        return returnMap;
    }

    [Serializable]
    public struct Obstacle
    {
        public Position2D topLeft;
        public int width;
        public int height;
    }
}
