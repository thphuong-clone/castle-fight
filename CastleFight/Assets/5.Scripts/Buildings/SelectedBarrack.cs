/*
* This class is used to control the barrack building behaviour
*/
using UnityEngine;
using System.Collections;

public class SelectedBarrack : SelectedBuilding {
	public Barrack parentBuilding;

	//This function is called then the player tap on the building. You know, to build unit.
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

	//This function is called then the player tap on the building. but in Single player mode
	public override void SP_isSelected(){
		if (parentBuilding.isPlayerOne) {
			GameObject.FindObjectOfType<SP_UI> ().showBarrackBuiltMenu ();
			P1_BarrackController.changeBarrack ((Barrack)parentBuilding);
		}

	}
}
