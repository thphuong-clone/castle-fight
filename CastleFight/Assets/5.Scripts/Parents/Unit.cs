//******************************

//The basic units, every thing else, including buildings and soldiers will inherit from this class

//******************************

using UnityEngine;
using System.Collections;


public class Unit : MonoBehaviour {
	//The player 1 is the player at the bottom, the player 2 is the player at the top.
	//so if HE IS the player 1, he is at bot, if FALSE, he is at the top
	public bool isPlayerOne;

	public bool isDead; //check if the unit is dead, so we can switch attack to another unit.
	
	//The health of the unit. If this value went below zero, the Unit will die.
	public float health;
	public float maxHealth;

	public GameObject healthBar;

	public float radius;

	//There are 3 amor types.
	//0 - unarmed, take extra damage from both attack type
	//1 - light amor, take normal damage (100% damage from ligith attack, and reduced damage from heavy attack)
	//2 - heavy amor, take reduced damage from light attack, and increase damage from heavy attack
	public int armorType;

	//Everyunit will have it's correspond display on the mini map of each player.
	//In each player map, each unit will have an icon for him to show up on the map
	public Unit displayerIconPrefab;//the prefab
	public Unit p1_mapDisplayer;
	public Unit p2_mapDisplayer;

	public virtual void Awake(){
		health = maxHealth;		
		StartCoroutine (updateHealth ());
		StartCoroutine (checkHealth());
		//Instantiate the p1_map displayer and the p2 displayer onto the map
	}

	public virtual void Update(){
	}

	public virtual IEnumerator checkHealth(){
		while (!isDead) {
			yield return new WaitForSeconds(0.1f);	
			if (health <= 0){
				isDead = true;
				//push the unit back
				//play some fancy animation here. hrr, I don't know
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
	}

	public virtual IEnumerator updateHealth(){
		float lastHealth = health;
		while (true) {
			yield return new WaitForSeconds(0.05f);		
			//if the unit is at max health, hide the health bar 
			if (health == maxHealth){
				healthBar.transform.localScale = new Vector3(0,1,1);
			}
			
			if (lastHealth != health){
				//update health
				lastHealth = health;
				healthBar.transform.localScale = new Vector3((float)health/(float)maxHealth,1,1);
				if (health <= 0){
					healthBar.transform.localScale = new Vector3(0,1,1);
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

	public virtual void receiveDamage(AttackInformation info){
		float attackMultiply = 0;
		switch (this.armorType) {
		case 0://un-amored - receve all damage
			attackMultiply = 1;
			break;
		case 1: // light amor, take full damage form light, half damage from heavy
			if (info.lightAttack)
				attackMultiply = 1;
			else
				attackMultiply = 0.75f;
			break;
		case 2://this unit use heavy armor, will take bonus damage from heavy attack
			if (info.lightAttack)
				attackMultiply = 0.4f;
			else
				attackMultiply = 1.25f;
			break;
		default:
			break;
		}
		this.health -= info.attackDamage * attackMultiply;
	}

	//This function is absolutely useless, but I keep it here so that the game won't annoy me with the 
	//Catch error E but I don't use it. errr.
	public void debugException(string _string){
		string s = _string.ToString();
		s= "";
		if (s != "")
			Debug.Log(s);
	}

	//update the current position of this unit to the corresponding map display
	public virtual void updateMapDisplay(){
	}

}

public class AttackInformation{
	public float attackDamage;
	public bool lightAttack;

	public AttackInformation(float a, bool b){
		attackDamage = a;
		lightAttack = b;
	}

}

