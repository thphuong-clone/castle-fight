/*
 *This class handle the input of player in single player mode. 
*/ using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PathFinder;
using System;

public class SPInputManager : MonoBehaviour {
    Vector2 one;
    Vector2 two;
    GameObject go;
    LineRenderer line;

    bool dragging;
    bool draggingCancelled = true;
    bool displayLine;
	public static int controlingState;
	//0 = one unit. 1-2-3-4-5 = swordman,archer, ... group play
	public static Soldier selectedSoldier;
    Vector3 oldPos;

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

            RaycastHit2D hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit2d.collider != null)
            {
                draggingCancelled = false;
                displayLine = false;
                //two = Input.mousePosition;
                //two.y = Screen.height - two.y;
                go = hit2d.transform.gameObject;

                //Debug.Log("Hit " + go.name);
                if (hit && !hitUI && hitInfo.transform.gameObject.tag == "SelectedBuilding")
                {
                    SelectedBuilding b = hitInfo.transform.gameObject.GetComponent<SelectedBuilding>();
                    b.SP_isSelected();
                }
                if (hit2d.transform.gameObject.tag == "Soldier")
                {
                    SelectedSoldier s = hit2d.transform.gameObject.GetComponent<SelectedSoldier>();
                    //Debug.Log(s == null);
                    s.isSelected();
                    controlingState = 0;
                    orderButton.gameObject.SetActive(true);
                }
            }
			
            //if (hit && !hitUI) {
            //    draggingCancelled = false;
            //    displayLine = false;
            //    //two = Input.mousePosition;
            //    //two.y = Screen.height - two.y;
            //    go = hitInfo.transform.parent.gameObject;

            //    //Debug.Log("Hit " + go.name);
            //    if (hitInfo.transform.gameObject.tag == "SelectedBuilding"){
            //        SelectedBuilding b  = hitInfo.transform.gameObject.GetComponent<SelectedBuilding>();
            //        b.SP_isSelected();
            //    }
            //    if (hitInfo.transform.gameObject.tag == "SelectedSoldier"){
            //        SelectedSoldier s = hitInfo.transform.gameObject.GetComponent<SelectedSoldier>();
            //        s.isSelected();
            //        controlingState = 0;
            //        orderButton.gameObject.SetActive(true);
            //    }
            //}
            oldPos = Input.mousePosition;
		}

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(oldPos);
            //Debug.Log(Input.mousePosition);
            draggingCancelled = true;
            
            if ((Math.Abs(Input.mousePosition.x - oldPos.x) >  1 || Math.Abs(Input.mousePosition.y - oldPos.y) > 1) && selectedSoldier != null)
            {
                //Debug.Log("weeeeeee");
                //Debug.Log(selectedSoldier == null);

                Vector2 mousePos = Input.mousePosition;

                go = selectedSoldier.gameObject;

                Vector2 startPos = Camera.main.ScreenToWorldPoint(oldPos);
                Vector2 endPos = Camera.main.ScreenToWorldPoint(mousePos);

                two = endPos;

                selectedSoldier.destinatedPos = endPos;

                Position2D start = GridMapUtils.GetTile(startPos.x, startPos.y);
                Position2D end = GridMapUtils.GetTile(endPos.x, endPos.y);

                //Debug.Log(start);
                //Debug.Log(end);

                selectedSoldier.nextPathNode = PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end);
                //Debug.Log(PathFinder.PathFinder.FindPath(PlayerController.knownWorld, start, end));
                selectedSoldier = null;
                controlingState = -1;
                displayLine = true;
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
            case TouchPhase.Moved:
                dragging = true;
                break;
            case TouchPhase.Ended:
                if (dragging)
                {
                    Vector2 endPos = touch.position;
                }
                break;
			default:
				break;
			}
		}
		
		//end of touch input
		#endif

        if (displayLine && draggingCancelled)
        {
            one = go.transform.position;
            line.SetPosition(0, one);
            line.SetPosition(1, two);
            line.enabled = true;
            StartCoroutine(HideLine());
        }
        else if (!draggingCancelled && !displayLine)
        {
            one = go.transform.position;
            two = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, one);
            line.SetPosition(1, two);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
	}

    IEnumerator HideLine()
    {
        yield return new WaitForSeconds(1);
        displayLine = false;
    }

}
