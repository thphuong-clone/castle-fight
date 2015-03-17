//******************************

//The basic building. Every building inherit from this class

//******************************


using UnityEngine;
using System.Collections;

public class Building : Unit {

	public GameObject buildingSprite;
	public GameObject unitAura;
	
	public override void Awake (){
		PlayerController.reupdateSoldier ();
		base.Awake ();
	}

	public virtual void OnDestroy(){
		PathFinder.GridMapUtils.MakeWorld ();
		PlayerController.reupdateSoldier ();
	}
}
