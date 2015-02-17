using UnityEngine;
using System.Collections;

public class POneController : MonoBehaviour {
/*


	private float fingerStartTime = 0;
	private Vector2 fingerStartPos = Vector2.zero;
	
	private bool isSwipe= false;
	private float minSwipeDist = 50;
	private float maxSwipeTime = 0.5f;
	
	
	//check for swipe left and right, just make smoother movement, don't need to look at this code too much yo
	void Update(){
		if (Input.GetMouseButtonDown(0))		{
			RaycastHit hitInfo = new RaycastHit();
			
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			
//			if (hit) {
//				//				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
//				if (hitInfo.transform.gameObject.tag == "Bomb"){
//					ParentBomb bomb  = hitInfo.transform.gameObject.GetComponent<ParentBomb>();
//					bomb.isClicked();
//				}
//				if (hitInfo.transform.gameObject.tag == "BoardTitle"){
//					BoardTitle title = hitInfo.transform.gameObject.GetComponent<BoardTitle>();
//					title.isSelected();					
//				}
//			}
		} 

		foreach(Touch touch in Input.touches)
		{
			switch (touch.phase){
			case TouchPhase.Began:
				RaycastHit hitInfo = new RaycastHit();
				
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hitInfo);
//				
//				if (hit) {
//					//				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
//					if (hitInfo.transform.gameObject.tag == "Bomb"){
//						ParentBomb bomb  = hitInfo.transform.gameObject.GetComponent<ParentBomb>();
//						bomb.isClicked();
//					}
//					if (hitInfo.transform.gameObject.tag == "BoardTitle"){
//						BoardTitle title = hitInfo.transform.gameObject.GetComponent<BoardTitle>();
//						title.isSelected();					
//					}
//				}

				break;
			default:
				Debug.Log("ERROR");
				break;
			}
		}
	}


*/
}
