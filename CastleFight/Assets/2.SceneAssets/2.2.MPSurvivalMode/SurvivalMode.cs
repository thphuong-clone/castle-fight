using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

public class SurvivalMode : MonoBehaviour
{
    public SwordMan swordmanPrefab;
    public Archer archerPrefab;
    public HorseMan horsemanPrefab;
    public Gladiator gladiatorPrefab;
    public Cannon cannonPrefab;
    public MPSurvivalMode.UI p1UI;
    public MPSurvivalMode.UI p2UI;

    private float[] COLUMN = { -1, 1, 2, -2 };

    public int wave;
    public int waveStrength = 200;
    public int wavePrepareDuration = 20;

    public bool waveInProgress;

    public static int redTeamRemaining;
    public static int blueTeamRemaining;

    private Queue<int> redTeamUnits;
    private Queue<int> blueTeamUnits;

    private static Color neutral = new Color(0, 0, 0, 0.60784313725f);
    private static Color red = new Color(1, 0, 0, 0.60784313725f);
    private static Color blue = new Color(0, 0, 1, 0.60784313725f);

    void Update()
    {
        if (redTeamRemaining == 0 && blueTeamRemaining == 0)
            waveInProgress = false;
    }

    void Awake()
    {
        ResourceSystem.p1_gold = 200;
        ResourceSystem.p2_gold = 200;


        redTeamRemaining = 0;
        blueTeamRemaining = 0;

        redTeamUnits = new Queue<int>();
        blueTeamUnits = new Queue<int>();
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        while (true)
        {
            wave++;
            p1UI.gameObject.SetActive(true);
            p2UI.gameObject.SetActive(true);

            Dictionary<int, int> units = ChooseUnit(waveStrength);

            //yield return new WaitForSeconds(wavePrepareDuration + 1);

            p1UI.Gold = (int)ResourceSystem.p1_gold;
            p2UI.Gold = (int)ResourceSystem.p2_gold;
            p1UI.ShowWaveAnnouncement(units);
            p2UI.ShowWaveAnnouncement(units);

            //hide all ui

            yield return new WaitForSeconds(wavePrepareDuration + 2);

            foreach (int unit in p1UI.attackForce.buyUnitList)
            {
                blueTeamUnits.Enqueue(unit);
            }
            blueTeamRemaining += p1UI.attackForce.buyUnitList.Count;
            foreach(int unit in p2UI.attackForce.buyUnitList)
            {
                redTeamUnits.Enqueue(unit);
            }
            redTeamRemaining += p2UI.attackForce.buyUnitList.Count;

            //start wave
            SpawnDefenseUnit(GameConstant.TEAM_RED);
            SpawnDefenseUnit(GameConstant.TEAM_BLUE);
            StartCoroutine(SpawnWave(redTeamUnits, GameConstant.TEAM_RED));
            StartCoroutine(SpawnWave(blueTeamUnits, GameConstant.TEAM_BLUE));

            waveInProgress = true;

            //wait for both players to finish wave
            while (waveInProgress)
            {
                yield return null;
            }

            ResourceSystem.p1_gold += waveStrength / 2;
            ResourceSystem.p2_gold += waveStrength / 2;

            waveStrength += wave * 50;

        }
    }

    private IEnumerator SpawnWave(Queue<int> units, int side)
    {
        float y;
        if (side == GameConstant.TEAM_BLUE)
            y = 0.5f;
        else if (side == GameConstant.TEAM_RED)
            y = -0.5f;
        else
            y = 0;

        while (units.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (units.Count == 0)
                    break;
                SpawnUnit(units.Dequeue(), side, COLUMN[i], y);
            }
            yield return new WaitForSeconds(10);
        }

    }

    private void SpawnDefenseUnit(int side)
    {
        if (side == GameConstant.TEAM_RED)
        {
            foreach (int unit in p1UI.defenseForce.buyUnitList)
            {
                SpawnUnit(unit, side, true, 0, -3);
            }
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            foreach (int unit in p2UI.defenseForce.buyUnitList)
            {
                SpawnUnit(unit, side, true, 0, -3);
            }
        }
    }

    private Dictionary<int, int> ChooseUnit(int strength)
    {
        Dictionary<int, int> wave = new Dictionary<int, int>();
        int remaining = strength;

        while (remaining > 0)
        {
            int unit = UnityEngine.Random.Range(0, 5);
            switch (unit)
            {
                case GameConstant.SWORDMAN:
                    remaining -= GameConstant.PRICE_SWORDMAN;
                    break;
                case GameConstant.ARCHER:
                    remaining -= GameConstant.PRICE_ARCHER;
                    break;
                case GameConstant.KNIGHT:
                    remaining -= GameConstant.PRICE_HORSEMAN;
                    break;
                case GameConstant.HEAVY_INFANTRY:
                    remaining -= GameConstant.PRICE_HEAVY_INFANTRY;
                    break;
                case GameConstant.CANNON:
                    remaining -= GameConstant.PRICE_CANNON;
                    break;
                default:
                    remaining -= GameConstant.PRICE_SWORDMAN;
                    break;
            }

            redTeamRemaining++;
            blueTeamRemaining++;
            redTeamUnits.Enqueue(unit);
            blueTeamUnits.Enqueue(unit);

            if (wave.ContainsKey(unit))
            {
                wave[unit] += 1;
            }
            else
            {
                wave.Add(unit, 1);
            }

        }

        return wave;
    }

    private void SpawnUnit(int type, int side, Vector2 position)
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

        soldier.isHuman = false;
        soldier.GetComponent<SpriteRenderer>().color = neutral;
        soldier.transform.position = position;
        soldier.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            soldier.isPlayerOne = false;
            soldier.Deploy(position.x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            soldier.isPlayerOne = true;
            soldier.Deploy(position.x, 7);
        }
    }

    private void SpawnUnit(int type, int side, float x, float y)
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

        soldier.isHuman = false;
        soldier.GetComponent<SpriteRenderer>().color = neutral;
        soldier.transform.position = new Vector2(x, y);
        soldier.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            soldier.isPlayerOne = false;
            soldier.Deploy(x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            soldier.isPlayerOne = true;
            soldier.Deploy(x, 7);
        }
    }

    private void SpawnUnit(int type, int side, bool isHuman, float x, float y)
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

        soldier.isHuman = isHuman;
        //soldier.GetComponent<SpriteRenderer>().color = neutral;
        soldier.transform.position = new Vector2(x, y);
        soldier.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            if (!isHuman)
            {
                soldier.isPlayerOne = false;
                soldier.GetComponent<SpriteRenderer>().color = neutral;
                soldier.Deploy(x, -7);
            }
            else
            {
                soldier.isPlayerOne = true;
                soldier.GetComponent<SpriteRenderer>().color = red;
            }
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            if (isHuman)
            {
                soldier.isPlayerOne = true;
                soldier.GetComponent<SpriteRenderer>().color = neutral;
                soldier.Deploy(x, 7);
            }
            else
            {
                soldier.isPlayerOne = false;
                soldier.GetComponent<SpriteRenderer>().color = blue;
            }
        }
    }
}
