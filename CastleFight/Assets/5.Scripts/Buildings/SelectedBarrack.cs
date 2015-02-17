/*
* This class is used to control the barrack building behaviour
*/
using UnityEngine;
using System.Collections;

public class SelectedBarrack : SelectedBuilding {
	public Barrack parentBuilding;


	public override void isSeleted () {
		if (parentBuilding.isPlayerOne) {
			GameObject.FindObjectOfType<P1_Controller> ().showSoldierBuilt ();
			P1_BarrackController.changeBarrack ((Barrack)parentBuilding);
		}
		else{
			GameObject.FindObjectOfType<P2_Controller> ().showSoldierBuilt ();
			P2_BarrackController.changeBarrack ((Barrack)parentBuilding);
		}

	}
}
