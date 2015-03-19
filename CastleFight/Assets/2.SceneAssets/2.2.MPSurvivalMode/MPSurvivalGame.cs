using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtil;

public class MPSurvivalGame : MonoBehaviour
{
    private float[] COLUMN = { -1, 1, 3, -3 ,
                               -0.4f, 1.4f, 3.4f, -2.4f,
                               0f, 1.8f, 3.8f, -2.8f
                               -1, 1, 3, -3 ,
                             -1, 1, 3, -3};
    //private float[] COLUMN = {-2.8f, -0.8f, 1.2f, 3.2f };

    private int turn;
    private int waveRepeat = 5;
    private float waveDuration = 20;
    private int p1Mob;
    private int p2Mob;

    private int p1Strength;
    private int p2Strength;

    private static Color neutral = new Color(0, 0, 0, 0.60784313725f);
    public int mobStrength = 152;

    public SwordMan swordmanPrefab;
    public Archer archerPrefab;
    public HorseMan horseManPrefab;
    public Gladiator gladiatorPrefab;
    public Cannon cannonPrefab;

    void Update()
    {
        
    }

    void Awake()
    {

        //StartCoroutine(SpawnWave(ChooseUnit(40), GameConstant.TEAM_RED));
        //StartCoroutine(SpawnWave(ChooseUnit(104), GameConstant.TEAM_BLUE));
        //StartCoroutine(StartWave());
    }

    void Start()
    {
        StartCoroutine(StartWave());
    }

    void SendGold(int amount, int side)
    {
        if (side == GameConstant.TEAM_BLUE)
            p2Mob += amount;
        else if (side == GameConstant.TEAM_RED)
            p1Mob += amount;
    }

    IEnumerator StartWave()
    {
        while (true)
        {
            CalculateMobStrength();

            //Debug.Log("turn: " + turn + "; bottom: " + p1Mob + "; top: " + p2Mob);
            //Debug.Log("modifier: " + (int)(turn / 1) * 50);
            //Debug.Log("bottom: " + p1Mob);
            //Debug.Log("top: " + p2Mob);

            yield return new WaitForSeconds(waveDuration);

            StartCoroutine(SpawnWave(ChooseUnit(p1Mob), GameConstant.TEAM_RED));
            StartCoroutine(SpawnWave(ChooseUnit(p2Mob), GameConstant.TEAM_BLUE)); 
        }
    }

    int StrengthDiff()
    {
        p1Strength = PlayerController.p1_listOfSoldierLists[0].Count * 20 + PlayerController.p1_listOfSoldierLists[1].Count * 24
            + PlayerController.p1_listOfSoldierLists[2].Count * 42 + PlayerController.p1_listOfSoldierLists[3].Count * 60
                + PlayerController.p1_listOfSoldierLists[4].Count * 46 + (int)ResourceSystem.p1_gold;
        
        p2Strength = PlayerController.p2_listOfSoldierLists[0].Count * 20 + PlayerController.p2_listOfSoldierLists[1].Count * 24
            + PlayerController.p2_listOfSoldierLists[2].Count * 42 + PlayerController.p2_listOfSoldierLists[3].Count * 60
                + PlayerController.p2_listOfSoldierLists[4].Count * 46 + (int)ResourceSystem.p2_gold;

        return p1Strength - p2Strength;
    }

    void CalculateMobStrength()
    {
        int general;
        turn++;

        if (turn < 2)
            general = 40;
        else
            general = 40 + (int)(turn/waveRepeat)*50;

        int diff = StrengthDiff();

        if (diff > 0)
        {
            p1Mob = general + diff;
            p2Mob = general;
        }
        else if (diff < 0)
        {
            p1Mob = general;
            p2Mob = general + diff*-1;
        }
        else
        {
            p1Mob = general;
            p2Mob = general;
        }

        //player too weak
        if (p1Strength < 100 && general > 80)
            p1Mob = 80;
        if (p2Strength < 100 && general > 80)
            p2Mob = 80;
        if (p1Strength < 300 && general > 200)
            p1Mob = 200;
        if (p2Strength < 300 && general > 200)
            p2Mob = 200;
    }

    IEnumerator SpawnWave(Dictionary<int, int> units, int side)
    {
        float y;
        if (side == GameConstant.TEAM_BLUE)
            y = 0.5f;
        else if (side == GameConstant.TEAM_RED)
            y = -0.5f;
        else
            y = 0;


        int pos = 0;
        foreach (KeyValuePair<int, int> entry in units)
        {
            float sec = 4;
            for (int i = 0; i < entry.Value; i++)
            {
                SpawnUnit(entry.Key, side, COLUMN[pos], y);
                pos++;
            }
            if (entry.Value > 2)
            {
                sec = 6;
            }
            yield return new WaitForSeconds(0);
        }
    }

    Dictionary<int, int> ChooseUnit(int s)
    {
        Dictionary<int, int> units = new Dictionary<int, int>();

        int strength;
        if (s >= 760)
            strength = 760;
        else
            strength = s;

        int footman = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (strength >= 20)
            {
                strength -= 20;
                footman++;
            }
            else
            {
                units.Add(GameConstant.SWORDMAN, footman);
                return units;
            }
        }
        units.Add(GameConstant.SWORDMAN, footman);

        int archer = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (strength >= 24)
            {
                strength -= 24;
                archer++;
            }
            else
            {
                units.Add(GameConstant.ARCHER, archer);
                return units;
            }
        }
        units.Add(GameConstant.ARCHER, archer);

        int knight = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (strength >= 42)
            {
                strength -= 42;
                knight++;
            }
            else
            {
                units.Add(GameConstant.KNIGHT, knight);
                return units;
            }
        }
        units.Add(GameConstant.KNIGHT, knight);

        int hvInfantry = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (strength >= 60)
            {
                strength -= 60;
                hvInfantry++;
            }
            else
            {
                units.Add(GameConstant.HEAVY_INFANTRY, hvInfantry);
                return units;
            }
        }
        units.Add(GameConstant.HEAVY_INFANTRY, hvInfantry);

        int cannon = 0;
        for (int i = 0; i <= 3; i++)
        {
            if (strength >= 46)
            {
                strength -= 46;
                cannon++;
            }
            else
            {
                units.Add(GameConstant.CANNON, cannon);
                return units;
            }
        }
        units.Add(GameConstant.CANNON, cannon);


        return units;
    }

    void SpawnUnit(int type, int side, float x, float y)
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
                soldier = Instantiate<HorseMan>(horseManPrefab);
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

    void SpawnFootman(int side, float x, float y)
    {
        Soldier footman = Instantiate<SwordMan>(swordmanPrefab);

        footman.GetComponent<SpriteRenderer>().color = neutral;
        footman.transform.position = new Vector2(x, y);
        footman.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            footman.isPlayerOne = false;
            footman.Deploy(x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            footman.isPlayerOne = true;
            footman.Deploy(x, 7);
        }
    }

    void SpawnArcher(int side, float x, float y)
    {
        Soldier archer = Instantiate<Archer>(archerPrefab);

        archer.GetComponent<SpriteRenderer>().color = neutral;
        archer.transform.position = new Vector2(x, y);
        archer.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            archer.isPlayerOne = false;
            archer.Deploy(x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            archer.isPlayerOne = true;
            archer.Deploy(x, 7);
        }
    }

    void SpawnKnight(int side, float x, float y)
    {
        Soldier knight = Instantiate<HorseMan>(horseManPrefab);

        knight.GetComponent<SpriteRenderer>().color = neutral;
        knight.transform.position = new Vector2(x, y);
        knight.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            knight.isPlayerOne = false;
            knight.Deploy(x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            knight.isPlayerOne = true;
            knight.Deploy(x, 7);
        }
    }

    void SpawnHeavyInfantry(int side, float x, float y)
    {
        Soldier gladiatior = Instantiate<Gladiator>(gladiatorPrefab);

        gladiatior.GetComponent<SpriteRenderer>().color = neutral;
        gladiatior.transform.position = new Vector2(x, y);
        gladiatior.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            gladiatior.isPlayerOne = false;
            gladiatior.Deploy(x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            gladiatior.isPlayerOne = true;
            gladiatior.Deploy(x, 7);
        }
    }

    void SpawnCannon(int side, float x, float y)
    {
        Soldier cannon = Instantiate<Cannon>(cannonPrefab);

        cannon.GetComponent<SpriteRenderer>().color = neutral;
        cannon.transform.position = new Vector2(x, y);
        cannon.soldierState = GameConstant.STATE_ATTACK_MOVE;
        if (side == GameConstant.TEAM_RED)
        {
            cannon.isPlayerOne = false;
            cannon.Deploy(x, -7);
        }
        else if (side == GameConstant.TEAM_BLUE)
        {
            cannon.isPlayerOne = true;
            cannon.Deploy(x, 7);
        }
    }
}
