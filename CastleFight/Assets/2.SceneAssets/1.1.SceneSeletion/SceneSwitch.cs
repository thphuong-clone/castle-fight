using UnityEngine;
using System.Collections;

public class SceneSwitch : MonoBehaviour {


	public void playSinglePlayer(){
		Application.LoadLevel ("2.1.SinglePlayerScene");
	}

	public void playMultiplayer(){
		Application.LoadLevel ("2.2.MultiplayerScene");
	}

	public void testCortana(){
		Application.LoadLevel ("3.1.CortanaTest");
	}
}
