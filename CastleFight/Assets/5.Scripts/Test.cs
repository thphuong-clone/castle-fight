/*
 * This is a test function, it mostly use for nothing but It is kinda usefull somehow ....
*/
using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	//int count = 0;
	//public float x;

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

		if (Input.GetKeyDown (KeyCode.Escape)) {
//			Application.CaptureScreenshot(count.ToString() + ".png");
//			count ++;		
			StartCoroutine(waitForEsacpe());
		}

	}

	IEnumerator waitForEsacpe(){
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
