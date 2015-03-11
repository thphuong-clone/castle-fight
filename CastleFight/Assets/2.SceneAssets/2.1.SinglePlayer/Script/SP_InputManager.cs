/*
 *This class handle the input of player in single player mode. 
*/ using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SP_InputManager : MonoBehaviour {
	public static int controlingState;
	//0 = one unit. 1-2-3-4-5 = swordman,archer, ... group play
	public static Soldier selectedSoldier;

	public Button orderButton;

	EventSystem _eventSystem;
	
	void Awake(){
		_eventSystem = EventSystem.current;

		controlingState = -1;
		selectedSoldier = null;
	}
	
	void Update () {
		//if the player click the mouse (or touch the screen) Unity is sooooo awesome.
		//The point is the detect the input of player to destroy the bomb.
		//#if UNITY_EDITOR		
		if (Input.GetMouseButtonDown(0)){
			bool hitUI = false;
			//if mouse is currently on a UI
			if (_eventSystem.IsPointerOverGameObject()){
				hitUI = true;
			}
			
			RaycastHit hitInfo = new RaycastHit();
			
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			
			if (hit && !hitUI) {
				//Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "SelectedBuilding"){
					SelectedBuilding b  = hitInfo.transform.gameObject.GetComponent<SelectedBuilding>();
					b.SP_isSelected();
				}
				if (hitInfo.transform.gameObject.tag == "SelectedSoldier"){
					SelectedSoldier s = hitInfo.transform.gameObject.GetComponent<SelectedSoldier>();
					s.isSelected();
					controlingState = 0;
					orderButton.gameObject.SetActive(true);
				}
			}
		}
		//#endif
		//touch input
		#if UNITY_ANDROID		
		foreach(Touch touch in Input.touches){
			switch (touch.phase){
			case TouchPhase.Began:
				RaycastHit hitInfo = new RaycastHit();
				bool hitUI = false;
				//if mouse is currently on a UI
				if (_eventSystem.IsPointerOverGameObject(touch.fingerId)){
					hitUI = true;
				}
				
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hitInfo);
				
				if (hit && !hitUI) {
					if (hitInfo.transform.gameObject.tag == "SelectedBuilding"){
						SelectedBuilding b  = hitInfo.transform.gameObject.GetComponent<SelectedBuilding>();
						b.SP_isSelected();
					}				
					if (hitInfo.transform.gameObject.tag == "SelectedSoldier"){
						SelectedSoldier s = hitInfo.transform.gameObject.GetComponent<SelectedSoldier>();
						s.isSelected();
					}

				}
				break;
			default:
				break;
			}
		}
		
		//end of touch input
		#endif
	}
}
