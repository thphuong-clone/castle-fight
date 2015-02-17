using UnityEngine;
using System.Collections;

public class SelectedCastle : SelectedBuilding {
	public MainCastle parentBuilding;

	public override void isSeleted () {
		if (parentBuilding.isPlayerOne) {
			GameObject.FindObjectOfType<P1_Controller> ().showHouseBuiltMenu ();
			//P1_BarrackController.changeBarrack ((MainCastle)parentBuilding);
		}
		else{
			GameObject.FindObjectOfType<P2_Controller> ().showHouseBuiltMenu ();
			//P2_BarrackController.changeBarrack ((MainCastle)parentBuilding);
		}
		
	}
}
