using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class Caravan : MonoBehaviour {

	public int moveSpeed;

	public GameObject aura;
	SpriteRenderer auraColor;
	Rigidbody2D r;

	//0 for not follow anyone, 1 for following player one, and 2 for following player 2
	public int followingState;

	//Display the cavaran on the minimap
	public GameObject displayerIconPrefab;//the prefab
	GameObject p1_mapDisplayer;
	GameObject p2_mapDisplayer;

	[SerializeField]
	int currentNode;

	void Awake(){
		currentNode = 0;
		r = this.gameObject.GetComponent<Rigidbody2D> ();
		auraColor = aura.gameObject.GetComponent<SpriteRenderer> ();
		setUpIcons ();
		StartCoroutine (checkSide ());
	}

	IEnumerator checkSide(){
		while (true) {
			checkSurrounding ();
			yield return new WaitForSeconds(0.5f);		
		}
	}

	void checkSurrounding(){
		int exFollowingState = followingState;
		Vector2 thisPos = new Vector2(transform.position.x,transform.position.y);
		//find all the collider in the range of the unit, which is 1.5 radius - maybe 2, I can't decide yet
		Collider2D[] col = Physics2D.OverlapCircleAll(thisPos,1.5f);
		//if this game object collide no object - do nothing
		if (col.Length == 0){
			Debug.Log("Errrr  ?");
		}
		else{
			//In here, we will try to count how many number of soldier in range of this game object and act accordingly
			int numberOfPlayer1_soldiers = 0;
			int numberOfPlayer2_soldiers = 0;

			foreach(Collider2D c in col){
				try{
					//add the number accordingly to the value.
					if (c.gameObject.GetComponent<Soldier>().isPlayerOne ){
						//add to the enemy list if he is not dead yet.
						if (!c.gameObject.GetComponent<Unit>().isDead){
							numberOfPlayer1_soldiers ++;
						}
					}
					else{
						if (!c.gameObject.GetComponent<Unit>().isDead){
							numberOfPlayer2_soldiers ++;
						}
					}
				}
				//that mean it's a moutain or building, don't add it..
				catch (Exception e){
					debugException(e.ToString());
				}
			}
			//Now, change the caravan side
			if (numberOfPlayer1_soldiers == numberOfPlayer2_soldiers){
				//Do nothing
			}
			else{
				if (numberOfPlayer1_soldiers > numberOfPlayer2_soldiers){
					//follow the player one..
					followingState = 1;
				}
				else{
					//follow player two.
					followingState = 2;
				}
			}
			if (exFollowingState != followingState){
				//if the state has changed
				changeState();
			}
			moveToPos (setUpDestinatedPos());
		}
	}

	void changeState(){		
		switch (followingState) {
		case 0:
			auraColor.color = new Color(1,1,1,0.21568627451f);
			break;
		case 1://follow player 1
			auraColor.color = new Color(1,0,0,0.21568627451f);
			currentNode ++;
			//destinatedPos = new Vector2(0,-7);
			break;
		case 2://follow player 2
			auraColor.color = new Color(0,0,1,0.21568627451f);
			//Debug.Log("this");
			currentNode --;
			//destinatedPos = new Vector2(0,-7);
			break;
		default:
			Debug.Log("How the fuck ?");
			break;
		}
		moveToPos (setUpDestinatedPos());

	}

	Vector2 setUpDestinatedPos(){
		Vector2 returnVector = Vector2.zero;
		if (followingState == 0) {
			return returnVector;
		}
		switch (Mathf.Abs (currentNode)) {
		case 0:
			returnVector = Vector2.zero;
			break;
		case 1:
			returnVector = new Vector2(3.5f,0);
			break;
		case 2:
			returnVector = new Vector2(3.5f,3);
			break;
		case 3:
			returnVector = new Vector2(-3.5f,3);
			break;
		case 4:
			returnVector = new Vector2(-3.5f,6);
			break;
		case 5:
			returnVector = new Vector2(0,6);
			break;
		default:
			Debug.Log("Errr ?");
			break;
		}
		if (currentNode > 0) {
			returnVector =  Vector2.zero - returnVector;
		}
		return returnVector;

	}

	//move the the destinated position
	void moveToPos(Vector2 _position){
		if (followingState == 0)
			return;

		float angle = 0;
		float x = 0;
		float y = 0;
		try {
			x =Mathf.Abs(_position.x - transform.position.x);
			y =Mathf.Abs( _position.y - transform.position.y);
            //Debug.Log(x + "," + y);
			angle = Mathf.Atan(x/y);

			
			float a = -Mathf.Atan((_position.x - transform.position.x )/ (_position.y - transform.position.y));
			
			if (transform.position.y > _position.y) 
				a += 180 * Mathf.Deg2Rad;
			if (!float.IsNaN(a)){				
				transform.localRotation = Quaternion.Euler (0,0,a * Mathf.Rad2Deg);
			}
			else{				
				transform.localRotation = Quaternion.Euler (0,0,0);
				angle = 0;
				x = 0;
				y = 0;
			}
		}
		catch(Exception e){
			Debug.Log(e.ToString());
			angle = 0;
			x = 0;
			y = 0;
		}
		//calculate the angle

        //Debug.Log(angle);
		
		x = Mathf.Sin(angle);
		y = Mathf.Sqrt(1 - x * x );

        //Debug.Log(x + "," + y);

        if (transform.position.x > _position.x)
        {
            x = -x;
        }

        if (transform.position.y > _position.y)
        {
            y = -y;
        }

        //Debug.Log(x + "," + y);
		
		Vector2 returnVector = new Vector2 (x, y) * moveSpeed /4;
		r.velocity = returnVector;

		if (Mathf.Abs (this.transform.position.x - _position.x) < 0.35f && Mathf.Abs (this.transform.position.y - _position.y) < 0.35f && followingState != 0) {
			if (followingState == 1)
				currentNode ++;
			else
				currentNode --;

			if (Mathf.Abs(currentNode) >= 6){
				destinationComplete();
			}
		}
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
		
		//set the color of the icon 
		p1_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.80392156862f);
		p2_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.80392156862f);

		p2_mapDisplayer.transform.localRotation = Quaternion.Euler (new Vector3(0,0,0));
		updateMapDisplay ();
		StartCoroutine (mapDisplayerCouroutine());
	}

	public virtual void updateMapDisplay(){
		switch(followingState){
		case 0:
			p1_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.80392156862f);
			p2_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.80392156862f);
			break;
		case 1:
			p1_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.80392156862f);
			p2_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(1,0,0,0.80392156862f);
			break;
		case 2:
			p1_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(0,0,1,0.80392156862f);
			p2_mapDisplayer.GetComponent<SpriteRenderer>().color = new Color(0,0,1,0.80392156862f);
			break;
		default:
			Debug.Log("How the fuck ?");
			break;
		}

		p1_mapDisplayer.transform.localPosition = this.transform.position + new Vector3(0,0,-0.5f);
		p2_mapDisplayer.transform.localPosition = new Vector3(-this.transform.position.x,-this.transform.position.y,-0.5f);;
	}

	public virtual IEnumerator mapDisplayerCouroutine(){
		while (true) {
			updateMapDisplay();
			yield return new WaitForSeconds(0.5f);
		}
	}

	void destinationComplete(){
		if (currentNode > 0) {
			//give player 1 the money
		}
		else {
			//give player 2 the money
		}
		Destroy (this.gameObject);
	}

	void OnDisable(){
		Destroy (p1_mapDisplayer);
		Destroy (p2_mapDisplayer);
	}

	//This function is absolutely useless, but I keep it here so that the game won't annoy me with the 
	//Catch error E but I don't use it. errr.
	public void debugException(string _string){
		string s = _string.ToString();
		s= "";
		if (s != "")
			Debug.Log(s);
	}

}
