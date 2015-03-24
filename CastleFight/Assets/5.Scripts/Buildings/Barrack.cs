/*
This is the barrack, it will spawn unit
It can heals unit around it as well.
*/
/*
 *Balancing issue
 *Swordman counter horsema and gladiator but weak to archer and watch tower. Cost 100.
 *Archer counter swordman but weak to horseman and gladiator. Cost 120
 *Horseman counter archer and cannon, but weak to swordman. Cost 220
 *Gladiator counter archer and watch tower but weak to cannon and swordman. Cost 350
 *cannon counter buildings and gladiator but weak to horseman. - cost 240
 *watch tower counter swordman and generally other unit but weak to cannon
 */
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using PathFinder;

public class Barrack : Building {
	//What type of soldier this is building. 
	//1 is swordman, 2 is archer, 3 is horseman 4 is gladiator, 5 is cannon
	public int currentSoldierBuilt;

	//The time countdown it. This will increase each seconds and will pull out a soldier when the time is enough
	public float buildCountDown;

	public List<float> buildCost = new List<float> {1f,1f,1f,1f,1f};

	//List of all the prefab
	public SwordMan swordmanPrefab;
	public Archer archerPrefab;
	public HorseMan horsemanPrefab;
	public Gladiator gladiatorPrefab;
	public Cannon cannonPrefab;
	//The time cost it takes to build the unit.
	[SerializeField]
	public float swordmanCost ;
	[SerializeField]
	public float archerCost;
	[SerializeField]
	public float horsemanCost;
	[SerializeField]
	public float gladiatorCost;
	[SerializeField]
	public float cannonCost;

	public override void Awake (){
		base.Awake ();
		buildCost [0] = swordmanCost;
		buildCost [1] = archerCost;
		buildCost [2] = horsemanCost;
		buildCost [3] = gladiatorCost;
		buildCost [4] = cannonCost;
		StartCoroutine (checkAndHealSoldiers ());
		StartCoroutine (buildSoldier());
	}

    public override PathFinder.Position2D[] GetOccupyingGrid()
    {
        return GameUtil.GameConstant.GRID_FOUR;
    }

	IEnumerator buildSoldier(){
		while (true) {
			buildCountDown += 0.05f;
			switch(currentSoldierBuilt){
			case 1:
				if(buildCountDown >= swordmanCost){
					buildCountDown -= swordmanCost;
					StartCoroutine(spawnSwordman());
				}
				break;
			case 2:
				if(buildCountDown >= archerCost){
					buildCountDown -= archerCost;
					StartCoroutine(spawnArcher());
				}
				break;
			case 3:
				if(buildCountDown >= horsemanCost){
					buildCountDown -= horsemanCost;
					StartCoroutine(spawnHorseMan());
				}
				break;
			case 4:
				if(buildCountDown >= gladiatorCost){
					buildCountDown -= gladiatorCost;
					StartCoroutine(spawnGladiator());
				}
				break;
			case 5:
				if(buildCountDown >= cannonCost){
					buildCountDown -= cannonCost;
					StartCoroutine(spawnCannon());
				}
				break;
			default:
				if(buildCountDown >= swordmanCost){
					buildCountDown -= swordmanCost;
					StartCoroutine(spawnSwordman());
				}
				break;
			}

			yield return new WaitForSeconds(0.05f);
		}
	}

	//spawn a swordman
	IEnumerator spawnSwordman(){
		//SwordMan newSoldier = (SwordMan)Instantiate (swordmanPrefab);
		Soldier newSoldier = (SwordMan)Instantiate (swordmanPrefab);

		newSoldier.isPlayerOne = this.isPlayerOne;
		newSoldier.GetComponent<SpriteRenderer> ().color = unitAura.GetComponent<SpriteRenderer> ().color;
		newSoldier.transform.position = this.transform.position + new Vector3 (0,0.1f,0);
		newSoldier.soldierState = 2;
		if (!newSoldier.isPlayerOne) {
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y - 1);
			newSoldier.transform.position += new Vector3 (0,-.2f,0);
			newSoldier.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
			PlayerController.p2_listOfSoldierLists[0].Add(newSoldier);
		}
		else{
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y + 1);
			PlayerController.p1_listOfSoldierLists[0].Add(newSoldier);
		}
		//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y);
		//PlayerController.orderSoldier ();
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne, 0);
	}

	//spawn an archer
	IEnumerator spawnArcher(){
		//Archer newSoldier = (Archer)Instantiate (archerPrefab);
		Soldier newSoldier = (Archer)Instantiate (archerPrefab);

		newSoldier.isPlayerOne = this.isPlayerOne;
		newSoldier.GetComponent<SpriteRenderer> ().color = unitAura.GetComponent<SpriteRenderer> ().color;
		newSoldier.transform.position = this.transform.position + new Vector3 (0,0.1f,0);
		newSoldier.soldierState = 2;
		if (!newSoldier.isPlayerOne) {
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y - 1);
			newSoldier.transform.position += new Vector3 (0,-0.2f,0);
			newSoldier.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
			PlayerController.p2_listOfSoldierLists[1].Add(newSoldier);
		}
		else{
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y + 1);
			PlayerController.p1_listOfSoldierLists[1].Add(newSoldier);
		}
		//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y);
		//PlayerController.orderSoldier ();
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,1);
	}


	//spawn a horseman
	IEnumerator spawnHorseMan(){
		//HorseMan newSoldier = (HorseMan)Instantiate (horsemanPrefab);
		Soldier newSoldier = (HorseMan)Instantiate (horsemanPrefab);

		newSoldier.isPlayerOne = this.isPlayerOne;
		newSoldier.GetComponent<SpriteRenderer> ().color = unitAura.GetComponent<SpriteRenderer> ().color;
		newSoldier.transform.position = this.transform.position + new Vector3 (0,0.75f,0);
		newSoldier.soldierState = 2;
		if (!newSoldier.isPlayerOne) {
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y - 1);
			newSoldier.transform.position += new Vector3 (0,-1.5f,0);
			newSoldier.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
			PlayerController.p2_listOfSoldierLists[2].Add(newSoldier);
		}
		else{
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y + 1);
			PlayerController.p1_listOfSoldierLists[2].Add(newSoldier);
		}
		//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y);
		//PlayerController.orderSoldier ();
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,2);
	}


	//spawn a gladiator
	IEnumerator spawnGladiator(){
//		Gladiator newSoldier = (Gladiator)Instantiate (gladiatorPrefab);
		Soldier newSoldier = (Gladiator)Instantiate (gladiatorPrefab);

		newSoldier.isPlayerOne = this.isPlayerOne;
		newSoldier.GetComponent<SpriteRenderer> ().color = unitAura.GetComponent<SpriteRenderer> ().color;
		newSoldier.transform.position = this.transform.position + new Vector3 (0,0.75f,0);
		newSoldier.soldierState = 2;
		if (!newSoldier.isPlayerOne) {
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y - 1);
			newSoldier.transform.position += new Vector3 (0,-1.5f,0);
			newSoldier.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
			PlayerController.p2_listOfSoldierLists[3].Add(newSoldier);
		}
		else{
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y + 1);
			PlayerController.p1_listOfSoldierLists[3].Add(newSoldier);
		}
		//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y);
		//		PlayerController.orderSoldier ();
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,3);
	}


	//spawn a cannon
	IEnumerator spawnCannon(){
//		Cannon newSoldier = (Cannon)Instantiate (cannonPrefab);
		Soldier newSoldier = (Cannon)Instantiate (cannonPrefab);

		newSoldier.isPlayerOne = this.isPlayerOne;
		newSoldier.GetComponent<SpriteRenderer> ().color = unitAura.GetComponent<SpriteRenderer> ().color;
		newSoldier.transform.position = this.transform.position + new Vector3 (0,0.75f,0);
		newSoldier.soldierState = 2;
		if (!newSoldier.isPlayerOne) {
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y - 1);
			newSoldier.transform.position += new Vector3 (0,-1.5f,0);
			newSoldier.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
			PlayerController.p2_listOfSoldierLists[4].Add(newSoldier);
		}
		else{
			//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y + 1);
			PlayerController.p1_listOfSoldierLists[4].Add(newSoldier);
		}
		//newSoldier.destinatedPos = new Vector2 (newSoldier.transform.position.x,newSoldier.transform.position.y);
		//		PlayerController.orderSoldier ();
		yield return new WaitForSeconds (0.25f);
		orderUnit (ref newSoldier,newSoldier.isPlayerOne,4);
	}

	//This function order the newly created unit to a place with a order.
	void orderUnit(ref Soldier _sol,bool _isPlayerOne,int _unitType){
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

//The coroutine to heal soldier around the building
	IEnumerator checkAndHealSoldiers(){
		while (true) {
			healSoldiers();
			yield return new WaitForSeconds(1f);
		}
	}

//This building will heal every wounded soldiers around 
	public void healSoldiers(){
		if (!this.isDead) {
			Vector2 thisPos = new Vector2(transform.position.x,transform.position.y);
			//find all the collider in the range of the unit, which is 1.5 radius - maybe 2, I can't decide yet
			Collider2D[] col = Physics2D.OverlapCircleAll(thisPos,2f);
			List<Soldier> alliesList = new List<Soldier> ();
			if (col.Length == 0){}//there are no unit in the area, nope
			else{
				foreach(Collider2D c in col){
					try{
						//If there is allies nearby, add him to the list.
						if (c.gameObject.GetComponent<Soldier>().isPlayerOne == this.isPlayerOne){
							//add him to the list if he is not dead yet.
							if (!c.gameObject.GetComponent<Soldier>().isDead){
								alliesList.Add(c.gameObject.GetComponent<Soldier>());
							}
						}
					}
					//that mean it's a moutain or whatever, I haven't invented it yet.
					catch (Exception e){
						debugException(e.ToString());
					}
				}
				if (alliesList.Count == 0){}//the list is empty, there is no ally
				else{ // heal him
					foreach (Soldier s in alliesList){
						if (s.health < s.maxHealth){
							s.health += s.maxHealth * 0.025f;
							if (s.health >= s.maxHealth)
								s.health = s.maxHealth;
						}
					}
				}

			}
		}
	}

	public override IEnumerator checkHealth (){
		while (!isDead) {
			yield return new WaitForSeconds(0.1f);	
			if (health <= 0){
				isDead = true;
				//push the unit back
				if (this.isPlayerOne)
					PlayerController.p1_buildingList[1].Remove(this);
				else
					PlayerController.p2_buildingList[1].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
		
	}

	public override void OnDestroy (){
		if (this.isPlayerOne) {
			PlayerController.p1_buildingList[1].Remove(this.gameObject.GetComponent<Building>());
		}
		else{
			PlayerController.p2_buildingList[1].Remove(this.gameObject.GetComponent<Building>());
		}
		base.OnDestroy ();
	}

}
