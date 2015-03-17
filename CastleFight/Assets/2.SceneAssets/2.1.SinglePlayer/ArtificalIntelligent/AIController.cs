/*
 * This class handle the Artifical intelligent of the game for the single player part.
 */ 
using UnityEngine;
using System.Collections;
using PathFinder;
using System.Collections.Generic;

public class AIController : MonoBehaviour {
	//the cost of unit
	[SerializeField]List<float> unitCost;
	//so I can see how much money the AI have
	[SerializeField]public float AI_Gold;

	//this is the order list.
	[SerializeField]List<Vector3> orderList = new List<Vector3> (); 

	[SerializeField]List<Vector3> possibleBuildPosition;

	public SwordMan swordManPrefab;
	public Archer archerPrefab;
	public HorseMan horseManPrefab;
	public Gladiator gladiatorPrefab;
	public Cannon cannonPrefab;

	public Barrack barrackPrefab;
	public Tower towerPrefab;

	//what are those two ints, please explain Phuong
    int currentOrder;
    int nextOrder;

	void Awake(){
		possibleBuildPosition.Remove (new Vector3(0,5,0));
		orderList.Clear ();
		for (int i = 0; i < 5;i++)
			orderList.Add (Vector3.zero);
		
		attackOrder();

		AI_Gold = ResourceSystem.p2_gold;

		StartCoroutine (randomSpawnUnit ());

		StartCoroutine (updateBrain ());

		//StartCoroutine (updateOrder());
		StartCoroutine (plusGold());
	}

	void Update(){
		AI_Gold = ResourceSystem.p2_gold;
	}

	//The AI will judge the player based on his gold, and army to come up with an approtiate strategy
	void evaluatePlayer(){
	}

	//The ai will evaluate himselft
	void evaluateAI(){
	}

	//This function is called every few frame to update the order from this class to the master cotroller
	IEnumerator updateOrder(){
		yield return new WaitForSeconds (0.1f);
		while (true) {
			yield return new WaitForSeconds(0.33f);
			for (int i = 0; i < 5; i ++) {
				PlayerController.p2_soldierOrder[i] = orderList[i];
			}
		}
	}


	IEnumerator updateBrain(){
		while (true) {
			yield return new WaitForSeconds (0.5f);
			whatShouldIDo();
			generalOrder();
		}
	}

	//this function decides what the AI should do - it's important
	void whatShouldIDo(){
		//first, check if the current army power and the player army power
		//evaluate the player strength
		float playerStrength = PlayerController.p1_listOfSoldierLists [0].Count * 20 + PlayerController.p1_listOfSoldierLists [1].Count * 24
			+ PlayerController.p1_listOfSoldierLists [2].Count * 42 + PlayerController.p1_listOfSoldierLists [3].Count * 60 
				+ PlayerController.p1_listOfSoldierLists [4].Count * 46;    
		//evaluate the AI strength
		float AIStrength = PlayerController.p2_listOfSoldierLists [0].Count * 20 + PlayerController.p2_listOfSoldierLists [1].Count * 24
			+ PlayerController.p2_listOfSoldierLists [2].Count * 42 + PlayerController.p2_listOfSoldierLists [3].Count * 60 
				+ PlayerController.p2_listOfSoldierLists [4].Count * 46; 

		float strengthRatio = AIStrength / playerStrength;
		//if the AI is overwhemingly stronger, 
		if (strengthRatio >= 1.5f) {
			//currentOrder = 0;
			nextOrder = 1;
			Debug.Log("attack");
			if (currentOrder != nextOrder)				
				attackOrder();
			//issue an attack order
		}
		else{
			//currentOrder = 0;
			nextOrder = 2;
			Debug.Log("defense");
			if (currentOrder != nextOrder)
				defenseOrder();
		}
		//if not, first, attack the caravan, and heal all unit. if there is no caravan, group unit up, wait for power to come up

	}

	//this is the general order, to micro almost die unit to go back to heal and strong unit to go fighting
	void generalOrder(){
		for (int i = 0; i < 5; i ++){
			for (int j = 0; j < PlayerController.p2_listOfSoldierLists[i].Count;j ++){
				Soldier s = PlayerController.p2_listOfSoldierLists[i][j];
			//foreach(Soldier s in PlayerController.p2_listOfSoldierLists[i]){
				//if the unit is almost dead, heal it
				if (s.health <= 0.325f * s.maxHealth){
					Debug.Log("DIE");
					s.destinatedPos = new Vector2(0,5.5f);
					s.EndCurrentMove();
					Position2D start = GridMapUtils.GetTile(s.transform.position.x,s.transform.position.y);
					s.destinatedPos = new Vector2(UnityEngine.Random.Range(-1,1.1f),5.5f);
					Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);

					s.soldierState = 1;
					s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
				}
				else if (s.health >= 0.85f * s.maxHealth){
//                    if (currentOrder != nextOrder)
//					{
						//Debug.LogWarning("yo");
                        s.EndCurrentMove();
						Position2D start = GridMapUtils.GetTile(s.transform.position.x, s.transform.position.y);
						s.destinatedPos = new Vector2(orderList[i].x,orderList[i].y);
						Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);
                        s.soldierState = (int)orderList[i].z;
                        s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
//                        while (s.nextPathNode.next != null)
//                        {
//                            //Debug.Log("Route: " + GridMapUtils.GetRealPosition(s.nextPathNode.next.position));
//                            s.nextPathNode = s.nextPathNode.next;
//                            //i++;
//                        }
                        currentOrder = nextOrder;
//                    }
				}
				else if (s.health > 0.325f * s.maxHealth && s.health < 0.85f*s.maxHealth){
					//if he is healing
					if (s.transform.position.y > 5.4f){					
						s.EndCurrentMove();
						Position2D start = GridMapUtils.GetTile(s.transform.position.x,s.transform.position.y);
						s.destinatedPos = new Vector2(UnityEngine.Random.Range(-1,1.1f),5.5f);
						Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);
						s.soldierState = 1;
						s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
					}
					else{
						if (s.health <= s.maxHealth * 0.5f){
							s.EndCurrentMove();
							Position2D start = GridMapUtils.GetTile(s.transform.position.x,s.transform.position.y);
							s.destinatedPos = new Vector2(UnityEngine.Random.Range(-1,1.1f),5.5f);
							Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);
							s.soldierState = 2;
							s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);						
						}
						else{
							s.EndCurrentMove();
							Position2D start = GridMapUtils.GetTile(s.transform.position.x,s.transform.position.y);
							s.destinatedPos = new Vector2(orderList[i].x,orderList[i].y);
							Position2D end = GridMapUtils.GetTile(s.destinatedPos.x,s.destinatedPos.y);
							s.soldierState = (int)orderList[i].z;
							s.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
						}
					}
				}
			}
		}
	}

	//order the unit to go left, right up down, err, anywhere you like
	void attackOrder(){		
		for (int i = 0; i < 5; i ++) {
			orderList[i] = new Vector3(UnityEngine.Random.Range(-1.5f,1.6f),-7,2);
			PlayerController.p2_soldierOrder[i] = orderList[i];
		}
	}

	void defenseOrder(){		
		for (int i = 0; i < 5; i ++) {
			orderList[i] = new Vector3(UnityEngine.Random.Range(-1.5f,1.6f),1,2);
			PlayerController.p2_soldierOrder[i] = orderList[i];
		}
	}

	void attackCaravan(){
		if (PlayerController.caravanList.Count == 0) {
			return;		
		}
		//if ()
	}

	IEnumerator randomSpawnUnit(){
		ResourceSystem.p2_gold = 50;
		while(true){
			yield return new WaitForSeconds(0.5f);
			if (ResourceSystem.p2_gold >= 0){
				if (UnityEngine.Random.Range(0,100) > chanceToBuildBarrack()){//25% chance to build a house
					//will later improve this, based on current number of unit and barrack
					buildBarrack();
					ResourceSystem.p2_gold -= unitCost[3];
				}
				else{
					switch(UnityEngine.Random.Range(0,3)){
					case 0://spawn swordman
						StartCoroutine(buildSwordman());
						ResourceSystem.p2_gold -= unitCost[0];
						break;
					case 1://spawn archer
						StartCoroutine(buildArcher());
						ResourceSystem.p2_gold -= unitCost[1];
						break;
					case 2://spawn horseman
						StartCoroutine(buildHorseman());
						ResourceSystem.p2_gold -= unitCost[2];
						break;
					default:
						Debug.Log("hmm ?");
						break;
					}
				}
			}
		}
	}



	IEnumerator buildSwordman(){
		Soldier newSoldier = (SwordMan)Instantiate (swordManPrefab);
		newSoldier.isPlayerOne = false;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (0,0,1,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p2_listOfSoldierLists[0].Add(newSoldier);
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,0);
	}

	IEnumerator buildArcher(){
		Soldier newSoldier = (Archer)Instantiate (archerPrefab);
		newSoldier.isPlayerOne = false;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (0,0,1,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p2_listOfSoldierLists[1].Add(newSoldier);
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,1);
	}

	IEnumerator buildHorseman(){
		Soldier newSoldier = (HorseMan)Instantiate (horseManPrefab);
		newSoldier.isPlayerOne = false;
		newSoldier.GetComponent<SpriteRenderer> ().color = new Color (0,0,1,0.60784313725f);
		newSoldier.transform.position = new Vector3 (UnityEngine.Random.Range (-1f,1f),6f,0);
		newSoldier.soldierState = 2;
		PlayerController.p2_listOfSoldierLists[2].Add(newSoldier);
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,2);
	}

	void buildBarrack(){
		Barrack build = (Barrack)Instantiate (barrackPrefab);
		build.isPlayerOne = false;
		build.transform.rotation = Quaternion.Euler (new Vector3(0,0,180));
		build.unitAura.GetComponent<SpriteRenderer> ().color = new Color(0,0,1,0.58823529411f);
		PlayerController.p2_buildingList [1].Add (build);
		build.currentSoldierBuilt = UnityEngine.Random.Range(1,6);
		Vector3 buildPosition = possibleBuildPosition [UnityEngine.Random.Range (0,possibleBuildPosition.Count)];		
		build.transform.position = buildPosition;
		possibleBuildPosition.Remove (buildPosition);
		try{
			possibleBuildPosition.Remove (buildPosition + new Vector3(0,-1,0));
		}
		catch{
			//do nothing
		}
		try{
			possibleBuildPosition.Remove (buildPosition + new Vector3(0,1,0));
		}
		catch{
			//do nothing	
		}

		PlayerController.knownWorld = PathFinder.GridMapUtils.MakeWorld ();	
	}

	//this function calculate should the AI build a barrack or not.
	float chanceToBuildBarrack(){
		//calculate the net worth of buildings
		float buildingNetWorth = 100 + PlayerController.p2_buildingList [1].Count * 150;
		//calculate the net worth of units
		float soldierNetWorth = PlayerController.p2_listOfSoldierLists [0].Count * 20 + PlayerController.p2_listOfSoldierLists [1].Count * 24
			+ PlayerController.p2_listOfSoldierLists [2].Count * 42 + PlayerController.p2_listOfSoldierLists [3].Count * 60 
				+ PlayerController.p2_listOfSoldierLists [4].Count * 46;    
		
		float weightRatio = soldierNetWorth / buildingNetWorth;
		//the less soldier we have, the less chance to build a barrack an reverse
		float adjustment = 25 * weightRatio; 
		float returnFloat = 100 - adjustment;
		//		Debug.Log ("Chance to NOT build barrack : " + returnFloat.ToString());
		return returnFloat;
	}

	IEnumerator plusGold(){
		while (true){
			yield return new WaitForSeconds(0.5f);
			ResourceSystem.p2_gold ++;
		}
	}

	//order a specific unit to a position
	void orderUnitTo(ref Soldier _sol,Vector3 pos){
		_sol.soldierState = (int)pos.z;
		if (pos.z == 0) {
			return;		
		}
		Position2D end = GridMapUtils.GetTile(pos.x,pos.y);
		Position2D start = GridMapUtils.GetTile (_sol.transform.position.x, _sol.transform.position.y);
		_sol.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
		_sol.EndCurrentMove();
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
				_sol.destinatedPos = new Vector2(PlayerController.p1_soldierOrder[_unitType].x,PlayerController.p1_soldierOrder[_unitType].y);				
				Position2D p1_end = GridMapUtils.GetTile(_sol.destinatedPos.x,_sol.destinatedPos.y);
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
				_sol.destinatedPos = new Vector2(PlayerController.p2_soldierOrder[_unitType].x,PlayerController.p2_soldierOrder[_unitType].y);				
				Position2D p2_end = GridMapUtils.GetTile(_sol.destinatedPos.x,_sol.destinatedPos.y);
				Position2D start = GridMapUtils.GetTile(_sol.transform.position.x, _sol.transform.position.y);
				_sol.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, p2_end);
				_sol.EndCurrentMove();
			}
		}
	}
}
