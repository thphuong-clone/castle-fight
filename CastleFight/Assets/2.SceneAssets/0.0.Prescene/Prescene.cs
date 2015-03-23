using UnityEngine;
using System.Collections;

public class Prescene : MonoBehaviour {

	void Awake(){	
		StartCoroutine (loadMainMenuLevel());	
	}

	IEnumerator loadMainMenuLevel(){
		yield return new WaitForSeconds (0.1f);
		Application.LoadLevel ("1.0.MainMenuScene");
	}
}
