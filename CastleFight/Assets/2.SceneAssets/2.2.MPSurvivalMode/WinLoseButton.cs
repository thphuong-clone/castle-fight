using UnityEngine;
using System.Collections;

public class WinLoseButton : MonoBehaviour {

	public void loadMainMenu(){
		Time.timeScale = 1;
		Application.LoadLevel ("1.0.MainMenuScene");
	}
}
