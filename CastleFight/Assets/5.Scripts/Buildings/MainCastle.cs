using UnityEngine;
using System.Collections;

public class MainCastle : Building {
//	public override void Awake(){
//		base.Awake ();
//	}

	public override IEnumerator updateHealth(){
		float lastHealth = health;
		while (true) {
			yield return new WaitForSeconds(0.1f);		
			//if the unit is at max health, hide the health bar 
			if (health == maxHealth){
				healthBar.transform.localScale = new Vector3(0,1,1);
			}
			
			if (lastHealth != health){
				//update health
				lastHealth = health;
				healthBar.transform.localScale = new Vector3((float)health/(float)maxHealth * 2.5f,2,1);
				if (health <= 0){
					healthBar.transform.localScale = new Vector3(0,2,1);
				}
				if ((float)health/(float)maxHealth <= 0.6f){
					if ((float)health/(float)maxHealth <= 0.3f){
						healthBar.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
					}
					else{
						healthBar.GetComponent<SpriteRenderer>().color = new Color(255,150,0);
					}
				}
				else{
					healthBar.GetComponent<SpriteRenderer>().color = new Color(0,255,0);
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
					PlayerController.p1_buildingList[0].Remove(this);
				else
					PlayerController.p2_buildingList[0].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
		
	}

	public override void OnDestroy (){
		if (this.isPlayerOne) {
			PlayerController.p1_buildingList[0].Remove(this.gameObject.GetComponent<Building>());
		}
		else{
			PlayerController.p2_buildingList[0].Remove(this.gameObject.GetComponent<Building>());
		}
		base.OnDestroy ();
	}

}
