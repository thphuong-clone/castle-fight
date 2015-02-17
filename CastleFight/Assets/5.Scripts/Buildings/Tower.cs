using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tower : Building {
	public Arrow arrow;

	public float attackDamage;
	public bool isLightAttack;

	bool isAttacking;

	public override void Awake(){
		base.Awake ();
		StartCoroutine (findEnemy ());
		isAttacking = false;
	}

	IEnumerator findEnemy(){
		//while not finding enemy around, find an enemy in the range
		while (!isAttacking) {
			Vector2 thisPos = new Vector2(transform.position.x,transform.position.y);
			//find all the collider in the range of the unit, which is 2 radius
			Collider2D[] col = Physics2D.OverlapCircleAll(thisPos,3f);
			List<GameObject> enemyList = new List<GameObject>();
			if (col.Length == 0){			
			}
			else{
				foreach(Collider2D c in col){
					try{
						if (c.gameObject.GetComponent<Unit>().isPlayerOne != this.isPlayerOne){
							if (!c.gameObject.GetComponent<Unit>().isDead){
								enemyList.Add(c.gameObject);
							}
						}
					}
					catch (Exception e){
						string s = e.ToString();
						s= "";
						if (s != ""){
							Debug.Log(s);
						}
					}
				}

				if (enemyList.Count  == 0){
				}
				else{
					float distance = 99999;
					GameObject nearest = this.gameObject;
					foreach(GameObject c in enemyList){
						if (calculateDistance(c) < distance){
							nearest = c;
							distance = calculateDistance(c);
						}
					}
					isAttacking = true;
					StartCoroutine(attackEnemy(nearest));
					break;
				}
			}
			
			yield return new WaitForSeconds(0.2f);
		}
		//Debug.Log (transform.name + " is attacking now!");

	}

	IEnumerator attackEnemy (GameObject enemy){
		//if the unit is dead then there is nothing to do
		if (this.isDead) {
			yield return null;
		}
		else{
			//if enemy is out of range, check the area again for other enemy
			if (calculateDistance(enemy) >= 2.5f){
				yield return new WaitForSeconds(0.5f);
			}
			//if the enemy is in range, then attack the mother fucker
			else{
				isAttacking = true;
				yield return new WaitForSeconds(0.5f);
				shootArrow(enemy);
				yield return new WaitForSeconds(1.5f);
			}
		}

				
		//Enemy is dead now.
		isAttacking = false;
		StartCoroutine (findEnemy());
		yield return null;
	}

	protected virtual void shootArrow(GameObject en){
		arrow.gameObject.SetActive (true);
		arrow.transform.localPosition = Vector3.zero;

		float a = -Mathf.Atan((en.transform.position.x - transform.position.x )/ (en.transform.position.y - transform.position.y));
		if (transform.position.y > en.transform.position.y) {
			a += 180 * Mathf.Deg2Rad;
		}
		arrow.transform.localRotation = Quaternion.Euler (0,0,a * Mathf.Rad2Deg);


		arrow.attackEnemy (en,new AttackInformation(attackDamage,isLightAttack));
	}


	float calculateDistance(GameObject col){
		float x = col.gameObject.transform.position.x;
		float y = col.gameObject.transform.position.y;
		
		x = Mathf.Abs (this.transform.position.x - x);
		y = Mathf.Abs (this.transform.position.y - y);
		
		float distance = new Vector2(x,y).magnitude;
		return distance;
	}

	public override IEnumerator checkHealth (){
		while (!isDead) {
			yield return new WaitForSeconds(0.1f);	
			if (health <= 0){
				isDead = true;
				//push the unit back
				if (this.isPlayerOne)
					PlayerController.p1_buildingList[2].Remove(this);
				else
					PlayerController.p2_buildingList[2].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
		
	}

}
