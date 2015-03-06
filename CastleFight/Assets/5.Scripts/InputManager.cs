/*
*This class handles the input (touch) from the player
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	EventSystem _eventSystem;

	void Awake(){
		_eventSystem = EventSystem.current;
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
				//				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "SelectedBuilding"){
					SelectedBuilding b  = hitInfo.transform.gameObject.GetComponent<SelectedBuilding>();
					b.isSeleted();
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
						b.isSeleted();
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
