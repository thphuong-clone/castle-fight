/*
 *This class handle the input of player in single player mode. 
*/ 
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PathFinder;

public class SP_InputManager : MonoBehaviour {
    Vector2 one;
    Vector2 two;
    GameObject go;
    LineRenderer line;
    Vector3 oldPos;

    bool dragging;
    bool draggingCancelled = true;
    bool displayLine;

	public static int controlingState;
	//-1 = one unit. 0 - 4 = swordman,archer, ... group play
	public static Soldier selectedSoldier;

    public Button orderButton;

	EventSystem _eventSystem;
	
	void Awake(){
		_eventSystem = EventSystem.current;
        line = gameObject.GetComponent<LineRenderer>();

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
                draggingCancelled = false;
                displayLine = false;
                go = hitInfo.transform.parent.gameObject;

				//Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "SelectedBuilding"){
					SelectedBuilding b  = hitInfo.transform.gameObject.GetComponent<SelectedBuilding>();
					b.SP_isSelected();
				}
				if (hitInfo.transform.gameObject.tag == "SelectedSoldier"){
					if (!hitInfo.transform.gameObject.GetComponent<SelectedSoldier>().parentSoldier.isPlayerOne)
						return;
					
					SelectedSoldier s = hitInfo.transform.gameObject.GetComponent<SelectedSoldier>();
					s.isSelected();
					controlingState = -1;
					orderButton.gameObject.SetActive(true);
				}
			}
            //else
            //{
            //    Debug.Log("miss");
            //    Debug.Log("hit is: " + hit);
            //    Debug.Log("hit ui is: " + hitUI);
            //}

            oldPos = Input.mousePosition;


		}


        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(oldPos);
            //Debug.Log(Input.mousePosition);
            draggingCancelled = true;

            if ((Math.Abs(Input.mousePosition.x - oldPos.x) > 1 || Math.Abs(Input.mousePosition.y - oldPos.y) > 1) && selectedSoldier != null)
            {
                //Debug.Log("weeeeeee");
                //Debug.Log(selectedSoldier == null);

                Vector2 mousePos = Input.mousePosition;

                go = selectedSoldier.gameObject;

                Vector2 endPos = Camera.main.ScreenToWorldPoint(mousePos);

                two = endPos;

                selectedSoldier.destinatedPos = endPos;

                selectedSoldier.Deploy(endPos, SP_UI.controlledSoldierState);
                selectedSoldier.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.60784313725f);


                //Debug.Log(start);
                //Debug.Log(end);

                //selectedSoldier.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
                //Debug.Log(PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end));
                selectedSoldier = null;
                controlingState = -1;
                displayLine = true;
                orderButton.gameObject.SetActive(false);
                StopCoroutine("HideLine");
            }

        }

        if (displayLine)
        {
            one = go.transform.position;
            line.SetPosition(0, one);
            line.SetPosition(1, two);
            line.enabled = true;
            StartCoroutine("HideLine");
        }
        else if (!draggingCancelled)
        {
            //Debug.Log("weeeeee");
            one = go.transform.position;
            two = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, one);
            line.SetPosition(1, two);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
            //Debug.Log("line: " + displayLine);
            //Debug.Log("dragging: " + !draggingCancelled);
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

    IEnumerator HideLine()
    {
        //Debug.Log("hey");
        yield return new WaitForSeconds(1);
        displayLine = false;
        //Debug.Log("line: " + displayLine);
        //Debug.Log("dragging: " + draggingCancelled);
    }
}
