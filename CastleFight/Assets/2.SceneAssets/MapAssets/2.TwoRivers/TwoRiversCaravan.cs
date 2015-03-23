/**************************
 * 
 * The caravan for the two rivers map ...
 * 
 **************************/
using UnityEngine;
using System.Collections;

public class TwoRiversCaravan : Caravan {
	
	protected override void Awake ()
	{
		base.Awake ();
		maximumNode = 4;
	}
	
	//when the caravan chances it state, it will change the displayer color
	//as well as change the destinated position
//	protected override void changeState ()
//	{
//		switch (followingState) {
//		case 0:
//			auraColor.color = new Color(1,1,1,0.21568627451f);
//			break;
//		case 1://follow player 1
//			auraColor.color = new Color(1,0,0,0.21568627451f);
//			currentNode ++;
//			//destinatedPos = new Vector2(0,-7);
//			break;
//		case 2://follow player 2
//			auraColor.color = new Color(0,0,1,0.21568627451f);
//			//Debug.Log("this");
//			currentNode --;
//			//destinatedPos = new Vector2(0,-7);
//			break;
//		default:
//			Debug.Log("How the fuck ?");
//			break;
//		}
//		moveToPos (setUpDestinatedPos());
//	}
	
	protected override Vector2 setUpDestinatedPos ()
	{
		Vector2 returnVector = Vector2.zero;
		
		if (followingState == 0) {
			return returnVector;
		}
		switch (Mathf.Abs (currentNode)) {
		case 0:
			returnVector = new Vector2(0.00001f,0.00001f);
			break;
		case 1:
			returnVector = new Vector2(-3.5f,0.00001f);
			break;
		case 2:
			returnVector = new Vector2(-3.5f,4);
			break;
		case 3:
			returnVector = new Vector2(-0.5f,4);
			break;
		case 4:
			returnVector = new Vector2(-0.5f,5.75f);
			break;
		default:
			Debug.Log("Errr ?");
			break;
		}
		if (currentNode > 0) {
			returnVector =  Vector2.zero - returnVector;
		}
		//Debug.Log (returnVector.ToString());
		return returnVector;
	}

}
