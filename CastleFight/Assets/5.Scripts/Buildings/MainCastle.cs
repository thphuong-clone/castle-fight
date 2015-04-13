using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainCastle : Building {
    public override PathFinder.Position2D[] GetOccupyingGrid()
    {
        return GameUtil.GameConstant.GRID_FOUR;
    }

	public override void Awake(){
		StartCoroutine (checkAndHealSoldiers ());
		base.Awake ();
	}

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

	IEnumerator checkAndHealSoldiers(){
		while (true) {
			healSoldiers();
			yield return new WaitForSeconds(1f);
		}
	}

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
							s.health += s.maxHealth * 0.035f;
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
					PlayerController.p1_buildingList[0].Remove(this);
				else
					PlayerController.p2_buildingList[0].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		loseFunction ();
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
		
	}

	void loseFunction(){
		Time.timeScale = 0.00000001f;
		//maybe I need an if else to check the game type so that ...
		//or a static variable to check the game type ....
		try{			
			GameObject.Find ("Player1-UI").gameObject.SetActive(false);
			GameObject.Find ("Player2-UI").gameObject.SetActive (false);
			//GameObject.Find ("WinLose Annoucement").gameObject.SetActive(true);
			if (this.isPlayerOne) {
				GameObject.Find ("P1_Winning Text").gameObject.GetComponent<Text>().text = "BOOOOO\nYOU LOSE!!!";
				GameObject.Find ("P2_Winning Text").gameObject.GetComponent<Text>().text = "CONGRATULATION\nYOU WIN!!!!";
			}
			else{
				GameObject.Find ("P2_Winning Text").gameObject.GetComponent<Text>().text = "BOOOOO\nYOU LOSE!!!";
				GameObject.Find ("P1_Winning Text").gameObject.GetComponent<Text>().text = "CONGRATULATION\nYOU WIN!!!!";		
			}
		}
		catch{
			GameObject.FindObjectOfType<SP_UI>().showWindLose(!this.isPlayerOne);
		}


	}

	public override IEnumerator gainHealth(){
		while (true) {
			yield return new WaitForSeconds(1);
			if (this.health < this.maxHealth) 
				this.health += 0.005f * this.maxHealth;
			if (this.health > this.maxHealth)
				this.health = this.maxHealth;
		}
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
