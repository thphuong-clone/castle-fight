/*
This function handle the input of the player 1.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class P1_Controller : MonoBehaviour {
	public GameObject soldierMenu;
	public GameObject collapseAllButton;

	public GameObject houseBuiltMenu;

	public GameObject tacticButton;
	public GameObject tacticalMap;

	void Awake(){
		showMap = false;
		hideAllUI ();
	}

//hide all the UI element, if player cancel.
	public void hideAllUI(){
		collapseAllButton.gameObject.SetActive (false);
		soldierMenu.gameObject.SetActive (false);
		tacticalMap.gameObject.SetActive (false);
		houseBuiltMenu.gameObject.SetActive (false);

		tacticButton.gameObject.SetActive (true);
		if (showMap)
			showMap = !showMap;
	}

//Show the interface of the barrack building
	public void showSoldierBuilt(){
		hideAllUI ();
		tacticButton.gameObject.SetActive (false);

		collapseAllButton.gameObject.SetActive (true);
		soldierMenu.gameObject.SetActive (true);

	}

	public void showHouseBuiltMenu(){
		hideAllUI ();
		tacticButton.gameObject.SetActive (false);
		
		collapseAllButton.gameObject.SetActive (true);
		houseBuiltMenu.gameObject.SetActive (true);

	}

//The bool is there so that if we click the show map button if the map is on already, we close the map.
	bool showMap;
//show the tactical map
	public void showTacticalMap(){
		if (showMap) {
			hideAllUI();
		}
		else{ 
			showMap = true;				 
			collapseAllButton.gameObject.SetActive (true);
			tacticalMap.gameObject.SetActive (true);
		}
	}
}
