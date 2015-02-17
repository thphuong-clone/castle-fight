using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackRange : MonoBehaviour {
	public Soldier parentUnit;

	public void OnTriggerStay2D(Collider2D col){
		//if the soldier is already attacking something else
//		if (parentUnit.isAttacking) {
//
//		}
//		if (col.gameObject.tag == "Building" || col.gameObject.tag == "Soldier"){
//			if (col.gameObject.GetComponent<Unit>().isPlayerOne != parentUnit.isPlayerOne){
//				parentUnit.isAttacking = true;
//				//attack
//			}
//		}
	}	
	

}
