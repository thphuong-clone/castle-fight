using UnityEngine;
using System.Collections;

public class Cannon : Archer {
	protected override void shootArrow (GameObject en){
		arrow.gameObject.SetActive (true);
		arrow.transform.localPosition = new Vector3 (0.0125f,0.35f,-1.25f);
		arrow.attackEnemy (en,new AttackInformation(attackDamage,isLightAttack));
	}

	public override IEnumerator checkHealth (){
		while (!isDead) {
			yield return new WaitForSeconds(0.1f);	
			if (health <= 0){
				isDead = true;
				//push the unit back
				r.velocity = Vector2.zero;
				Vector2 force = calculateVelocity(destinatedPos);
				force = new Vector2(force.x * 10 / moveSpeed,force.y * 10 / moveSpeed);
				r.AddForce( - calculateVelocity(destinatedPos) * 5000 / moveSpeed);
				//remove the unit from the damn list
				if (this.isPlayerOne)
					PlayerController.p1_listOfSoldierLists[4].Remove(this);
				else
					PlayerController.p2_listOfSoldierLists[4].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
		
	}
}
