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

            if (random < 60)
            {
                SpawnBuilding(GameConstant.BARRACK, position, side);
            }
            else if (random >= 60 && random < 70)
            {
                SpawnBuilding(GameConstant.TOWER, position, side);
            }
        }
    }

    public void SpawnRandomUnit(int side)
    {
        int unit = UnityEngine.Random.Range(0, 4);

        if (side == GameConstant.TEAM_RED)
        {
            SpawnUnit(unit, new Vector2(0.5f, -0.4f), side);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            SpawnUnit(unit, new Vector2(-0.5f, 0.5f), side);
        }
    }

    private void SpawnBuilding(int type, Vector2 position, int side)
    {
        Building building;

        switch (type)
        {
            case GameConstant.BARRACK:
				building = Instantiate<Barrack>(barrackPrefab);
				building.transform.name = "Barrack";
                break;
            case GameConstant.TOWER:
				building = Instantiate<Tower>(towerPrefab);
				building.transform.name = "WatchTower";
                break;
            default:
                building = new Building();
                break;
        }

        building.transform.position = position;
        if (side == GameConstant.TEAM_RED)
        {
            building.isPlayerOne = true;
            building.unitAura.GetComponent<SpriteRenderer>().color = red;
            int healthPercent = UnityEngine.Random.Range(64, 200);
            if (healthPercent > 100)
                healthPercent = 100;
            building.health = healthPercent * building.maxHealth / 100;
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            building.isPlayerOne = false;
            building.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            int healthPercent = UnityEngine.Random.Range(27, 200);
            if (healthPercent > 100)
                healthPercent = 100;
            building.health = healthPercent * building.maxHealth / 100;
            building.unitAura.GetComponent<SpriteRenderer>().color = blue;
        }
    }

    private void SpawnUnit(int type, Vector2 position, int side)
    {
        Soldier soldier;

        switch (type)
        {
            case GameConstant.SWORDMAN:
                soldier = Instantiate<SwordMan>(swordmanPrefab);
				soldier.transform.name = "SwordMan";
				break;
            case GameConstant.ARCHER:
				soldier = Instantiate<Archer>(archerPrefab);
				soldier.transform.name = "Archer";
                break;
            case GameConstant.KNIGHT:
				soldier = Instantiate<HorseMan>(horsemanPrefab);
				soldier.transform.name = "HorseMan";
                break;
            case GameConstant.HEAVY_INFANTRY:
				soldier = Instantiate<Gladiator>(gladiatorPrefab);
				soldier.transform.name = "Gladiator";
                break;
            case GameConstant.CANNON:
				soldier = Instantiate<Cannon>(cannonPrefab);
				soldier.transform.name = "Cannon";
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
