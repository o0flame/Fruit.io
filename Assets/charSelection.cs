using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charSelection : MonoBehaviour {


	public GameObject apple, banana, watermelon;

	public enum fruitSelect{apple,banana,watermelon};

	public bool disableBullet = true;

	public bool disableAllPlayer = false;

	public fruitSelect fs = fruitSelect.apple;
	// Use this for initialization
	void Start () {
		selectFruit ();
	}

	void selectFruit(){
		if (fs == fruitSelect.apple) {
			apple.SetActive (true);
			banana.SetActive (false);
			watermelon.SetActive (false);

			apple.GetComponent<playerAttributes>().fruitname ="apple";
		}
		if (fs == fruitSelect.banana) {
			apple.SetActive (false);
			banana.SetActive (true);
			watermelon.SetActive (false);
			banana.GetComponent<playerAttributes>().fruitname ="banana";
		}
		if (fs == fruitSelect.watermelon) {
			apple.SetActive (false);
			banana.SetActive (false);
			watermelon.SetActive (true);
			watermelon.GetComponent<playerAttributes>().fruitname ="watermelon";
		}
	}
	void disableAll(){
		apple.SetActive (false);
		banana.SetActive (false);
		watermelon.SetActive (false);
	}
	/*
	public void disableBullets(){
		GameObject fruit = GameObject.FindWithTag("FruitPlayer");

		fruit.GetComponent<playerController> ().disableAllBullet ();
	}*/


	// Update is called once per frame
	void Update () {
		//disableAll (); // this is for testing, disable all player.
		if (disableBullet == false) {

			if (fs == fruitSelect.apple) {
				apple.SetActive (true);
				banana.SetActive (false);
				watermelon.SetActive (false);
			}
			if (fs == fruitSelect.banana) {
				apple.SetActive (false);
				banana.SetActive (true);
				watermelon.SetActive (false);
			}
		} else {
			//disableBullets ();
		}
		if (disableAllPlayer == true) {
			disableAll ();
		}
	}
}
