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
	}

	void Update(){
		//Time.timeScale = 0.0000f;
		if (Input.GetKeyDown(KeyCode.Space)){
			if (Time.timeScale == 3)
				Time.timeScale =1;
			else
				Time.timeScale = 3;
		}

		if (Input.GetKeyDown(KeyCode.A)){
			if (Time.timeScale == 0.5f)
				Time.timeScale =1;
			else
				Time.timeScale = 0.5f;
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
				Application.LoadLevel("1.0.MainMenuScene");
			}
			yield return null;
		}
	}


}
