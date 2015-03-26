/******************
 * 
 * This class handles the Demo Scene, doing some weird animation and stuffs,
 * Like really, really awesome and kick ass animation
 * Fuck yeah
 * 
 * 
 ****************/ 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoScript : MonoBehaviour {

	public float speed;

	public GameObject fatherCamera;
	public GameObject mainCamera;

	public GameObject theField;
	public GameObject castle_one;
	public GameObject castle_two;

	public GameObject firstSword;
	public GameObject secondSword;

	public GameObject kickAssLogo;
	public GameObject bossSword;

	public List<GameObject> starLists = new List<GameObject> ();

	Vector3 c1_position;
	Vector3 c2_position;
	Vector3 swordPosition;
	void Awake(){
		theField.transform.position = new Vector3 (0,30,0);
		c1_position = castle_one.transform.position;
		c2_position = castle_two.transform.position;
		swordPosition = bossSword.transform.position;
		castle_one.transform.position += new Vector3 (0,30,0);
		castle_two.transform.position += new Vector3 (0,30,0);
		bossSword.transform.position += new Vector3 (0, 20, 0);
		firstSword.transform.position = new Vector3 (-20.5f,-20,0);
		secondSword.transform.position = new Vector3 (20.5f,-20,0);
		kickAssLogo.transform.position = new Vector3 (0,-30,0);
		activeAllStars ();
		StartCoroutine (waitForIt ());
	}

	//Wait for the signal yooo
	IEnumerator waitForIt(){
		for (int i = 0; i < 999999999; i ++)  {
			if (Input.GetKeyDown(KeyCode.A)){
				StartCoroutine(showTheField());
				break;
			}
			yield return null;
		}
		yield return null;
	}


	//first, show the damn field
	IEnumerator showTheField(){
		for (int i = 0; i < 999999999; i ++)  {
			theField.transform.position =
				Vector3.MoveTowards(theField.transform.position,Vector3.zero,Time.deltaTime * speed);

			if (theField.transform.position == Vector3.zero){
				StartCoroutine(screenShake());
				StartCoroutine(showCastle_one());
				break;
			}

			yield return null;
		}
	}

	//then, show the first castle
	IEnumerator showCastle_one(){
		yield return new WaitForSeconds(0.75f);
		for (int i = 0; i < 999999999; i ++)  {
			castle_one.transform.position =
				Vector3.MoveTowards(castle_one.transform.position,c1_position,Time.deltaTime * speed);
			
			if (castle_one.transform.position == c1_position){
				StartCoroutine(screenShake());
				StartCoroutine(showCastle_two());
				break;
			}
			
			yield return null;
		}

	}

	//then, shows the second caslte
	IEnumerator showCastle_two(){
		yield return new WaitForSeconds(0.75f);
		for (int i = 0; i < 999999999; i ++)  {
			castle_two.transform.position =
				Vector3.MoveTowards(castle_two.transform.position,c2_position,Time.deltaTime * speed);
			
			if (castle_two.transform.position == c2_position){
				StartCoroutine(screenShake());
				StartCoroutine(showFirstSword());
				//Debug.Log("ANIMATION COMPLETE");
				break;
			}
			
			yield return null;
		}
		
	}

	IEnumerator showFirstSword(){
		yield return new WaitForSeconds(0.55f);
		for (int i = 0; i < 999999999; i ++)  {
			firstSword.transform.position = 
				Vector3.MoveTowards(firstSword.transform.position, new Vector3(-0.5f,0.75f,-2),Time.deltaTime * speed * 1.5f);
			if (firstSword.transform.position == new Vector3(-0.5f,0.75f,-2)){
				StartCoroutine(showSecondSword());
				break;
				//StartCoroutine(screenShake());
			}
			yield return null;
		}
	}
	
	IEnumerator showSecondSword(){		
		yield return new WaitForSeconds(0.55f);
		for (int i = 0; i < 999999999; i ++) {
			secondSword.transform.position = 
				Vector3.MoveTowards(secondSword.transform.position, new Vector3(0.5f,0.75f,-2),Time.deltaTime * speed * 1.5f);
			if (secondSword.transform.position == new Vector3(0.5f,0.75f,-2)){
				StartCoroutine(showKickassLogo());
				break;
				//StartCoroutine(screenShake());
			}
			yield return null;
		}
	}

	IEnumerator showKickassLogo(){
		yield return new WaitForSeconds(0.55f);

		for (int i = 0; i < 999999999; i ++)  {
			try{
				kickAssLogo.transform.position = 
					Vector3.MoveTowards(kickAssLogo.transform.position, new Vector3(0f,0.5f,-3),Time.deltaTime * speed * 1.5f);
				
				if (kickAssLogo.transform.position == new Vector3(0f,0.5f,-3)){
					StartCoroutine(showBossSword());
					mainCamera.GetComponent<Animator>().SetBool("MassiveScreenShake",true);
					mainCamera.GetComponent<Animator>().SetBool("AnotherShake",true);
					break;
				}
			}
			catch{
				Debug.Log("Fuck this shit");
				break;
			}

			yield return null;
		}

	}

	IEnumerator showBossSword(){
		yield return new WaitForSeconds(1.5f);
		mainCamera.GetComponent<Animator>().SetBool("AnotherShake",true);
		for (int i = 0; i < 999999999; i ++)  {
			fatherCamera.transform.position = Vector3.MoveTowards
				(fatherCamera.transform.position, new Vector3(0,16,0),Time.deltaTime * speed / 40);

			if (fatherCamera.transform.position.y >= 11.5f){					
				mainCamera.GetComponent<Animator>().SetBool("MassiveScreenShake",false);
				mainCamera.GetComponent<Animator>().SetBool("AnotherShake",false);
			}

			if (fatherCamera.transform.position == new Vector3(0,16,0)){
				fatherCamera.transform.parent = bossSword.transform;
				fatherCamera.transform.localPosition = Vector3.zero;
				break;
			}
			yield return null;	
		}
		//slowly move the camera to the top
		for (int i = 0; i < 999999999; i ++)  {
			bossSword.transform.position = 
				Vector3.MoveTowards(bossSword.transform.position, swordPosition,Time.deltaTime * speed * 1f);

			if (bossSword.transform.position == swordPosition){
				StartCoroutine(idle());
				StartCoroutine(screenShake());
				break;
			}
			yield return null;
		}
		fatherCamera.transform.parent = null;
		fatherCamera.transform.position = Vector3.zero;

	}

	IEnumerator idle(){
		for (int i = 0; i < 999999999; i ++)  {
			if (Input.GetKeyDown(KeyCode.B)){
				Awake();
				break;
			}
			yield return null;
		}
		yield return null;
	}

	IEnumerator screenShake(){
		mainCamera.GetComponent<Animator> ().SetBool ("ShakeNow",true);
		yield return new WaitForSeconds (0.1f);
		mainCamera.GetComponent<Animator> ().SetBool ("ShakeNow",false);
	}

	void activeAllStars(){
		foreach (GameObject stars in starLists) {
			StartCoroutine(activeStar(stars));
		}
	}

	IEnumerator activeStar(GameObject _star){
		yield return null;
		yield return new WaitForSeconds (UnityEngine.Random.Range(0.001f,0.75f));
		_star.gameObject.SetActive (true);

	}
}
