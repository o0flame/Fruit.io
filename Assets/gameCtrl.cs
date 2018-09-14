using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCtrl : MonoBehaviour {
	
	public GameObject fruit;
	public GameObject[] enemies;

	public bool disablePlayerBullet = false;

	public bool disableEnemyBullet = false;


	public bool change = false;
	// Use this for initialization
	void Start () {
		
		fruit = GameObject.FindWithTag("FruitPlayer");
		//enemies = new GameObject[2];

		//Debug.Log ("number of enemies" + enemies.Length);
	}

	void disableEnemyBulletMethod(){
		foreach (GameObject e in enemies) {
			e.GetComponent<UbhShotCtrl> ().enabled = false;
		}
	}

	void enableEnemyBulletMethod(){
		foreach (GameObject e in enemies) {
			e.GetComponent<UbhShotCtrl> ().enabled = true;
		}
	}

	void disablePlayerBulletMethod(){
		
		if (fruit != null) {
			
			fruit.GetComponent<UbhShotCtrl> ().enabled = false;

		}
	}

	void enablePlayerBulletMethod(){
		
		if (fruit != null) {
			fruit.GetComponent<UbhShotCtrl> ().enabled = true;


		}
	}


	// Update is called once per frame
	void Update () {
		//click change, and then disable
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		//Debug.Log ("5 th enemy" + enemies[4].name);
		if (change == false || change ==true) {

			if (disablePlayerBullet == false) {
				enablePlayerBulletMethod ();
				change = false;
			} else {
				//Debug.Log ("disablePlayerBulletMethod called");
				disablePlayerBulletMethod ();
				change = false;
			}


			if (disableEnemyBullet == false) {
				enableEnemyBulletMethod ();
				change = false;
			} else {
				disableEnemyBulletMethod ();
				Debug.Log ("disable Enemy Bullets");
				change = false;
			}
		}

	}

}
