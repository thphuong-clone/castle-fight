using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Archer : Soldier {
	public Arrow arrow;

	public override void Awake ()
	{
		base.Awake ();
		arrow.gameObject.SetActive (false);
	}

//
//	protected override void findEnemy(){	
//		if (!isAttacking) {
//			Debug.Log (".");
//			Vector2 thisPos = new Vector2(transform.position.x,transform.position.y);
//			//find all the collider in the range of the unit, which is 1.5 radius - maybe 2, I can't decide yet
//			Collider2D[] col = Physics2D.OverlapCircleAll(thisPos,2f);
//			List<GameObject> enemyList = new List<GameObject>();
//			if (col.Length == 0){}
//			else{
//				foreach(Collider2D c in col){
//					try{
//						//if enemy, then add him to the enemy list.
//						if (c.gameObject.GetComponent<Unit>().isPlayerOne != this.isPlayerOne){
//							//add to the enemy list if he is not dead yet.
//							if (!c.gameObject.GetComponent<Unit>().isDead){
//								enemyList.Add(c.gameObject);
//							}
//						}
//					}
//					//that mean it's a moutain or whatever, I haven't invented it yet.
//					catch (Exception e){
//						debugException(e.ToString());
//					}
//				}
//				//if there is no enemy
//				if (enemyList.Count  == 0){
//					//do nothing, maybe, do whatever you are meant to do, like move to the destinated position ?
//				}
//				//there is enemy here bitch.
//				else{
//					float distance = 99999;
//					GameObject nearest = this.gameObject;
//					foreach(GameObject c in enemyList){
//						//find the nearest enemy
//						if (calculateDistance(c) < distance){
//							nearest = c;
//							distance = calculateDistance(c);
//						}
//					}
//					//attack him
//
//					StartCoroutine(attackEnemy(nearest));
//					//soldierState = -1;
//					isAttacking = true;
//					//Debug.Log("Why");
//				}
//			}			
//		}
//	}

	//override the attack function since this unit attack from far away, not near
	public override IEnumerator attackEnemy (GameObject enemy){
		isAttacking = true;
		int exState = soldierState;
		soldierState = -1;

		int count = 0;
		while (calculateDistance (enemy) > 1.9f) {
			r.velocity =  calculateVelocity(new Vector2(enemy.transform.position.x,enemy.transform.position.y));
			count ++;
			if (enemy.GetComponent<Unit>().isDead || count == 20 || this.isDead){
				goto StopAttack;
				//StopCoroutine("attackEnemy"); - this doesn't work.
				//break;
			}
			yield return new WaitForSeconds(0.05f);

		}

		r.velocity = calculateVelocity (new Vector2(enemy.transform.position.x,enemy.transform.position.y));
		r.velocity = Vector2.zero;
		r.drag = 100;

		if (!this.isDead){
			ani.SetBool("Attack",true);
			isAttacking = true;
			yield return new WaitForSeconds(animationTime);
			shootArrow(enemy);
			yield return new WaitForSeconds(2f - animationTime);
			ani.SetBool("Attack",false);
		}

	StopAttack:
		if (soldierState == -1)
			soldierState = exState;

		r.drag = 12;
		isAttacking = false;
		yield return null;
	}


	protected virtual void shootArrow(GameObject en){
		//arrow.gameObject.SetActive (true);
		arrow.transform.localPosition = new Vector3 (0.1f,0.3f,-1.25f);
		arrow.attackEnemy (en,new AttackInformation(attackDamage,attackType));
	}

	public override IEnumerator checkHealth (){
		while (!isDead) {
			yield return new WaitForSeconds(0.1f);	
			if (health <= 0){
				isDead = true;
				if (r.mass != 2)
					r.mass /= 2;
				//push the unit back
				r.velocity = Vector2.zero;
				Vector2 force = calculateVelocity(destinatedPos);
				force = new Vector2(force.x * 10 / moveSpeed,force.y * 10 / moveSpeed);
				r.AddForce( - calculateVelocity(destinatedPos) * 5000 / moveSpeed);
				//remove the unit from the damn list
				if (this.isPlayerOne)
					PlayerController.p1_listOfSoldierLists[1].Remove(this);
				else
					PlayerController.p2_listOfSoldierLists[1].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);

	}
	
}
