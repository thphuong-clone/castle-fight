using UnityEngine;
using System.Collections;

public class SelectedSoldier : MonoBehaviour {
	public Soldier parentSoldier;

	public void isSelected(){
		if (!parentSoldier.isPlayerOne)
			return;

		//high light the unit
		//Debug.Log ("Errr");
		parentSoldier.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1,1,1,0.60784313725f);
		//set the unit is to be the only one to be controlled by the player.
		SP_InputManager.selectedSoldier = parentSoldier.gameObject.GetComponent<Soldier> ();
		//open the control interface
		//parentSoldier.gameObject.SetActive (false);

		StartCoroutine (deselected());
	}

	IEnumerator deselected(){
		int count = 0;
		while (count < 12) {
			if (parentSoldier.gameObject.GetComponent<SpriteRenderer> ().color == new Color (1,0,0,0.60784313725f)){
				count = 20;
				break;
			}
			yield return new WaitForSeconds(0.25f);
			count ++;
		}
		if (count >= 19) {
			SP_InputManager.selectedSoldier = null;
			parentSoldier.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1,0,0,0.60784313725f);
		}
	}
}
