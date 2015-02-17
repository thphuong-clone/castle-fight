using System.Collections.Generic;
using UnityEngine;
using System.Collections;


//this is for player one.
public class DropUnit : MonoBehaviour {
//	public List<Soldier> soldierList = new List<Soldier> ();
//
//	public SwordMan swordManPrefab;
//	public Archer archerPrefab;
//	public Medic medicPrefab;
//	public HorseMan horsemanPrefab;
//	public Gladiator gladiatorPrefab;
//	public Cannon cannontPrefab;
//
//	public Wall wallPrefab;
//	public Tower towerPrefab;
//
//	public GameObject board;
//
//	public void dropUnit (int x){
//		//the x/y position to drop the unit.
//		int xPos;
//		int yPos;
//		xPos = (int)(x % 10);
//		yPos = (int)(x / 10);
//
//		switch (MenuPlayerOne.unitToSpawn){
//		case "swordMan":
//			spawnSwordMan(xPos,yPos);
//			break;
//		case "archer":
//			spawnArcher(xPos,yPos);
//			break;
////		case "medic":
////			spawnMedic(xPos,yPos);
////			break;
//		case "horseMan":
//			spawnHorseMan(xPos,yPos);
//			break;
//		case "defender":
//			spawnDefender(xPos,yPos);
//			break;
//		case "catapult":
//			spawnCatapult(xPos,yPos);
//			break;
//		case "wall":
//			spawnWall(xPos,yPos);
//			break;
//		case "tower":
//			spawnTower(xPos,yPos);
//			break;
//		default:
//			Debug.LogError("Some thing is definetly wrong.");
//			break;
//		}
//
//		board.gameObject.SetActive (false);
//	}
//
//	public void spawnSwordMan(int a,int b){
//		SwordMan soldier = (SwordMan)Instantiate (swordManPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		soldier.transform.position = new Vector3 (posX,posY,0);
//		soldierList.Add (soldier);
//		soldier.isPlayerOne = true;
//	}
//
//	public void spawnArcher(int a,int b){
//		Archer soldier = (Archer)Instantiate (archerPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		soldier.transform.position = new Vector3 (posX,posY,0);
//		soldierList.Add (soldier);
//		soldier.isPlayerOne = true;
//	}
//
////	public void spawnMedic(int a,int b){
////		Medic soldier = (Medic)Instantiate (medicPrefab);
////		int posX = -5 + a;
////		int posY = -8 + b;
////		soldier.transform.position = new Vector3 (posX,posY,0);
////		soldierList.Add (soldier);
////		soldier.isPlayerOne = true;
////	}
//
//	public void spawnHorseMan(int a,int b){
//		HorseMan soldier = (HorseMan)Instantiate (horsemanPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		soldier.transform.position = new Vector3 (posX,posY,0);
//		soldierList.Add (soldier);
//		soldier.isPlayerOne = true;
//	}
//
//	public void spawnDefender(int a,int b){
//		Gladiator soldier = (Gladiator)Instantiate (gladiatorPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		soldier.transform.position = new Vector3 (posX,posY,0);
//		soldierList.Add (soldier);
//		soldier.isPlayerOne = true;
//	}
//
//	public void spawnCatapult(int a,int b){
//		Cannon soldier = (Cannon)Instantiate (cannontPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		soldier.transform.position = new Vector3 (posX,posY,0);
//		soldierList.Add (soldier);
//		soldier.isPlayerOne = true;
//	}
//
//	public void spawnWall(int a,int b){
//		Wall buidling = (Wall)Instantiate (wallPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		buidling.transform.position = new Vector3 (posX,posY,0);
//		buidling.isPlayerOne = true;
//
//	}
//
//	public void spawnTower(int a,int b){
//		Tower buidling = (Tower)Instantiate (towerPrefab);
//		int posX = -5 + a;
//		int posY = -8 + b;
//		buidling.transform.position = new Vector3 (posX,posY,0);
//		buidling.isPlayerOne = true;
//	}

}
