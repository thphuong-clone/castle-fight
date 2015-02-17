using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class P2_BarrackController : MonoBehaviour {
	public static Barrack selectedBarrack;
	
	public static bool barrackChanged;
	
	[SerializeField]
	float currentCountDown;
	
	[SerializeField]
	float countdownRatio;
	
	public List<Image> soldierCDBar = new List<Image>();
	
	[SerializeField]
	Animator imageAnimator;
	
	void Awake(){
		barrackChanged = false;
		//StartCoroutine (checkForInput());
		theCase = 1;//check for input
	}
	
	int theCase;
	//1 = check for input, 2 = update cooldown
	void Update(){
		switch (theCase) {
		case 1:
			if (barrackChanged){
				theCase = 2;
			}
			break; 
		case 2:
			if (barrackChanged) {
				barrackChanged = false;
				//update the button countdown
				updateBuildingBar();
			}
			countdownRatio = selectedBarrack.buildCountDown /currentCountDown * 3f;
			imageAnimator.SetFloat ("CountDown",countdownRatio);
			break;
		default:
			break;
		}
	}
	
	//update the cool down to the approtiate barrack
	void updateBuildingBar(){
		hideAllColddown ();
		soldierCDBar[selectedBarrack.currentSoldierBuilt - 1].gameObject.SetActive(true);
		currentCountDown = selectedBarrack.buildCost[selectedBarrack.currentSoldierBuilt -1];
		imageAnimator = soldierCDBar [selectedBarrack.currentSoldierBuilt -1].GetComponent<Animator> ();	
	}
	
	//1 - swordman, 2 = archer, 3 = horseman, 4 = gladiator, 5 = cannon
	public void changeCurrentSoldierBuilt(int soldier){
		//make the selected barrack build my soldier
		selectedBarrack.currentSoldierBuilt = soldier;
		updateBuildingBar ();
	}
	
	public static void changeBarrack(Barrack b){
		selectedBarrack = b;
		barrackChanged = true;
	}
	
	//Hide all the countdown button.
	void hideAllColddown(){
		foreach(Image i in soldierCDBar){
			i.gameObject.SetActive(false);
		}
	}

}
