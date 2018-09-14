using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charSelection : MonoBehaviour {


	public GameObject apple, banana, watermelon,lime,pepper,orange,starfruit;
	public List<GameObject> fruitList;
	public enum fruitSelect{apple,banana,watermelon,lime,pepper,orange,starfruit};

	public bool disableBullet = true;

	public bool disableAllPlayer = false;

	public fruitSelect fs = fruitSelect.apple;
	// Use this for initialization
	void Start () {
		if(gameObject.transform.childCount>0){
			foreach (Transform child in transform) {
				fruitList.Add (child.gameObject);
			}
		}
		selectFruit ();
	}

	void selectFruit(){
		if (fs == fruitSelect.apple) {
			apple.SetActive (true);
			banana.SetActive (false);
			watermelon.SetActive (false);
			lime.SetActive (false);
			pepper.SetActive (false);
			orange.SetActive (false);
			starfruit.SetActive (false);
			apple.GetComponent<playerAttributes> ().fruitname = "apple";
			setPlayerAttributes (apple);
		} else if (fs == fruitSelect.banana) {
			apple.SetActive (false);
			banana.SetActive (true);
			watermelon.SetActive (false);
			lime.SetActive (false);
			pepper.SetActive (false);
			orange.SetActive (false);
			starfruit.SetActive (false);
			banana.GetComponent<playerAttributes> ().fruitname = "banana";
			setPlayerAttributes (banana);
		} else if (fs == fruitSelect.watermelon) {
			apple.SetActive (false);
			banana.SetActive (false);
			watermelon.SetActive (true);
			lime.SetActive (false);
			pepper.SetActive (false);
			orange.SetActive (false);
			starfruit.SetActive (false);
			watermelon.GetComponent<playerAttributes> ().fruitname = "watermelon";
			setPlayerAttributes (watermelon);
		} else if (fs == fruitSelect.lime) {
			apple.SetActive (false);
			banana.SetActive (false);
			watermelon.SetActive (false);
			lime.SetActive (true);
			pepper.SetActive (false);
			orange.SetActive (false);
			starfruit.SetActive (false);
			lime.GetComponent<playerAttributes> ().fruitname = "lime";
			setPlayerAttributes (lime);

		} else if (fs == fruitSelect.pepper) {
			apple.SetActive (false);
			banana.SetActive (false);
			watermelon.SetActive (false);
			lime.SetActive (false);
			pepper.SetActive (true);
			orange.SetActive (false);
			starfruit.SetActive (false);
			pepper.GetComponent<playerAttributes> ().fruitname = "pepper";
			setPlayerAttributes (pepper);

		} else if (fs == fruitSelect.orange) {
			apple.SetActive (false);
			banana.SetActive (false);
			watermelon.SetActive (false);
			lime.SetActive (false);
			pepper.SetActive (false);
			orange.SetActive (true);
			starfruit.SetActive (false);
			orange.GetComponent<playerAttributes> ().fruitname = "orange";
			setPlayerAttributes (orange);
		}else if (fs == fruitSelect.starfruit) {
			apple.SetActive (false);
			banana.SetActive (false);
			watermelon.SetActive (false);
			lime.SetActive (false);
			pepper.SetActive (false);
			orange.SetActive (false);
			starfruit.SetActive (true);
			starfruit.GetComponent<playerAttributes> ().fruitname = "starfruit";
			setPlayerAttributes (starfruit);
		}

	}
	void disableAll(){
		apple.SetActive (false);
		banana.SetActive (false);
		watermelon.SetActive (false);
		lime.SetActive (false);
		pepper.SetActive (false);
		orange.SetActive (false);
		starfruit.SetActive (false);
	}



	void setPlayerAttributes(GameObject f){
		f.GetComponent<playerAttributes> ().health = 100f;


		f.GetComponent<playerAttributes> ().bulletDieTime = 5f;
		f.GetComponent<playerAttributes> ().BulletdmgToOtherBullets = 50f;
		f.GetComponent<playerAttributes> ().bulletRange = 5f;
		f.GetComponent<playerAttributes> ().bulletHealth = 100f;
		f.GetComponent<playerAttributes> ().bulletBasicDmg=20f;


		//f.GetComponent<playerAttributes> ().criticRate = 0.3f;
		//f.GetComponent<playerAttributes> ().criticMagnifier = 2f;
	}





	// Update is called once per frame
	void Update () {
		//disableAll (); // this is for testing, disable all player.
		/*
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
		}*/
	}
}
