/*****************************
 * 
 * This class takes screen shot of game play, because of ... reasons. 
 *
 *****************************/ 

using UnityEngine;
using System.Collections;
using System;

public class ScreenShotTakers : MonoBehaviour {
	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log("Screenshot Captured : " + DateTime.Now.ToString() +  ".png");
			Application.CaptureScreenshot(Application.persistentDataPath + "/" + DateTime.Now.GetHashCode() +  ".png");
		}
	}
}
