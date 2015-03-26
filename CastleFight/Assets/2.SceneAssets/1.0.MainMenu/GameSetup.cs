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
		//if the player play the survival maps
		if (!isCastleFight) {
			if (!MainMenuController.playSinglePlayer){
				switch(mapToPlay){
				case 1:
					//load the valley multiplayer map
					break;
				case 2:
					//load four rivers multiplayer map
					break;
				default:
					break;				
				}
			}
			else{
				switch(mapToPlay){
				case 1:
					//load the valley single player map
					break;
				case 2:
					//load four rivers single player map
					break;
				default:
					break;				
				}
			}
			return;	
		}

		//if play multi player
		if (!MainMenuController.playSinglePlayer) {
			switch(mapToPlay){
			case 1: //The valley map
				Application.LoadLevel("2.2.1.TheValleyMultiplayerMap");
				break;
			case 2://the maze map
				Application.LoadLevel("2.2.0.MazeMultiplayer");
				break;
			case 3://two rivers
				Application.LoadLevel("2.2.3.TwoRiversMultiplayerMap");
				break;
			case 4://four rivers
				Application.LoadLevel("2.2.2.FourRiversMultiplayerMap");
				break;
			default:
				Debug.Log("Error loading multiplayer map. I don't know why, honestly");
				break;
			}
		}
		else{//if not ....
			switch(mapToPlay){
			case 1: //The valley map
				Application.LoadLevel("2.1.1.TheValleySinglePlayerMap");
				break;
			case 2://the maze map
				Application.LoadLevel("2.1.0.MazeSinglePlayer");
				break;
			case 3://two rivers
				Application.LoadLevel("2.1.2.TwoRiversSinglePlayerMap");
				break;
			case 4://four rivers
				Application.LoadLevel("2.1.3.FourRiversSinglePlayerMap");
				break;
			default:
				Debug.Log("Error loading multiplayer map. I don't know why, honestly");
				break;
			}

		}
	}
	
	public void setUpFightMap(int map){
		mapToPlay = map;
		foreach(Button b in fightMapList){
			b.GetComponent<Image>().color =  new Color (0.7f,0.7f,0.7f,0.29411764705f);
		}
		fightMapList[map - 1].GetComponent<Image>().color =  new Color (1,1,1,0.8f);

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
		survivalMapList[map - 1].GetComponent<Image>().color =  new Color (1,1,1,0.8f);
		if (map == 1) {
			mapInformation.text = "MAP: THE VALLEY";
		}
		else{
			mapInformation.text = "MAP: AUTUMN LEAVES";
		}

	}


}
