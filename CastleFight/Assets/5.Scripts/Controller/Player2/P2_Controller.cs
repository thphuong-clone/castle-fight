/*
This function handle the input of the player 1.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class P2_Controller : MonoBehaviour {
	public GameObject soldierMenu;
	public GameObject collapseAllButton;
	
	
	public GameObject tacticButton;
	public GameObject tacticalMap;
	
	public GameObject buyButton;
	public GameObject buyMenu;
	public GameObject soldierBuiltMenu;
	public GameObject houseBuiltMenu;
	
	public GameObject moneyBackGround;
	public Text playerMoney;
	
	bool isIdle;
	float idleTime;
	
	void Awake(){
		isIdle = true;
		idleTime = 0;
		
		ResourceSystem.p2_gold = 160;
		showMap = false;
		hideAllUI ();
		hideMajorButton ();
		StartCoroutine (updateGold());
	}
	
	
	void Update(){
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)){
			if (Input.mousePosition.y > Screen.height / 2){
				if (isIdle)
					hideAllUI();
				isIdle = false;
				idleTime = 3;
			}
		}
		#endif
		if (!isIdle) {
			idleTime -= Time.deltaTime;
			if (idleTime <= 0){
				isIdle = true;
				hideAllUI();
				hideMajorButton();
			}
		}
	}
	
	void Start(){
		playerMoney.text = ResourceSystem.p2_gold.ToString();
	}
	
	//hide all the UI element, if player cancel.
	public void hideAllUI(){
		collapseAllButton.gameObject.SetActive (false);
		soldierMenu.gameObject.SetActive (false);
		tacticalMap.gameObject.SetActive (false);
		houseBuiltMenu.gameObject.SetActive (false);
		buyButton.gameObject.SetActive (false);
		buyMenu.gameObject.SetActive (false);
		soldierBuiltMenu.gameObject.SetActive (false);
		
		
		tacticButton.gameObject.SetActive (true);
		buyButton.gameObject.SetActive (true);
		moneyBackGround.gameObject.SetActive (true);
		playerMoney.gameObject.SetActive (true);
		if (showMap)
			showMap = !showMap;
	}
	
	//hide all the first layer button
	public void hideMajorButton(){
		tacticButton.gameObject.SetActive (false);
		buyButton.gameObject.SetActive (false);
		moneyBackGround.gameObject.SetActive (false);
		playerMoney.gameObject.SetActive (false);
	}
	
	//Show the interface of the barrack building
	public void showSoldierBuilt(){
		hideAllUI ();
		hideMajorButton ();
		
		collapseAllButton.gameObject.SetActive (true);
		soldierMenu.gameObject.SetActive (true);
		
	}
	
	//show the buy menu, which has two things: buildings or soldiers
	public void showBuyMenu(){
		hideAllUI ();
		hideMajorButton ();
		
		collapseAllButton.gameObject.SetActive (true);
		buyMenu.gameObject.SetActive (true);
	}
	
	//Show the menu which unit that can be bought from the castle.
	public void showSoldierToBuyMenu(){
		hideAllUI ();
		hideMajorButton ();
		
		collapseAllButton.gameObject.SetActive (true);
		soldierBuiltMenu.gameObject.SetActive (true);
	}
	
	//Show the list of possible buy building
	public void showHouseBuiltMenu(){
		hideAllUI ();
		hideMajorButton ();
		
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
			hideAllUI();
			hideMajorButton();
			
			showMap = true;				 
			collapseAllButton.gameObject.SetActive (true);
			tacticalMap.gameObject.SetActive (true);
		}
	}
	
	
	IEnumerator updateGold(){
		while (true) {
			playerMoney.text = ResourceSystem.p2_gold.ToString();		
			yield return new WaitForSeconds(0.5f);
		}
	}
}
