using UnityEngine;
using System;
using PathFinder;

namespace GameUtil
{
    class GameConstant
    {
        //unit
        public const int SWORDMAN = 0;
        public const int ARCHER = 1;
        public const int KNIGHT = 2;
        public const int HEAVY_INFANTRY = 3;
        public const int CANNON = 4;
        //building
        public static int BARRACK = 0;
        public static int WALL = 1;
        public static int TOWER = 2;
        //attack
        public static int ATTACK_TYPE_SWORDMAN = 1;
        public static int ATTACK_TYPE_ARCHER = 2;
        public static int ATTACK_TYPE_KNIGHT = 3;
        public static int ATTACK_TYPE_HEAVY_INFANTRY = 4;
        public static int ATTACK_TYPE_CANNON = 5;
        public static int ATTACK_TYPE_TOWER = 6;
        //armor
        public static int ARMOR_SWORDMAN = 1;
        public static int ARMOR_ARCHER = 2;
        public static int ARMOR_KNIGHT = 3;
        public static int ARMOR_HEAVY_INFANTRY = 4;
        public static int ARMOR_CANNON = 5;
        public static int ARMOR_TOWER = 6;
        //unit state
        public static int STATE_IDLE = 0;
        public static int STATE_MOVE_ONLY = 1;
        public static int STATE_ATTACK_MOVE = 2;
        public static int STATE_UNAVAILABLE = -1;
        //unit price
        public static int PRICE_SWORDMAN = 20;
        public static int PRICE_ARCHER = 24;
        public static int PRICE_HORSEMAN = 42;
        public static int PRICE_HEAVY_INFANTRY = 60;
        public static int PRICE_CANNON = 46;

        //team
        public static int TEAM_RED = 1;
        public static int TEAM_BLUE = 2;

        //building grid
        public static Position2D[] GRID_ONE = { new Position2D(0, 0) };

        public static Position2D[] GRID_FOUR = { new Position2D(-1, -1), new Position2D(0, -1), 
                                                 new Position2D(-1, 0), new Position2D(0, 0) };

        public static Position2D[] GRID_TWO = { new Position2D(-1, 0), new Position2D(0, 0) };

        public static Position2D[] GRID_SIX = { new Position2D(-1, -2), new Position2D(0, -2),
                                                new Position2D(-1, -1), new Position2D(0, -1), 
                                                new Position2D(-1, 0), new Position2D(0, 0)};
    }
}
