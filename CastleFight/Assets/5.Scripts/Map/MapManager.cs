using System;
using System.Collections.Generic;
using UnityEngine;
using PathFinder;

public class MapManager : MonoBehaviour
{
    //nooooooooooooooooo, later.........................

    public static SimpleWorld2D persistentWorld;
    public Map map;

    void Awake()
    {
        if (map != null)
        {
            persistentWorld = new SimpleWorld2D(map.GetGridMap());
        }
        else
        {
            persistentWorld = null;
        }
    }
}
