using UnityEngine;
using System.Collections;

public class Wall : Building {
    public override PathFinder.Position2D[] GetOccupyingGrid()
    {
        return GameUtil.GameConstant.GRID_TWO;
    }

	public override IEnumerator checkHealth (){
		while (!isDead) {
			yield return new WaitForSeconds(0.1f);	
			if (health <= 0){
				isDead = true;
				//push the unit back
				if (this.isPlayerOne)
					PlayerController.p1_buildingList[3].Remove(this);
				else
					PlayerController.p2_buildingList[3].Remove(this);
			}
		}
		yield return new WaitForSeconds (0.5f);
		this.gameObject.transform.position = new Vector3(100,100,100);
		yield return new WaitForSeconds (5f);
		Destroy (this.gameObject);
		
	}

	public override void OnDestroy (){
		if (this.isPlayerOne) {
			PlayerController.p1_buildingList[3].Remove(this.gameObject.GetComponent<Building>());
		}
		else{
			PlayerController.p2_buildingList[3].Remove(this.gameObject.GetComponent<Building>());
		}
		base.OnDestroy ();
	}

}
