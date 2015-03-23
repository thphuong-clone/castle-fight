/**************************
 * 
 * This class is responsible for the spawn of the caravan unit
 * The caravan is the only resource in the game. Get it
 * 
**************************/
using UnityEngine;
using System.Collections;

public class CaravanSpawner : MonoBehaviour {
	public Caravan caravanPrefab;

	[SerializeField]
	protected float caravanInitialDelay;//the time for the first caravan to appear to the game.

	[SerializeField]
	protected float caravanWaitTime;//the time for the caravan to be spawn

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

	protected virtual void spawnCaravan(){
		Caravan newCaravan = (Caravan)Instantiate (caravanPrefab);
		PlayerController.caravanList.Add (newCaravan);
		newCaravan.transform.position = 
			new Vector3(UnityEngine.Random.Range(-2f,2f),0.00001f,0);

		newCaravan.transform.localRotation = Quaternion.Euler (new Vector3(0,0,UnityEngine.Random.Range(0.1f,359)));
	}

}
