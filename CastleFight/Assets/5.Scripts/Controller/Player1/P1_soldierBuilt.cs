/*
 * This class handles the player 1 soldier buying unit
 * The soldiers can be built normaly at a slow speed at local barrack,
 *  or be bought and build at a very high speed in the castle
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathFinder;


public class P1_soldierBuilt : MonoBehaviour {
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
			if (ResourceSystem.p1_gold < costOfSwordMan){}
			else{
				ResourceSystem.p1_gold -= costOfSwordMan;
				StartCoroutine(spawnSwordman());
			}
			break;
		case 2:
			if (ResourceSystem.p1_gold <costOfArcher){}
			else{
				ResourceSystem.p1_gold -= costOfSwordMan;
				StartCoroutine(spawnArcher());
			}
			break;
		case 3:
			if (ResourceSystem.p1_gold < costOfHorseMan){}
			else{
				ResourceSystem.p1_gold -= costOfHorseMan;
				StartCoroutine(spawnHorseMan());
			}
			break;
		default:
			Debug.Log("Error spawning unit.");
			break;
		}
	}

	IEnumerator spawnSwordman(){
		Soldier newSoldier = (SwordMan)Instantiate (swordmanPrefab);
		newSoldier.isPlayerOne = true;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (1,0,0,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),-6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p1_listOfSoldierLists[0].Add(newSoldier);		
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,0);
	}

	IEnumerator spawnArcher(){
		Soldier newSoldier = (Archer)Instantiate (archerPrefab);
		newSoldier.isPlayerOne = true;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (1,0,0,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),-6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p1_listOfSoldierLists[1].Add(newSoldier);
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,1);
	}

	IEnumerator spawnHorseMan(){
		Soldier newSoldier = (HorseMan)Instantiate (horsemanPrefab);
		newSoldier.isPlayerOne = true;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (1,0,0,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),-6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p1_listOfSoldierLists[2].Add(newSoldier);
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,2);
	}


	//This function order the newly created unit to a place with a order.
	void orderUnit(ref Soldier _sol,bool _isPlayerOne,int _unitType){
		//yield return new WaitForSeconds (0.1f);
		if (_isPlayerOne) {
			_sol.soldierState = (int)PlayerController.p1_soldierOrder[_unitType].z;
			if (PlayerController.p1_soldierOrder[_unitType].z == 0){
				_sol.destinatedPos = new Vector2(_sol.transform.position.x,_sol.transform.position.y);
			}
			else{				
				Position2D p1_end = GridMapUtils.GetTile(PlayerController.p1_soldierOrder[_unitType].x,PlayerController.p1_soldierOrder[_unitType].y);
				Position2D start = GridMapUtils.GetTile(_sol.transform.position.x, _sol.transform.position.y);
				_sol.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, p1_end);
				_sol.EndCurrentMove();
			}
		}
		else{
			_sol.soldierState = (int)PlayerController.p2_soldierOrder[_unitType].z;
			if (PlayerController.p2_soldierOrder[_unitType].z == 0){
				_sol.destinatedPos = new Vector2(_sol.transform.position.x,_sol.transform.position.y);
			}
			else{				
				Position2D p2_end = GridMapUtils.GetTile(PlayerController.p2_soldierOrder[_unitType].x,PlayerController.p2_soldierOrder[_unitType].y);
				Position2D start = GridMapUtils.GetTile(_sol.transform.position.x, _sol.transform.position.y);
				_sol.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, p2_end);
				_sol.EndCurrentMove();
			}
		}
	}

}
