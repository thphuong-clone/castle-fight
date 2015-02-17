using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {
	Vector3 startNode;
	Vector3 endNode;

	List<Vector3> positionNode = new List<Vector3>();

	public GameObject startUnit;
	public GameObject endUnit;

	Vector3 newPos;
//	bool noPath;
	//raycast test
	void Awake(){
		startNode = startUnit.transform.position;
		endNode = endUnit.transform.position;
//
//		positionNode.Add (startNode);
//		noPath = false;
//		makeRoute (startNode);
//		if (!noPath){
//			StartCoroutine (drawRoute ());
//		}
//		else{
//			Debug.Log("YOU SHALL NOT PASS");
//		}
		raycastTest ();
	}

	void makeRoute(Vector3 s){
		//Cast the ray to see if the path is clear or not 
		RaycastHit h;
		Vector3 v = endNode - s;
		bool hit = Physics.Raycast(s,v,out h,v.magnitude);
		Debug.DrawRay(s,v,new Color(1,1,1),2);
		if (hit) {
			Vector3 breakPoint = h.transform.gameObject.transform.position;		
			Vector3 temp = getPosition(breakPoint);
			if (temp == Vector3.zero){
				//noPath = true;
			}
			else{
				positionNode.Add(temp);
				Vector3 t = temp + new Vector3(0,1,0);
				positionNode.Add(t);
				makeRoute(t);
			}
		}
		else{
			positionNode.Add(endNode);
		}
	}

	Vector3 getPosition(Vector3 vector){
		Vector3 left = vector - new Vector3 (1, 0, 0);
		Vector3 right = vector + new Vector3 (1, 0, 0);
		
		int leftHit = -1;
		int rightHit = -1;

		for (int i  = 0;i < 8 ; i ++) {
			//left vector
			Vector3 l = left - positionNode[positionNode.Count-1];
			if (Physics.Raycast(positionNode[positionNode.Count-1],l,l.magnitude)){
				//not the path
				leftHit --;
			}
			else{
				leftHit = -leftHit;
			}
			//Debug.DrawRay(startPos,l,new Color(1,1,1),2);
			
			//right vector
			Vector3 r = right - positionNode[positionNode.Count-1];
			if (Physics.Raycast(positionNode[positionNode.Count-1],r,r.magnitude)){
				rightHit --;
			}
			else{
				rightHit = -rightHit;			
			}
			//Debug.DrawRay(startPos,r,new Color(1,1,1),2);
			
			if (leftHit > 0 || rightHit > 0){
				break;
			}
			else{
				left -= new Vector3(1,0,0);
				right += new Vector3(1,0,0);
			}
			
		}

		if (leftHit > 0 && rightHit > 0) {
			if (UnityEngine.Random.Range(0,2) == 0){
				return left;
			}
			else{
				return right;
			}
		}
		else{
			if (leftHit > 0) {
				return left;
//				Vector3 l =endPos - left;
//				Debug.DrawRay(left,l,new Color(1,1,1),2);
			}
			if (rightHit > 0) {
				return right;
//				Vector3 r = endPos - right;
//				Debug.DrawRay(right,r,new Color(1,1,1),2);
			}
			//no path
			return Vector3.zero;
		}
	}

//DRAW ROUTE
	IEnumerator drawRoute(){
		for (int i = 0; i < positionNode.Count - 1; i ++) {
			yield return new WaitForSeconds(1f);
			Vector3 v = positionNode[i+1] - positionNode[i];
			Debug.DrawRay(positionNode[i],v,new Color(1,1,1,1),5);
		}
	}

//OLD CODE


	void raycastTest(){
		RaycastHit h;
		Vector3 v = endNode - startNode;
		bool hit = Physics.Raycast(startNode,v,out h,v.magnitude);
		Debug.DrawRay(startNode,v,new Color(1,1,1),2);
		if (hit ){
			newPos = h.transform.gameObject.transform.position;
			//h.transform.gameObject.SetActive(false);
			StartCoroutine(findPath ());
			Debug.Log("hit");
		}
		else{
			Debug.Log("nope");
		}
		//Ray r = new Ray(new Vector3(0,0,0),new Vector3(9,9,0));
	}

	IEnumerator findPath(){
		Vector3 left = newPos - new Vector3 (1, 0, 0);
		Vector3 right = newPos + new Vector3 (1, 0, 0);

		int leftHit = -1;
		int rightHit = -1;

		yield return new WaitForSeconds (1.5f);

		for (int i  = 0;i < 8 ; i ++) {
			//left vector
			Vector3 l = left - startNode;
			if (Physics.Raycast(startNode,l,l.magnitude)){
				//not the path
				leftHit --;
			}
			else{
				leftHit = -leftHit;
			}
			Debug.DrawRay(startNode,l,new Color(1,1,1),2);

			//right vector
			Vector3 r = right - startNode;
			if (Physics.Raycast(startNode,r,r.magnitude)){
				rightHit --;
			}
			else{
				rightHit = -rightHit;			
			}
			Debug.DrawRay(startNode,r,new Color(1,1,1),2);

			if (leftHit > 0 || rightHit > 0){
				break;
			}
			else{
				left -= new Vector3(1,0,0);
				right += new Vector3(1,0,0);
			}

			yield return new WaitForSeconds(1.5f);
		}
		if (leftHit > 0) {
			Vector3 l =endNode - left;
			Debug.DrawRay(left,l,new Color(1,1,1),2);
		}
		if (rightHit > 0) {
			Vector3 r = endNode - right;
			Debug.DrawRay(right,r,new Color(1,1,1),2);
		}


	}


}
