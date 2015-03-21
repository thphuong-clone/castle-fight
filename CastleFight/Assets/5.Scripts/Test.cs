/*
 * This is a test function, it mostly use for nothing but It is kinda usefull somehow ....
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {
	//int count = 0;
	//public float x;

//	List<string> sList = new List<string>{"Two","four","six","eight","hello"};

	public string _command;

	void Awake(){

//		
//		string[] part = new string[4];
//		int count = 0;
//		char[] c = _command.ToCharArray();
//		int cached = 0;
//		//first, find the number of words,
//		int spaceCount = 0;
//		for (int i = 0; i < c.Length; i ++) {
//			if (c[i] == ' ')
//				spaceCount ++;
//		}
//		if (spaceCount != 3){
//			Debug.LogWarning("Not enough words");
//			return;
//		}
//		//divide the command string into 4 parts.
//		for (int i = 0; i < c.Length; i ++) {
//			if (c[i] == ' '){
//				//add the word
//				for (int j = cached; j < i; j ++){
//					part[count] += c[j];
//				}
//				cached = i + 1 ;
//				count ++;
//				if (count == 3){
//					for (int k = cached; k < c.Length ; k ++){
//						part[3] += c[k];
//					}
//					//error
//				}
//			}
//		}
//		Debug.Log (part[0] + " , " + part[1] + " , " + part[2] + " , " + part[3]);
	}

	void Update(){
		//Time.timeScale = 0.0000f;
		if (Input.GetKeyDown(KeyCode.Space)){
			if (Time.timeScale == 3)
				Time.timeScale =1;
			else
				Time.timeScale = 3;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
//			Application.CaptureScreenshot(count.ToString() + ".png");
//			count ++;		
			StartCoroutine(waitForEscape());
		}

	}

	IEnumerator waitForEscape(){
		float time = 2f;
		while (time > 0){
			time -= Time.unscaledDeltaTime;
			if (Input.GetKeyDown(KeyCode.Escape)){
				Application.LoadLevel("1.0.SceneSelection");
			}
			yield return null;
		}
	}

}
