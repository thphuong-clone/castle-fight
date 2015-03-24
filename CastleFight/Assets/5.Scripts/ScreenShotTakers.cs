/*****************************
 * 
 * This class takes screen shot of game play, because of ... reasons. 
 *
 *****************************/ 

using UnityEngine;
using System.Collections;
using System;

public class ScreenShotTakers : MonoBehaviour {

	private static ScreenShotTakers instance;
	private static bool created;
	
	public static ScreenShotTakers Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<ScreenShotTakers>();
				DontDestroyOnLoad(instance.gameObject);
			}
			
			return instance;
		}
	}
	
	void Awake () {
		if (created)
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(this.gameObject);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			created = true;
		}
	}
	
	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log("Screenshot Captured : " + DateTime.Now.ToString() +  ".png");
			Application.CaptureScreenshot(Application.dataPath + DateTime.Now.GetHashCode().ToString() +  ".png");		
		}
	}


}
