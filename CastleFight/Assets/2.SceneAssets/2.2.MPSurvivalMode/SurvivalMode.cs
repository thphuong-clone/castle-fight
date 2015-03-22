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
    public MPSurvivalMode.UI ui;

    private float[] COLUMN = { -1, 1, 3, -3 ,
                               -0.4f, 1.4f, 3.4f, -2.4f,
                               0f, 1.8f, 3.8f, -2.8f
                               -1, 1, 3, -3 ,
                             -1, 1, 3, -3};

    private int wave;
    public int waveStrength = 200;
    public int wavePrepareDuration = 20;

    public bool waveInProgress;

    public static int redTeamRemaining;
    public static int blueTeamRemaining;

    private Queue<int> redTeamUnits;
    private Queue<int> blueTeamUnits;

    private static Color neutral = new Color(0, 0, 0, 0.60784313725f);

    void Update()
    {
        if (redTeamRemaining == 0 && blueTeamRemaining == 0)
            waveInProgress = false;
    }

    void Awake()
    {
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

            Dictionary<int, int> units = ChooseUnit(waveStrength);

            //ui display units on this wave
            ui.ShowWaveAnnouncement(units);
            //foreach (KeyValuePair<int, int> k in units)
            //{
            //    Debug.Log(k.Key + ": " + k.Value);
            //}

            yield return new WaitForSeconds(wavePrepareDuration);

            //hide all ui

            //start wave
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

    private void SendUnit(int unit, int side)
    {
        if (side == GameConstant.TEAM_BLUE)
        {
            blueTeamUnits.Enqueue(unit);
            blueTeamRemaining++;
        }
        else if (side == GameConstant.TEAM_RED)
        {
            redTeamUnits.Enqueue(unit);
            redTeamRemaining++;
        }
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
}
