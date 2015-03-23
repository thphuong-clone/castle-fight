/**************************
 * 
 * This class handles the behavior of the caravan in the Valley map.
 * 
 *************************/ 

using UnityEngine;
using System.Collections;

public class ValleyCaravan : Caravan {

	protected override void Awake(){
		base.Awake ();
		maximumNode = 1;
	}
	//when the caravan chances it state, it will change the displayer color
	//as well as change the destinated position
	protected override void changeState ()
	{
		switch (followingState) {
		case 0:
			auraColor.color = new Color(1,1,1,0.21568627451f);
			break;
		case 1://follow player 1
			auraColor.color = new Color(1,0,0,0.21568627451f);
			currentNode = 1;
			//destinatedPos = new Vector2(0,-7);
			break;
		case 2://follow player 2
			auraColor.color = new Color(0,0,1,0.21568627451f);
			//Debug.Log("this");
			currentNode = -1;
			//destinatedPos = new Vector2(0,-7);
			break;
		default:
			Debug.Log("How the fuck ?");
			break;
		}
		moveToPos (setUpDestinatedPos());
	}

	protected override Vector2 setUpDestinatedPos ()
	{
		Vector2 returnVector = Vector2.zero;
		if (followingState == 0) {
			return returnVector;
		}
		if (currentNode == 1) {
			returnVector = new Vector2(0,-6f);
		}
		else{
			if (currentNode == -1){
				returnVector = new Vector2(0,6f);
			}
		}
		//Debug.Log (returnVector.ToString());
		return returnVector;
	}

}
