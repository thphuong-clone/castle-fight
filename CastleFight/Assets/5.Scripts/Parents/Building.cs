//******************************

//The basic building. Every building inherit from this class

//******************************


using UnityEngine;
using System.Collections;

public class Building : Unit {
    public virtual PathFinder.Position2D[] GetOccupyingGrid()
    {
        return GameUtil.GameConstant.GRID_ONE;
    }

	public GameObject buildingSprite;
	public GameObject unitAura;
	
	public override void Awake (){
		PlayerController.reupdateSoldier ();
		StartCoroutine (gainHealth ());
		base.Awake ();
	}

	public virtual void OnDestroy(){
		PathFinder.GridMapUtils.MakeWorld ();
		PlayerController.reupdateSoldier ();
	}

	IEnumerator gainHealth(){
		while (true) {
			yield return new WaitForSeconds(1);
			if (this.health < this.maxHealth) 
				this.health += 0.01f * this.maxHealth;
			if (this.health > this.maxHealth)
				this.health = this.maxHealth;
		}
	}

}
