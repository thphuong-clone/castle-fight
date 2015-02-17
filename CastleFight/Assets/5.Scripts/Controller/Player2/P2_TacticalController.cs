/*
 * Player 2 can order unit in this UI.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class P2_TacticalController : MonoBehaviour {
	//public PlayerController mainController;
	public Button orderButton;
	
	public List<Button> soldierButtons = new List<Button>();
	
	//This is the current unit taking the order, be it horseman, or archer ....
	[SerializeField]
	int currentControlledUnit;
	
	//The attacking order or the moving order 
	//during moving, the unit will ignore all enemy on its path
	[SerializeField]
	bool isAttackingOrder;
	
	void Awake(){
		isAttackingOrder = true;
		orderButton.image.color = Color.red;
		
	}
	

	//This function change the currently controlled unit (swordman, archer, horseman ... )
	//This function is called by clicking at the approtiate unit button on the tactical map
	public void changeControlledUnit(int unit){
		currentControlledUnit = unit;
		
		foreach(Button b in soldierButtons)
			b.image.color = Color.white;
		
		soldierButtons [unit - 1].image.color = orderButton.image.color;
	}
	
	//This function is called to change the current type of order, moving or attacking.
	public void changeCurrentControllerOrder(){
		isAttackingOrder = !isAttackingOrder;
		if (isAttackingOrder)
			orderButton.image.color = Color.red;
		else
			orderButton.image.color = Color.green;
		if (currentControlledUnit != 0)
			soldierButtons [currentControlledUnit - 1].image.color = orderButton.image.color;
	}
	
	//This function is called if player touch a position on the map.
	public void orderUnit(){
		//if player haven't chosen any type of unit yet.
		if (currentControlledUnit == 0) {
			//do nothing.
			Debug.Log("No unit");
		}
		else{
			Vector2 position = Vector2.zero;
			float x = Screen.width;
			float y = Screen.height;

#if UNITY_EDITOR
			//Delete if build for mobile
			position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
#endif
			//enabl for mobile input
			foreach (Touch t in Input.touches) {
				if (t.position.x > 0.25f * x && t.position.x < 0.75f* x){
					if (t.position.y > 0.5f * y){
						position = t.position;
						break;
					}
				}
			}

			//The mouse position range from 0,0 to screen width and screen height
			position = new Vector2(position.x / x,position.y / y);
			position += new Vector2(-0.5f,-0.75f);
			position*= 4;			
			//translate the position onto the real world script
			position = new Vector2(position.x * 4.5f,position.y * 8);
			
			//order the unit to do its approtiate action
			int orderAction = 0;
			if (isAttackingOrder)
				orderAction =2;
			else
				orderAction = 1;
			PlayerController.p2_soldierOrder[currentControlledUnit - 1] = new Vector3(position.x,position.y,orderAction);
			
			PlayerController.orderSoldier();
		}
	}

}
