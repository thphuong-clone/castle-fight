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
	public GameObject constructionMenu;

	public GameObject SPUI;

	public static bool playSinglePlayer;
	
	public GameObject tapToPlay;

	public Text underConstruction;

	void Enable(){
		StartCoroutine (tapTapTap ());
		playSinglePlayer = true;
	}

//Functions for the main menus
	public void showGameModes(){
		hideEverything ();
		gameModesMenu.gameObject.SetActive (true);
	}

	public void instantPlay(){
		//Debug.Log ("Magic!!!");	
		SPUI.SetActive (true);
		this.gameObject.SetActive (false);
	}

//functions for the game mode menu
	public void playCampaign(){
		underConstruction.text = "CAMPAIGN";
		hideEverything ();
		constructionMenu.gameObject.SetActive (true);
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
		underConstruction.text = "ONLINE\nBATTLE";
		hideEverything ();
		constructionMenu.gameObject.SetActive (true);
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
		constructionMenu.gameObject.SetActive (false);
	}
	
	public void moveBackOneLevel(){
		//Debug.Log ("Yo");
	}

	IEnumerator tapTapTap(){
		while (true) {
			tapToPlay.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.75f);
			tapToPlay.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.75f);
		}
	}
}
