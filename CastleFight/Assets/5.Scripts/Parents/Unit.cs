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

	//public float radius;

	//there are 6 types of amor. more detail in the receive damage function
	public int armorType;

	//Everyunit will have it's correspond display on the mini map of each player.
	//In each player map, each unit will have an icon for him to show up on the map
	public GameObject displayerIconPrefab;//the prefab
	GameObject p1_mapDisplayer;
	GameObject p2_mapDisplayer;

	public virtual void Awake(){
		health = maxHealth;		
		StartCoroutine (updateHealth ());
		StartCoroutine (checkHealth());
	}

	public virtual void Start(){
		setUpIcons ();
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
		/*
		 * 6 types of attack
		 * 1- Sword attack - sword man
		 * 2 - bow attack - archer
		 * 3 - knight damage - horseman
		 * 4 - gladiator attack - gladiator
		 * 5 - explosive attack - cannon - castle maybe
		 * 6 - tower attack - watch tower
		 * 6 types of defense
		 * 1 - sword amor - sword man amor
		 * 2 - cloth amor - archer
		 * 3 - knight armor - knight
		 * 4 - heavy armor - gladiator
		 * 5 - cannon amor - cannon
		 * 6 - building type - buildings
		*/
		float attackMultiply = 0;
		switch (this.armorType) {
		case 1://swordman amor, will receive extra damage from archer and watch tower
			if (info.attackType == 2 || info.attackType == 6)
				attackMultiply = 1.25f;
			else
				attackMultiply = 1;

			break;
		case 2://archer type, will recei extra damage from horseman.
			if (info.attackType == 3)
				attackMultiply = 1.25f;
			else
				attackMultiply = 1;
			break;
		case 3://horse man, take reduced damage from archer
			if (info.attackType == 2 || info.attackType == 6)
				attackMultiply = 0.65f;
			else
				attackMultiply = 1;
			break;
		case 4: //gladiator - take more damage from cannon, less damage from archer and watch tower
			if (info.attackType == 2 || info.attackType == 6)
				attackMultiply = 0.45f;
			else{
				if (info.attackType == 5)
					attackMultiply = 1.35f;
				else
					attackMultiply = 1;
			}
			break;
		case 5://cannon take reduce damage from archer and watch tower
			if (info.attackType == 2 || info.attackType == 6)
				attackMultiply = 0.75f;
			else
				attackMultiply = 1;
			break;
		case 6://building type, will take more damage from cannon
			if (info.attackType == 6)
				attackMultiply = 1.3f;
			else			
				attackMultiply = 1;
			break;
		default:
			Debug.Log("Something is wrong!");
			attackMultiply = 1;
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

	public virtual void setUpIcons(){
		//Instantiate the p1_map displayer and the p2 displayer onto the map
		p1_mapDisplayer = (GameObject)Instantiate (displayerIconPrefab);
		p2_mapDisplayer = (GameObject)Instantiate (displayerIconPrefab);
		
		//This is the parent of the icon, which is the displayer map
		GameObject p1_parent;
		GameObject p2_parent;
		
		//find the parent game object and set this game object to be the parent of this unit icon
		GameObject[] parent = GameObject.FindGameObjectsWithTag ("DisplayMap");
		if (parent [0].transform.name == "Player1_DisplayMap") {
			p1_parent = parent[0];
			p2_parent = parent[1];
		}
		else{
			p1_parent = parent[1];
			p2_parent = parent[0];
		}
		//set the parent of the icon
		p1_mapDisplayer.transform.parent = p1_parent.transform;
		p2_mapDisplayer.transform.parent = p2_parent.transform;

		//Change the color of the display icon, red of blue 
		if (this.isPlayerOne) {
			p1_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.60784313725f);
			p2_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.60784313725f);
		}
		else{
			p1_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(0,0,1,0.60784313725f);
			p2_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(0,0,1,0.60784313725f);
		}
		p2_mapDisplayer.transform.localRotation = Quaternion.Euler (new Vector3(0,0,0));
		updateMapDisplay ();
		StartCoroutine (mapDisplayerCouroutine());
	}

	//update the current position of this unit to the corresponding map display
	public virtual void updateMapDisplay(){
		p1_mapDisplayer.transform.localPosition = this.transform.position + new Vector3(0,0,-0.5f);
		p2_mapDisplayer.transform.localPosition = new Vector3(-this.transform.position.x,-this.transform.position.y,-0.5f);;
	}

	public virtual IEnumerator mapDisplayerCouroutine(){
		while (!this.isDead) {
			updateMapDisplay();
			yield return new WaitForSeconds(0.5f);
		}
		Destroy (p1_mapDisplayer);
		Destroy (p2_mapDisplayer);
	}

}

public class AttackInformation{
	public float attackDamage;
	public int attackType;

	public AttackInformation(float a, int b){
		attackDamage = a;
		attackType = b;
	}

}

