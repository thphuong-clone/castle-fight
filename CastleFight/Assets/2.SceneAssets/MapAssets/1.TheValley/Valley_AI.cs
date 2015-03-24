/*
 *
 * The AI for the Valley map.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Valley_AI : AIController {

	//this function decides what the AI should do - it's important
	protected override void whatShouldIDo(){
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
		if (strengthRatio >= 1.5f && Time.timeSinceLevelLoad > 15f) {
			//currentOrder = 0;		
			attackOrder();
			//issue an attack order
		}
		else{
			if (strengthRatio <= 0.75f){
				retreatOrder();
			}
			else{
				defenseOrder();
			}
			//currentOrder = 0;
			
		}
		//if not, first, attack the caravan, and heal all unit. if there is no caravan, group unit up, wait for power to come up
		
	}

	//find the nearlest enemy caravan
	protected override void defenseOrder (){
		//if the bots is controlling all the caravan, gather at the middle of the map.
		//if not, attack the nearlest caravan.
		bool moveToMiddle = false;

		Vector3 targettedPosition = new Vector3(0,-10f,0);

		if (PlayerController.caravanList.Count == 0){
			moveToMiddle = true;
		}
		else{
			int enemyCaravan = 0;
			foreach(Caravan c in PlayerController.caravanList){
				if (c.followingState == 1){
					enemyCaravan ++;
				}
			}
			if (enemyCaravan < 0){
				moveToMiddle = true;
			}
			else{

				moveToMiddle = false;
				foreach(Caravan c in PlayerController.caravanList){
					if (c.followingState == 1){
						if (c.transform.position.y > targettedPosition.y){
							targettedPosition = c.transform.position;
						}
					}
				}
			
			}
		}
		targettedPosition = new Vector3(UnityEngine.Random.Range(-1.5f,1.6f),1,2);

		if (moveToMiddle){
			for (int i = 0; i < 5; i ++) {
				orderList[i] = new Vector3(UnityEngine.Random.Range(-1.5f,1.6f),1,2);
				PlayerController.p2_soldierOrder[i] = orderList[i];
			}
		}
		else{
			for (int i = 0; i < 5; i ++) {
				orderList[i] = targettedPosition;
				PlayerController.p2_soldierOrder[i] = orderList[i];
			}
		}

	}

}
