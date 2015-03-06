using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class P2_HouseBuilder : MonoBehaviour {
	//This is the building I want to built
	[SerializeField]//0 = barrack, 1 = wall, 2 = tower.
	int houseToBuild;
	
	[SerializeField]int costOfBarrack;
	[SerializeField]int costOfTower;
	[SerializeField]int costOfWall;

	public P2_Controller masterController;
	
	//This button is used to place the house down.
	public Button buildButton;
	//This button is there in case I don't want to create that building.
	public Button cancleButton;
	
	public Button wallButton;
	public Button barrackButton;
	public Button towerButton;
	
	public Barrack barrackPrefab;
	public Wall wallPrefab;
	public Tower towerPrefab;
	
	public void chooseHouseBuilt(int house){
		houseToBuild = house;
		//deactive all the button
		wallButton.gameObject.SetActive (false);
		barrackButton.gameObject.SetActive (false);
		towerButton.gameObject.SetActive (false);
		//active the cancle button and the build range;
		buildButton.gameObject.SetActive (true);
		cancleButton.gameObject.SetActive (true);
	}
	
	public void buildAHouse(){
		//get the mouse position
		Vector2 position = Vector2.zero;
		float x = Screen.width;
		float y = Screen.height;

#if UNITY_EDITOR
		//enable this line when test on computer
		position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
#endif
		//enable this line for mobile device
#if UNITY_ANDROID 
		foreach (Touch t in Input.touches) {
			if (t.position.y >=0.6f * y){
				position = t.position;
				break;
			}
		}
#endif
		int costToBuild = costOfBarrack;
		
		switch (houseToBuild) {
		case 0:
			costToBuild = costOfBarrack;
			break;
		case 1:
			costToBuild = costOfWall;
			break;
		case 2:
			costToBuild = costOfTower;
			break;
		default:
			break;
		}
		if (ResourceSystem.p1_gold < costToBuild) {
			cancleBuilding();
			return;		
		}

		//The mouse position range from 0,0 to screen width and screen height
		position = new Vector2(position.x / x,position.y / y);//it is now range from 0 ,0 ~ 0.6, 0.9
		//translate it into real world position
		position += new Vector2 (-0.5f, -0.5f); 
		position = new Vector2 (position.x * 9f, position.y * 16);
		//round it up
		
		//select house to build
		Building b = barrackPrefab;
		switch (houseToBuild) {
		case 0:
			b = barrackPrefab;			
			PlayerController.p2_buildingList[1].Add(b);
			break;
		case 1:
			b = wallPrefab;
			PlayerController.p2_buildingList[3].Add(b);
			break;
		case 2:
			b = towerPrefab;
			PlayerController.p2_buildingList[2].Add(b);
			break;
		default:
			break;
		}
		//build 
		ResourceSystem.p1_gold -= costToBuild;
		Building build = (Building)Instantiate ((Building)b);
		build.isPlayerOne = false;
		build.transform.rotation = Quaternion.Euler (new Vector3(0,0,180));
		build.unitAura.GetComponent<SpriteRenderer> ().color = new Color(0,0,1,0.58823529411f);

		//Round up the building position, so that the building will always be built on a round position
		float modX = Mathf.Abs(position.x) % 1;
		float modY = Mathf.Abs (position.y) % 1;
		if (modX < 0.5f){}
		else{
			if (position.x > 0)
				position.x ++;
			else
				position.x --;
		}		
		if (modY < 0.5f) {}
		else{
			if (position.y > 0)
				position.y ++;
			else
				position.y --;
		}


		build.transform.position = new Vector3 ((int)position.x,(int)position.y,0);
		//congratulations, you have a building!
		cancleBuilding ();
	}
	
	public void cancleBuilding(){
		//active the button, again.
		wallButton.gameObject.SetActive (true);
		barrackButton.gameObject.SetActive (true);
		towerButton.gameObject.SetActive (true);
		//deactive the other button, err
		buildButton.gameObject.SetActive (false);
		cancleButton.gameObject.SetActive (false);
		//hide everythig
		masterController.hideAllUI ();
	}
	

}
