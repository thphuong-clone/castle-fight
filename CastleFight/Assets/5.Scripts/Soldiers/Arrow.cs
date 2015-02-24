using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public float arrowSpeed;
	public AttackInformation attack = new AttackInformation(0,0);

	public void attackEnemy(GameObject e,AttackInformation info){
		this.gameObject.SetActive(true);
		attack.attackDamage = info.attackDamage;
		attack.attackType = info.attackType;

		StartCoroutine (moveToEnemy (e));
	}

	IEnumerator moveToEnemy(GameObject enemy){
		while (true) {
			this.transform.position = Vector3.MoveTowards (transform.position,enemy.transform.position,Time.deltaTime * arrowSpeed);

			if (enemy.gameObject.GetComponent<Unit>().isDead)
				break;

			if (transform.position == enemy.transform.position){
				break;
			}

			yield return null;
		}

		enemy.GetComponent<Unit> ().SendMessage ("receiveDamage",attack);
		this.gameObject.SetActive (false);
	}

}
