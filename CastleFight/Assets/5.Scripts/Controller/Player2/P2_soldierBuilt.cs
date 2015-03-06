/*
 * This class handles the player 2 soldier buying unit
 * The soldiers can be built normaly at a slow speed at local barrack,
 *  or be bought and build at a very high speed in the castle
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class P2_soldierBuilt : MonoBehaviour {
	[SerializeField]int costOfSwordMan;
	[SerializeField]int costOfArcher;
	[SerializeField]int costOfHorseMan;
	
	
	public Button swordMan;
	public Button archer;
	public Button horseMan;
	
	public Text swordManCost;
	public Text archerCost;
	public Text horseManCost;
	
	public SwordMan swordmanPrefab;
	public Archer archerPrefab;
	public HorseMan horsemanPrefab;
	
	void Awake(){
		swordManCost.text = costOfSwordMan.ToString ();
		archerCost.text = costOfArcher.ToString ();
		horseManCost.text = costOfHorseMan.ToString ();
	}
	
	public void soldierBought(int s){
		switch (s) {
		case 1:
			if (ResourceSystem.p2_gold < costOfSwordMan){}
			else{
				ResourceSystem.p2_gold -= costOfSwordMan;
				spawnSwordman();
			}
			break;
		case 2:
			if (ResourceSystem.p2_gold <costOfArcher){}
			else{
				ResourceSystem.p2_gold -= costOfSwordMan;
				spawnArcher();
			}
			break;
		case 3:
			if (ResourceSystem.p2_gold < costOfHorseMan){}
			else{
				ResourceSystem.p2_gold -= costOfHorseMan;
				spawnHorseMan();
			}
			break;
		default:
			break;
		}
	}
	
	void spawnSwordman(){
		SwordMan newSoldier = (SwordMan)Instantiate (swordmanPrefab);
		newSoldier.isPlayerOne = false;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (0,0,1,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p2_listOfSoldierLists[0].Add(newSoldier);
		PlayerController.orderSoldier ();
	}
	
	void spawnArcher(){
		Archer newSoldier = (Archer)Instantiate (archerPrefab);
		newSoldier.isPlayerOne = false;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (0,0,1,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p2_listOfSoldierLists[1].Add(newSoldier);
		PlayerController.orderSoldier ();
	}
	
	void spawnHorseMan(){
		HorseMan newSoldier = (HorseMan)Instantiate (horsemanPrefab);
		newSoldier.isPlayerOne = false;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (0,0,1,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p2_listOfSoldierLists[2].Add(newSoldier);
		PlayerController.orderSoldier ();
	}
	
}
