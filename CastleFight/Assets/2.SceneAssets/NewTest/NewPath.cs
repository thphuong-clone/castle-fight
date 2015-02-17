using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPath : MonoBehaviour {
/*

	Vector3 startNode;
	Vector3 endNode;
	
	List<Vector3> positionNode = new List<Vector3>();
	
	public GameObject startUnit;
	public GameObject endUnit;

	void Awake(){
		startNode = startUnit.transform.position;
		endNode = endUnit.transform.position;

	}

	void makeRoute(){
		Vector3 v = endNode - startNode;
		bool hit = Physics.Raycast(startNode,v,v.magnitude);
		if (hit) {
			StartCoroutine(subOptimalRoute());		
		}
		//If the path is straight, we just run foward to the damn point.
		else{
			Debug.DrawRay(startNode,v,new Color(1,1,1),2);
			positionNode.Add (startNode);
			positionNode.Add(endNode);
		}

	}

	IEnumerator subOptimalRoute(){
		Vector3 tempNode = startNode;
		for (int i = 0; i < 8; i ++) {
			RaycastHit h;
			positionNode.Add(tempNode);
			Vector3 v = endNode - tempNode;		
			bool hit = Physics.Raycast(startNode,v,out h,v.magnitude);
			if (hit){
				Vector3 hitPosition = h.transform.gameObject.transform.position;
				Vector3 leftHit = hitPosition - new Vector3(1,0,0);
				for (int j = 0 ; j < 8 ; j ++){
					//check if that position has a obstacle or not.
					if (Physics.Raycast(leftHit + new Vector3 (0,0,2),new Vector3(0,0,-2),3)){
							
					}
					else{
					}
				}
				Vector3 rightHit = hitPosition + new Vector3(1,0,0);
			}
			else{
				positionNode.Add(endNode);
				break;
			}
		}
		yield return null;
	}


*/
}
