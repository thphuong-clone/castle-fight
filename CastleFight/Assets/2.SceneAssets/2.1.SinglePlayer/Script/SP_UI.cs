/*
 * This class handle the user interface for the Singple player
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PathFinder;

public class SP_UI : MonoBehaviour {
	[SerializeField]int controlledSoldierState;
	//this button get the position of the player pressed
	public Button unitCommandButton;

	//This button to cancle everything
	public Button cancleEverythingButton;
	//the buy menu, contains 
	public GameObject buyMenu;

	public GameObject houseBuiltMenu;

	public GameObject soldierBuiltMenu;

	public GameObject barrackBuyMenu;

	public GameObject tacticalMenu;

	//The displayer menu
	public GameObject displayMenu;
	//The money of the player
	public Text playerMoney; 
	//this button change the oder type, attack or move only
	public Button orderButton;

	void Awake(){
		controlledSoldierState = 2;
		orderButton.image.color = new Color (1,0,0,0.6078f);
		ResourceSystem.p1_gold = 160;
		StartCoroutine (updateGold ());
	}

	IEnumerator updateGold(){
		while (true) {
			yield return new WaitForSeconds(0.25f);
			playerMoney.text = ResourceSystem.p1_gold.ToString();
		}
	}

	//This function order a single unit or a lot to move to a position on map
	public void orderUnit(){
		Vector2 orderedPosition = getCommandPosition ();
		//controlling a single UNIT
		if (SP_InputManager.controlingState == 0) {
			SP_InputManager.selectedSoldier.destinatedPos = orderedPosition;
			SP_InputManager.selectedSoldier.soldierState = controlledSoldierState;
			
			Position2D p1End = GridMapUtils.GetTile(orderedPosition.x,orderedPosition.y);

			Position2D start = GridMapUtils.GetTile(SP_InputManager.selectedSoldier.transform.position.x,SP_InputManager.selectedSoldier.transform.position.y);
			SP_InputManager.selectedSoldier.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, p1End);

			SP_InputManager.selectedSoldier.gameObject.GetComponent<SpriteRenderer>().color = new Color (1,0,0,0.60784313725f);
			SP_InputManager.selectedSoldier = null;

		}
		//controling group of unit, like all soldiers, all archer ....
		else{
		}
		unitCommandButton.gameObject.SetActive (false);
	}

	Vector2 getCommandPosition(){
		float x = Input.mousePosition.x / (float)Screen.width - 0.5f;
		float y = Input.mousePosition.y / (float)Screen.height - 0.5f;
		x *= 9f;
		y *= 16f;
		return new Vector2 (x, y);
	}

	//this function hide all the major UI, drop it back to the highest level (the main level)
	public void hideAllUI(){
		cancleEverythingButton.gameObject.SetActive (false);
		buyMenu.gameObject.SetActive (false);
		soldierBuiltMenu.gameObject.SetActive (false);
		barrackBuyMenu.gameObject.SetActive (false);
		unitCommandButton.gameObject.SetActive (false);
		tacticalMenu.gameObject.SetActive (false);
		houseBuiltMenu.gameObject.SetActive (false);
		//deactive all the other stuffs
		//leave it active though
		displayMenu.gameObject.SetActive (true);
	}

	//hide the top level UI
	public void hideMajorUI(){
		displayMenu.gameObject.SetActive (false);
	}

	//change the current order state, from move only to attak and reverse
	public void changeCurrentOrder(){
		if (controlledSoldierState == 1) {
			controlledSoldierState = 2;		
			orderButton.image.color = new Color (1,0,0,0.60784313725f);
		}
		else{
			controlledSoldierState = 1;
			orderButton.image.color = new Color (0,1,0,0.60784313725f);
		}
	}

	//Open the buy menu. - they player can buy House and Units here.
	public void openBuyMenu(){
		hideAllUI ();
		hideMajorUI ();
		//show up the buy menu here.
		cancleEverythingButton.gameObject.SetActive (true);
		buyMenu.gameObject.SetActive (true);
	}

	//show the barrack built, when you tap on a barrack.
	public void showBarrackBuiltMenu(){
		hideAllUI ();
		hideMajorUI ();
		cancleEverythingButton.gameObject.SetActive (true);
		barrackBuyMenu.gameObject.SetActive (true);
	}

	//show the single player order. when you want to order all kind of unit
	public void showUnitSelection(){
		hideAllUI ();
		hideMajorUI ();
		cancleEverythingButton.gameObject.SetActive (true);
		tacticalMenu.gameObject.SetActive (true);
	}

	//show the buy able soldier from the castle
	public void showSoldierBuiltMenu(){
		hideAllUI ();
		hideMajorUI ();
		cancleEverythingButton.gameObject.SetActive (true);

		soldierBuiltMenu.gameObject.SetActive (true);
	}

	//show the buyable building from the castle
	public void showHouseBuiltMenu(){		
		hideAllUI ();
		hideMajorUI ();
		cancleEverythingButton.gameObject.SetActive (true);
		houseBuiltMenu.gameObject.SetActive (true);
	}
}
