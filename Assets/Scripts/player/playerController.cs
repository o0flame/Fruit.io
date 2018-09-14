using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum bigLevel{
	first,
	second,
	third
}



public class playerController : MonoBehaviour {
	public GameObject player, fruit, bullet;
	public bool isEnemy;


	public float speed = 5.0f;

	public float updateRate = 0.5f;

	public bigLevel biglevel;

	//public float shootInteval = 2f;
	public float time = 0f;

	// Use this for initialization
	void Start () {
		isEnemy = false;
		biglevel = bigLevel.first;
		InvokeRepeating ("updateFruit", 0.0001f, updateRate);
		//Fire();
		//Invoke

	}


	public void updateFruit(){
		if (isEnemy == false) {
			if (fruit.tag == "FruitPlayer") {
				if (fruit.GetComponent<playerAttributes> ().fruitname == "apple") {
					bulletLevelUP ();
				} else if (fruit.GetComponent<playerAttributes> ().fruitname == "banana") {
					bulletLevelUP ();
				} else if (fruit.GetComponent<playerAttributes> ().fruitname == "watermelon") {
					bulletLevelUP ();
				} else if (fruit.GetComponent<playerAttributes> ().fruitname == "lime") {
					bulletLevelUP ();
				} else if (fruit.GetComponent<playerAttributes> ().fruitname == "pepper") {
					bulletLevelUP ();
				} else if (fruit.GetComponent<playerAttributes> ().fruitname == "orange") {
					bulletLevelUP ();
				} else if (fruit.GetComponent<playerAttributes> ().fruitname == "starfruit") {
					bulletLevelUP ();
					//Debug.Log ("");
				}
			} else {//enemy fruit
				bulletLevelUP ();
			}
		}
	}

	void bulletLevelUP(){

		//Debug.Log ("bulletlvlup called");
		if (fruit.GetComponent<playerAttributes> ().fruitname == "lime") {
			//set limectrl
			fruit.GetComponent<limeCtrl>().bulletLvlChange =true;}

		if (biglevel == bigLevel.first) {
			setBulletLvlActive (0);
		} else if (biglevel == bigLevel.second) {
			setBulletLvlActive (1);
		} else if (biglevel == bigLevel.third) {
			setBulletLvlActive (2);
		}
	}



	void setBulletLvlActive(int i){
		
		for (int index = 0; index < 3; index++) {
			if (index == i) {
				transform.GetChild (index).gameObject.SetActive (true);
				transform.GetChild (index).gameObject.GetComponent<UbhBaseShot> ().isActive = true;
			} else {
				transform.GetChild (index).gameObject.SetActive (false);
				transform.GetChild (index).gameObject.GetComponent<UbhBaseShot> ().isActive = false;
			}

		}
	}
	/*
	public void disableAllBullet(){
		for (int index = 0; index < transform.childCount; index++) {
			transform.GetChild (index).gameObject.SetActive (false);
			transform.GetChild (index).gameObject.GetComponent<UbhBaseShot> ().isActive = false;
		}
	}*/  //not working cuz not all bullet can be inactive

	void zeroVelocity(){
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);

	}

	// Update is called once per frame
	void Update () {
		if (isEnemy == false) {
			if (gameObject.tag == "FruitPlayer") {
				//set child transform equal to parent
				gameObject.transform.localPosition = new Vector3 (0, 0, 0);
				if (Input.GetKey (KeyCode.A)) {
					player.transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);

					zeroVelocity ();
					//Quaternion rotTo = Quaternion.LookRotation (new Vector3 (0,0, 10));

					//player.transform.rotation = Quaternion.Lerp (transform.rotation, rotTo, 0.3f);


					player.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, -90));


				}
				if (Input.GetKey (KeyCode.W)) {
					player.transform.position += new Vector3 (0, speed * Time.deltaTime, 0);
					zeroVelocity ();
					player.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				}
				if (Input.GetKey (KeyCode.S)) {
					player.transform.position += new Vector3 (0, -speed * Time.deltaTime, 0);
					zeroVelocity ();
					player.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
				}
				if (Input.GetKey (KeyCode.D)) {
					player.transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
					zeroVelocity ();
					player.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));
				}

			}
		}
		// if this is enemy.
		else {
		}


	}
}
