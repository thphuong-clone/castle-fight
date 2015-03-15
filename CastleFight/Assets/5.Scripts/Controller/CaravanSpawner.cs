/*
 * This class is responsible for the spawn of the caravan unit
 * The caravan is the only resource in the game.
*/
using UnityEngine;
using System.Collections;

public class CaravanSpawner : MonoBehaviour {
	public Caravan caravanPrefab;

	[SerializeField]
	float caravanInitialDelay;//the time for the first caravan to appear to the game.

	[SerializeField]
	float caravanWaitTime;//the time for the caravan to be spawn

	void Awake(){
		StartCoroutine (caravanCountdown ());
	}

	IEnumerator caravanCountdown(){
		yield return new WaitForSeconds (caravanInitialDelay);
		while (true) {
			spawnCaravan();
			yield return new WaitForSeconds(caravanWaitTime);
		}
	}

	void spawnCaravan(){
		Caravan newCaravan = (Caravan)Instantiate (caravanPrefab);
		PlayerController.caravanList.Add (newCaravan);
		newCaravan.transform.position = Vector3.zero;
	}

}
