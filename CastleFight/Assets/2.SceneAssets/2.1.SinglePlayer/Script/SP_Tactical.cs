/*
 *This class handle the group command for the single player mode 
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SP_Tactical : MonoBehaviour {
	public SP_UI masterUI;

	public int controlledSoldier; //0 = sword man .....

	//This will be active/deactive a lot so we should use on enable and disable
	void OnEnable(){
		controlledSoldier = -1;
	}

	void OnDisable(){
	}

	//This function will be called when we press an unit button
	public void changeControlledUnit(int _controlledSoldier){
		controlledSoldier = _controlledSoldier;
		masterUI.cancleEverythingButton.gameObject.SetActive (false);
		masterUI.unitCommandButton.gameObject.SetActive (true);
		SP_InputManager.controlingState = _controlledSoldier;
		foreach (Soldier s in PlayerController.p1_listOfSoldierLists[_controlledSoldier]) {
			s.gameObject.GetComponent<SpriteRenderer>().color =  new Color (1,1,1,0.60784313725f);		
		}
	}
}
