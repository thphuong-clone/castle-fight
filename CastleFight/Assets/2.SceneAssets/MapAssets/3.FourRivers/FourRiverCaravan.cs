/***************
 * 
 * This class the caravan for the four rivers map
 * 
 **************/ 
using UnityEngine;
using System.Collections;

public class FourRiverCaravan : Caravan {
	protected override void Awake ()
	{
		base.Awake ();
		maximumNode = 4;
	}

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
			returnVector = new Vector2(0f,3.25f);
			break;
		case 2:
			returnVector = new Vector2(-3.25f,3.25f);
			break;
		case 3:
			returnVector = new Vector2(-3.25f,6.25f);
			break;
		case 4:
			returnVector = new Vector2(-0.000001f,6.25f);
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
