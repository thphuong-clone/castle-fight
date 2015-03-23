/**************************
 * 
 * This class handles the game setup menu
 * How to set up the game, like map, mode and such...
 * 	
 **************************/ 
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : MonoBehaviour {
	public Button castleFight;
	public Button kingOfTheHill;

	public GameObject fightingMaps;
	public GameObject survivalMaps;

	public Text annoucingText;
	public Text gameInformation;
	public Text mapInformation;

	[SerializeField]List<Button> fightMapList = new List<Button>();
	[SerializeField]List<Button> survivalMapList = new List<Button>();

	int mapToPlay;
	bool isCastleFight;

	void OnEnable(){
		castleFightButton ();
		setUpFightMap (1);
		if (MainMenuController.playSinglePlayer)
			annoucingText.text = "SINGLE\nBATTLE";
		else
			annoucingText.text = "LOCAL\nBATTLE";		
		
	}

	public void castleFightButton(){
		castleFight.GetComponent<Button> ().image.color = new Color (1,0,0,0.49019607843f);
		kingOfTheHill.GetComponent<Button> ().image.color = new Color (0,0,0,0.49019607843f);
		isCastleFight = true;
		gameInformation.text = "MODE: CASTLE FIGHT";
		fightingMaps.gameObject.SetActive (true);
		survivalMaps.gameObject.SetActive (false);
	}

	public void kingOfTheHillButton(){
		castleFight.GetComponent<Button> ().image.color = new Color (0,0,0,0.49019607843f);
		kingOfTheHill.GetComponent<Button> ().image.color = new Color (0,0,1,0.49019607843f);
		isCastleFight = false;
		gameInformation.text = "MODE: KING OF THE HILL";
		fightingMaps.gameObject.SetActive (false);
		survivalMaps.gameObject.SetActive (true);
	}

	//press this button to play
	public void playButton(){
		//lots more stuff to come ....
		
		
		if (MainMenuController.playSinglePlayer) {
			Application.LoadLevel("2.1.SinglePlayerScene");	
		}
		else{
			Application.LoadLevel("2.2.MultiplayerScene");
		}
	}
	
	public void setUpFightMap(int map){
		mapToPlay = map;
		foreach(Button b in fightMapList){
			b.GetComponent<Image>().color =  new Color (0.7f,0.7f,0.7f,0.29411764705f);
		}
		fightMapList[map - 1].GetComponent<Image>().color =  new Color (1,1,1,0.60784313725f);

		switch (map) {
		case 1:
			mapInformation.text = "MAP: THE VALLEY";
			break;
		case 2:
			mapInformation.text = "MAP: THE MAZE";
			break;
		case 3:
			mapInformation.text = "MAP: TWO RIVERS";
			break;
		case 4:
			mapInformation.text = "MAP: AUTUMN LEAVES";
			break;
		default :
			Debug.LogError("HOW ?");
			break;
		}
	}

	public void setUpSurvivalMap(int map){
		mapToPlay = map;
		foreach(Button b in survivalMapList){
			b.GetComponent<Image>().color =  new Color (0.7f,0.7f,0.7f,0.29411764705f);
		}
		survivalMapList[map - 1].GetComponent<Image>().color =  new Color (1,1,1,0.60784313725f);
		if (map == 1) {
			mapInformation.text = "MAP: THE VALLEY";
		}
		else{
			mapInformation.text = "MAP: AUTUMN LEAVES";
		}

	}
}
