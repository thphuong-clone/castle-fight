/*
This class order unit to go, attack and stuffs.
*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using PathFinder;

public class PlayerController : MonoBehaviour {
    public static SimpleWorld2D knownWorld = new SimpleWorld2D(18, 32);

//The attack order for player one - the z dimension of the vector is the order.
//0 = stay idle at current position, 1 is move, 2 is attack
	public static List<Vector3> p1_soldierOrder = new List<Vector3>();

	public static List<Vector3> p2_soldierOrder = new List<Vector3>();


	//this list contains all the caravan
	public static List<Caravan> caravanList = new List<Caravan> ();
//This lsit contain all the list of soldier. List 0 = swordman, 1 = archer ....
	public static List<List<Soldier>> p1_listOfSoldierLists = new List<List<Soldier>>(); 
	public static List<List<Soldier>> p2_listOfSoldierLists = new List<List<Soldier>>();

//The list of all buildings.
	public static List<List<Building>> p1_buildingList = new List <List<Building>>();
	public static List<List<Building>> p2_buildingList = new List <List<Building>>();
	//castle = 0 ; barrack= 1, tower = 2 , wall = 3

//awake void. add all the soldier to all the list
	void Awake(){
		//clear all of the list, since they are static, they are not cleared by the end of the scene
		p1_listOfSoldierLists.Clear ();
		p2_listOfSoldierLists.Clear ();
		p1_buildingList.Clear ();
		p2_buildingList.Clear ();
//		ResourceSystem.p1_gold = 100;
//		ResourceSystem.p2_gold = 100;
		//There are 5 types of unit so there will be 5 lists of soldier
		for (int i = 0; i < 5; i ++) {
			p1_listOfSoldierLists.Add(new List<Soldier>());
			p2_listOfSoldierLists.Add(new List<Soldier>());
		}

		//there are four types of building icluding wall so we have 4 lists
		for (int i = 0; i < 4; i ++) {
			p1_buildingList.Add(new List<Building>());
			p2_buildingList.Add(new List<Building>());
		}

		updateSoldierList ();
		updateBuildingList ();

		p1_soldierOrder.Clear ();
		p2_soldierOrder.Clear ();
		for (int i = 0; i < 5; i ++) {
			p1_soldierOrder.Add (Vector3.zero);
			p2_soldierOrder.Add (Vector3.zero);
		}

        knownWorld = GridMapUtils.MakeWorld(MapManager.persistentWorld);
	}


	//this function order every unit to do its destinated task
	//Only use this function for the multiplayer mode only.
	//In single player mode, we have the ability to single control one unit, so it will be really fucked up.
	public static void orderSoldier(){

        //knownWorld.SetPosition(new Position2D(0, 10), true);
        //knownWorld.SetPosition(new Position2D(1, 10), true);
        //knownWorld.SetPosition(new Position2D(2, 10), true);
        //knownWorld.SetPosition(new Position2D(3, 10), true);
        //knownWorld.SetPosition(new Position2D(4, 10), true);
        //knownWorld.SetPosition(new Position2D(5, 10), true);
        //knownWorld.SetPosition(new Position2D(6, 10), true);

		for (int i = 0; i < 5; i ++) {
            Position2D p1End = GridMapUtils.GetTile(p1_soldierOrder[i].x, p1_soldierOrder[i].y);
			//for the player one
			foreach(Soldier s in p1_listOfSoldierLists[i]){
				s.soldierState = (int)p1_soldierOrder[i].z;
                if (p1_soldierOrder[i].z == 0)
                {
                    s.destinatedPos = new Vector2(s.transform.position.x, s.transform.position.y);
                }
                else
                {
                    s.destinatedPos = new Vector2(p1_soldierOrder[i].x, p1_soldierOrder[i].y);
                    Position2D start = GridMapUtils.GetTile(s.transform.position.x, s.transform.position.y);
                    //Debug.Log("from: " + start + " to: " + p1End);
                    s.nextPathNode = PathFinder.PathFinder.FindPath(knownWorld, start, p1End);
                    s.EndCurrentMove();
                }
			}
            Position2D p2End = GridMapUtils.GetTile(p2_soldierOrder[i].x, p2_soldierOrder[i].y);
			//for the player 2
			foreach(Soldier s in p2_listOfSoldierLists[i]){
				s.soldierState = (int)p2_soldierOrder[i].z;
                if (p2_soldierOrder[i].z == 0)
                    s.destinatedPos = new Vector2(s.transform.position.x, s.transform.position.y);
                else
                {
                    s.destinatedPos = new Vector2(p2_soldierOrder[i].x, p2_soldierOrder[i].y);
                    Position2D start = GridMapUtils.GetTile(s.transform.position.x, s.transform.position.y);
                    s.nextPathNode = PathFinder.PathFinder.FindPath(knownWorld, start, p2End);
                    s.EndCurrentMove();
                }
			}

		}

	}

//This function find all the soldier on the field and add that soldier to its approtiate list.
	void updateSoldierList(){
		//add all the unit on map to the list.
		GameObject[] soldierArray = GameObject.FindGameObjectsWithTag ("Soldier");
		foreach (GameObject s in soldierArray) {
			switch(s.name){
			case "SwordMan":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[0].Add(s.gameObject.GetComponent<SwordMan>());
				else
					p2_listOfSoldierLists[0].Add(s.gameObject.GetComponent<SwordMan>());
				break;
			case "Archer":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[1].Add(s.gameObject.GetComponent<Archer>());
				else
					p2_listOfSoldierLists[1].Add(s.gameObject.GetComponent<Archer>());
				break;
			case "HorseMan":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[2].Add(s.gameObject.GetComponent<HorseMan>());
				else
					p2_listOfSoldierLists[2].Add(s.gameObject.GetComponent<HorseMan>());
				break;
			case "Gladiator":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[3].Add(s.gameObject.GetComponent<Gladiator>());
				else
					p2_listOfSoldierLists[3].Add(s.gameObject.GetComponent<Gladiator>());
				break;
			case "Cannon":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[4].Add(s.gameObject.GetComponent<Cannon>());
				else
					p2_listOfSoldierLists[4].Add(s.gameObject.GetComponent<Cannon>());
				break;
			default:
				Debug.Log("Errr,<color=red>WRONG</color>  name : " + s.gameObject.name);
				break;
			}
		}

	}

	//find all the building on the map and add its to the list. Use when awake.
	void updateBuildingList(){
		GameObject[] buildingArray = GameObject.FindGameObjectsWithTag ("Building");
		foreach (GameObject b in buildingArray) {
			//castle = 0, barrack = 1; tower = 2, wall = 3
			switch(b.name){
			case "Castle":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[0].Add(b.gameObject.GetComponent<MainCastle>());
				else
					p2_buildingList[0].Add(b.gameObject.GetComponent<MainCastle>());
				break;
			case "Barrack":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[1].Add(b.gameObject.GetComponent<Barrack>());
				else
					p2_buildingList[1].Add(b.gameObject.GetComponent<Barrack>());
				break;
			case "WatchTower":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[2].Add(b.gameObject.GetComponent<Tower>());
				else
					p2_buildingList[2].Add(b.gameObject.GetComponent<Tower>());
				break;
			case "Wall":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[3].Add(b.gameObject.GetComponent<Wall>());
				else
					p2_buildingList[3].Add(b.gameObject.GetComponent<Wall>());
				break;
			default:
				Debug.Log("ERRRR ?????");
				break;
			}
		}

	}

	public static void reupdateSoldier(){
		try{
			foreach( List<Soldier> ls in p1_listOfSoldierLists){
				foreach(Soldier s in ls){
					s.EndCurrentMove();
					Position2D start = GridMapUtils.GetTile(s.transform.position.x,s.transform.position.y);
					Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);
					s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
				}
			}
			foreach( List<Soldier> ls in p2_listOfSoldierLists){
				foreach(Soldier s in ls){
					s.EndCurrentMove();
					Position2D start = GridMapUtils.GetTile(s.transform.position.x,s.transform.position.y);
					Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);
					s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
				}
			}
		}
		catch{
			//Debug.LogWarning("Errr ?");
			return;		
		}
	}



}

