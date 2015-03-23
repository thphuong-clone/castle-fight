/***************
 * 
 * This class spawns the caravan for the 4 rivers map
 * 
 **************/ 
using UnityEngine;
using System.Collections;

public class FourRiverCaravanSpawner : CaravanSpawner {
	protected override void spawnCaravan ()
	{		
		Caravan newCaravan = (Caravan)Instantiate (caravanPrefab);
		PlayerController.caravanList.Add (newCaravan);
		newCaravan.transform.position = 
			new Vector3(0.00001f,UnityEngine.Random.Range(-0.75f,0.75f),0);
		
		newCaravan.transform.localRotation = Quaternion.Euler (new Vector3(0,0,UnityEngine.Random.Range(0.1f,359)));
	}

}
