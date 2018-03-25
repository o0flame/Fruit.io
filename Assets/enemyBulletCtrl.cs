using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBulletCtrl : MonoBehaviour {

	public float originRange = 5.0f;
	public float distance = 0f;
	public Vector3 initPos ;
	public Vector3 currPos ;
	public float health = 100f;
	// Use this for initialization
	public float dmg = -100.0f;

	public float dmgToBullet = -50.0f;

	public float dieTime;
	public float time;

	void Start () {
		initPos = transform.position;
		currPos = transform.position;
		dieTime = 10.0f;
	}

	void OnCollisionEnter2D(Collision2D col){

		if (col.gameObject.name == "bananaBullet(Clone)"||col.gameObject.name == "appleBullet(Clone)"||col.gameObject.name == "watermelonBullet(Clone)") {
			col.gameObject.GetComponent<BulletsController> ().health += dmgToBullet;

			health -= 50f;
			//Destroy (gameObject);
		}

		if (col.gameObject.transform.parent != null) {
			if (col.gameObject.transform.parent.name == "player") {
				col.gameObject.GetComponent<playerAttributes> ().healthUpdate (dmg);
				Debug.Log("yellowBullets Collision by Player itself");
				Destroy(gameObject);

			}
		}


	}


	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		currPos = transform.position;
		distance = Vector3.Distance (initPos, currPos); 
		//Debug.Log (distance);
		if (distance > originRange || time > dieTime || health<=0f) {
			Destroy (gameObject);
		}

	}
}
