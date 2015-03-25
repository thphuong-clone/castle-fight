using System;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;
using PathFinder;

class RandomBattleGenerator : MonoBehaviour
{
    public SwordMan swordmanPrefab;
    public Archer archerPrefab;
    public HorseMan horsemanPrefab;
    public Gladiator gladiatorPrefab;
    public Cannon cannonPrefab;

    public Barrack barrackPrefab;
    public Tower towerPrefab;

    public List<Vector2> redBuildingPosition;
    public List<Vector2> blueBuildingPosition;

    public Vector2 redAttackPosition;
    public Vector2 blueAttackPosition;

    private static Color red = new Color(1, 0, 0, 0.60784313725f);
    private static Color blue = new Color(0, 0, 1, 0.60784313725f);

    private int unitAmount;

    void Start()
    {
        SpawnRandomBuilding(GameConstant.TEAM_RED);
        SpawnRandomBuilding(GameConstant.TEAM_BLUE);
        SpawnRandomUnit(GameConstant.TEAM_RED);
        SpawnRandomUnit(GameConstant.TEAM_BLUE);
    }

    public void SpawnRandomBuilding(int side)
    {
        List<Vector2> buildingPosition;

        if (side == GameConstant.TEAM_RED)
            buildingPosition = redBuildingPosition;
        else if (side == GameConstant.TEAM_BLUE)
            buildingPosition = blueBuildingPosition;
        else
            return;

        foreach (Vector2 position in buildingPosition)
        {
            int random = UnityEngine.Random.Range(0, 100);

            if (random < 25)
            {
                SpawnBuilding(GameConstant.BARRACK, position, GameConstant.TEAM_RED);
            }
            else if (random >= 25 && random < 50)
            {
                SpawnBuilding(GameConstant.TOWER, position, GameConstant.TEAM_RED);
            }
        }
    }

    public void SpawnRandomUnit(int side)
    {
        int lowBorder = 5;
        float realLowBorder;

        if (side == GameConstant.TEAM_RED)
            realLowBorder = -8 + lowBorder;
        else if (side == GameConstant.TEAM_BLUE)
            realLowBorder = 8 - lowBorder;
        else
            return;

        for (int i = 0; i < unitAmount; i++)
        {
            float x;
            float y;

            //while (PlayerController.knownWorld.IsWalkable(GridMapUtils.GetTile(x, y)))
            //{

            //}
        }
    }

    private void SpawnBuilding(int type, Vector2 position, int side)
    {
        Building building;

        switch (type)
        {
            case GameConstant.BARRACK:
                building = Instantiate<Barrack>(barrackPrefab);
                break;
            case GameConstant.TOWER:
                building = Instantiate<Tower>(towerPrefab);
                break;
            default:
                building = new Building();
                break;
        }

        building.transform.position = position;
        if (side == GameConstant.TEAM_RED)
        {
            building.isPlayerOne = true;
            building.gameObject.GetComponent<SpriteRenderer>().color = red;
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            building.isPlayerOne = false;
            building.gameObject.GetComponent<SpriteRenderer>().color = blue;
        }
    }

    private void SpawnUnit(int type, Vector2 position, int side)
    {
        Soldier soldier;

        switch (type)
        {
            case GameConstant.SWORDMAN:
                soldier = Instantiate<SwordMan>(swordmanPrefab);
                break;
            case GameConstant.ARCHER:
                soldier = Instantiate<Archer>(archerPrefab);
                break;
            case GameConstant.KNIGHT:
                soldier = Instantiate<HorseMan>(horsemanPrefab);
                break;
            case GameConstant.HEAVY_INFANTRY:
                soldier = Instantiate<Gladiator>(gladiatorPrefab);
                break;
            case GameConstant.CANNON:
                soldier = Instantiate<Cannon>(cannonPrefab);
                break;
            default:
                soldier = new Soldier();
                break;
        }

        soldier.transform.position = position;
        soldier.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            soldier.isPlayerOne = true;
            soldier.Deploy(position.x, -7);
            soldier.GetComponent<SpriteRenderer>().color = red;
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            soldier.isPlayerOne = false;
            soldier.Deploy(position.x, 7);
            soldier.GetComponent<SpriteRenderer>().color = blue;
        }
    }
}
