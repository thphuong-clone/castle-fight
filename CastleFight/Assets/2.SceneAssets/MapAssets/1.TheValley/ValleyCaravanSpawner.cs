/***************
 * 
 * This class spawn the caravan for the valley map
 * 
 **************/ 

using UnityEngine;
using System.Collections;

public class ValleyCaravanSpawner : CaravanSpawner {
	protected override void spawnCaravan (){
		Caravan newCaravan = (Caravan)Instantiate (caravanPrefab);
		PlayerController.caravanList.Add (newCaravan);

		newCaravan.transform.position = 
			new Vector3 (UnityEngine.Random.Range(-3.75f,3.75f),UnityEngine.Random.Range(-1.15f,1.15f),0);

		newCaravan.transform.localRotation = Quaternion.Euler (new Vector3(0,0,UnityEngine.Random.Range(0.1f,359)));

	}


}
