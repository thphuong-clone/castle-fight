using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

	public Image upPanel;
	public Image downPanel;

	public Image upCircle;
	public Image downCircle;

	//Main menu right here
	public Button playButton;
	public Text castleFight;

	//play selection menu here.
	public Button backButton;
	public Text onePlayer;
	public Text twoPlayers;

	bool doneAnimation = false;

	public void play(){
		StartCoroutine (playAction ());
	}

	IEnumerator playAction(){
		playButton.gameObject.SetActive (false);
		castleFight.gameObject.SetActive (false);
		StartCoroutine (backGroundAnimation ());
		yield return new WaitForSeconds (0.1f);
		while (!doneAnimation) {
			yield return null;
		}
		backButton.gameObject.SetActive (true);
		onePlayer.gameObject.SetActive (true);
		twoPlayers.gameObject.SetActive (true);

	}

	public void back(){
		StartCoroutine (backAction ());
	}

	IEnumerator backAction(){
		backButton.gameObject.SetActive (false);
		onePlayer.gameObject.SetActive (false);
		twoPlayers.gameObject.SetActive (false);
		StartCoroutine (backGroundAnimation ());
		yield return new WaitForSeconds (0.1f);
		while (!doneAnimation) {
			yield return null;
		}
		playButton.gameObject.SetActive (true);
		castleFight.gameObject.SetActive (true);

	}

	IEnumerator backGroundAnimation(){
		float h = Screen.height;
		doneAnimation = false;
		float p = 0;
		Vector2 positive = Vector2.zero;
		Vector2 negative = Vector2.zero;
		while (p < h/2* 1.1f) {
			p += Time.deltaTime * h * 1.5f;
			positive = new Vector2(0,p);
			negative = new Vector2(0,-p);

			upPanel.rectTransform.offsetMax = positive;
			upPanel.rectTransform.offsetMin = positive;
			upCircle.rectTransform.offsetMax = positive;
			upCircle.rectTransform.offsetMin = positive;

			downPanel.rectTransform.offsetMax = negative;
			downPanel.rectTransform.offsetMin = negative;
			downCircle.rectTransform.offsetMax = negative;
			downCircle.rectTransform.offsetMin = negative;
			yield return null;
		}
		yield return new WaitForSeconds (0.125f);
		while (true) {
			p -= Time.deltaTime * h * 1.5f;
			positive = new Vector2(0,p);
			negative = new Vector2(0,-p);

			upPanel.rectTransform.offsetMax = positive;
			upPanel.rectTransform.offsetMin = positive;
			upCircle.rectTransform.offsetMax = positive;
			upCircle.rectTransform.offsetMin = positive;
			
			downPanel.rectTransform.offsetMax = negative;
			downPanel.rectTransform.offsetMin = negative;
			downCircle.rectTransform.offsetMax = negative;
			downCircle.rectTransform.offsetMin = negative;

			if (p <= 0){
				upPanel.rectTransform.offsetMax = Vector2.zero;
				upPanel.rectTransform.offsetMin = Vector2.zero;
				upCircle.rectTransform.offsetMax = Vector2.zero;
				upCircle.rectTransform.offsetMin = Vector2.zero;
				
				downPanel.rectTransform.offsetMax = Vector2.zero;
				downPanel.rectTransform.offsetMin = Vector2.zero;
				downCircle.rectTransform.offsetMax = Vector2.zero;
				downCircle.rectTransform.offsetMin = Vector2.zero;

				break;
			}
			yield return null;
		}
		doneAnimation = true;
	}

}
