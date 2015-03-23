/*
 * The will be just one menu scene.
 * Other menu like menu will be just this one.
 */
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject gameModesMenu;
	public GameObject gameSetupMenu;

	public static bool playSinglePlayer;

	void Awake(){
		playSinglePlayer = true;
	}

//Functions for the main menus
	public void showGameModes(){
		hideEverything ();
		gameModesMenu.gameObject.SetActive (true);
	}

	public void instantPlay(){
		Debug.Log ("Magic!!!");	
	}

//functions for the game mode menu
	public void playCampaign(){
		Debug.Log ("Under construction!");
	}

	public void playSingleBattle(){
		playSinglePlayer = true;
		hideEverything ();
		gameSetupMenu.gameObject.SetActive (true);
	}

	public void playLocalBattle(){
		playSinglePlayer = false;
		hideEverything ();
		gameSetupMenu.gameObject.SetActive (true);
	}

	public void playOnline(){
		Debug.Log ("Under construction!");
	}

	public void backToMainMenu(){
		hideEverything ();
		mainMenu.gameObject.SetActive (true);
	}
//The functions for the game set up menu is set in its own script
	//This only have the quit function


	public void quitSetup(){
		hideEverything ();
		gameModesMenu.gameObject.SetActive (true);
	}

//general functions that will be used for everything else
	public void hideEverything(){
		mainMenu.gameObject.SetActive (false);
		gameModesMenu.gameObject.SetActive (false);
		gameSetupMenu.gameObject.SetActive (false);
	}
	
	public void moveBackOneLevel(){
		Debug.Log ("Yo");
	}

}
