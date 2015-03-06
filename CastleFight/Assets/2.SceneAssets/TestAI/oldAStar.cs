using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class oldAStar : MonoBehaviour {
	public int[,] board = new int[9,16];
	public Vector3 startPos;
	public Vector3 endPos;

	public GameObject cube;

	void Awake(){
		for (int i = 0; i < 9 ; i ++)
			for(int j = 0; j < 16;j++)
				board[i,j] = 0;

		for (int i = 2; i < 7; i ++) {
			board[i,6] = 1;		
			board[i,9] = 1;		

		}



		cube.gameObject.SetActive (false);

		for (int i = 0; i < 9 ; i ++){
			for(int j = 0; j < 16;j++){
				if (board[i,j] == 1){
					GameObject superCub = (GameObject)Instantiate(cube);
					superCub.transform.position = new Vector3(i,j,0);
					superCub.gameObject.SetActive(true);
				}
			}
		}

		searchRoute ();

	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Return)){
			RaycastHit h;
			Vector3 v = endPos - startPos;
			bool hit = Physics.Raycast(startPos,endPos,out h,v.magnitude,3);
			if (hit ){
				h.transform.gameObject.SetActive(false);
				Debug.Log("hit");
			}
			else{
				Debug.Log("nope");
			}
			Debug.DrawRay(startPos,endPos,new Color(1,1,1),2);
			//Ray r = new Ray(new Vector3(0,0,0),new Vector3(9,9,0));
		}
	}
	
	void searchRoute(){
		//Open node
		List<Node> openNode = new List<Node> ();
		//clode node
		List<Node> closeNode = new List<Node> ();
		//All of the node.
		Node[,] boardNode = new Node[9, 16];



		for (int i = 0; i < 9 ; i ++){
			for(int j = 0; j < 16;j++){
				boardNode[i,j] = new Node (i,j);
				}
		}
		Node startNode = boardNode[(int)startPos.x,(int)startPos.y];
		Node endNode = boardNode[(int)endPos.x,(int)endPos.y];

		//The first node
		closeNode.Add (boardNode[(int)startPos.x,(int)startPos.y]);
		Debug.Log ("Node 0 : " + closeNode[0].x.ToString() + " , " + closeNode[0].y.ToString());

		for (int i = 0; i < 40; i ++){
		//while(closeNode[closeNode.Count -1].F != 0 || count < 40){
			Node tempNode = closeNode[closeNode.Count -1];
			//add left.
			if (tempNode.x != 0){
				if (!closeNode.Contains(boardNode[tempNode.x - 1,tempNode.y]) && !openNode.Contains(boardNode[tempNode.x - 1,tempNode.y])){
					openNode.Add(boardNode[tempNode.x - 1,tempNode.y]);
					boardNode[tempNode.x - 1,tempNode.y].calculateScore(startNode,endNode);
					Node s = boardNode[tempNode.x - 1,tempNode.y];
					string info = s.x.ToString() + " , " + s.y.ToString() + " | G : " +s.G.ToString() + " | H : " + s.H.ToString() + " | F : " + s.F.ToString();
					Debug.Log(info);
					//Debug.Log(boardNode[tempNode.x - 1,tempNode.y].x.ToString() + " , " + boardNode[tempNode.x - 1,tempNode.y].y.ToString());
				}
			}
			//add right
			if (tempNode.x != 8){
				if (!closeNode.Contains(boardNode[tempNode.x + 1,tempNode.y])&& !openNode.Contains(boardNode[tempNode.x + 1,tempNode.y])){
					openNode.Add(boardNode[tempNode.x + 1,tempNode.y]);
					boardNode[tempNode.x + 1,tempNode.y].calculateScore(startNode,endNode);
					Node s = boardNode[tempNode.x + 1,tempNode.y];
					string info = s.x.ToString() + " , " + s.y.ToString() + " | G : " +s.G.ToString() + " | H : " + s.H.ToString() + " | F : " + s.F.ToString();
					Debug.Log(info);
					//Debug.Log(boardNode[tempNode.x + 1,tempNode.y].x.ToString() + " , " + boardNode[tempNode.x + 1,tempNode.y].y.ToString());
				}
			}

			//add up
			if (tempNode.y != 0){
				if (!closeNode.Contains(boardNode[tempNode.x,tempNode.y-1])&& !openNode.Contains(boardNode[tempNode.x,tempNode.y-1])){
					openNode.Add(boardNode[tempNode.x,tempNode.y-1]);
					boardNode[tempNode.x,tempNode.y-1].calculateScore(startNode,endNode);
					Node s = boardNode[tempNode.x,tempNode.y-1];
					string info = s.x.ToString() + " , " + s.y.ToString() + " | G : " +s.G.ToString() + " | H : " + s.H.ToString() + " | F : " + s.F.ToString();
					Debug.Log(info);
					//Debug.Log(boardNode[tempNode.x,tempNode.y-1].x.ToString() + " , " + boardNode[tempNode.x,tempNode.y-1].y.ToString());
				}
			}

			//add down
			if (tempNode.y != 15){
				if (!closeNode.Contains(boardNode[tempNode.x,tempNode.y+1])&& !openNode.Contains(boardNode[tempNode.x,tempNode.y+1])){
					openNode.Add(boardNode[tempNode.x,tempNode.y+1]);
					boardNode[tempNode.x,tempNode.y+1].calculateScore(startNode,endNode);
					Node s = boardNode[tempNode.x,tempNode.y+1];
					string info = s.x.ToString() + " , " + s.y.ToString() + " | G : " +s.G.ToString() + " | H : " + s.H.ToString() + " | F : " + s.F.ToString();
					Debug.Log(info);
					//Debug.Log(boardNode[tempNode.x,tempNode.y+1].x.ToString() + " , " + boardNode[tempNode.x,tempNode.y+1].y.ToString());
				}
			}

			//Choose the best score node
			float highScore = 99;
			foreach(Node n in openNode){
				if (n.F < highScore){
					highScore = n.F;
					tempNode = n;
				}
			}
			openNode.Remove(tempNode);
			closeNode.Add(tempNode);
			Debug.LogWarning("Node " + (closeNode.Count-1).ToString() + " : " + tempNode.x.ToString() + " , " + tempNode.y.ToString());
			if (tempNode.x == endNode.x && tempNode.y == endNode.y){
				Debug.LogError("YAY");
				break;
			}

		}
		//Debug.Log ("Path Complete");

	}
	

}

public class Node{
	public int x;
	public int y;
	public float G;
	public float H;
	public float F;

	public Node (int _x,int _y){
		x = _x;
		y = _y;
	}

	public void calculateScore(Node _startNode,Node _endNode){
		G = Mathf.Abs (this.x - _startNode.x) + Mathf.Abs (this.y - _startNode.y);
		H = Mathf.Abs (this.x - _endNode.x) + Mathf.Abs (this.y - _endNode.y);
		F = G + H;
	}

}
