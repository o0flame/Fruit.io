using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepsControl : MonoBehaviour {
	public int health = 100;

	public float colDmg;

	// Use this for initialization
	void Start () {
		colDmg = -2.0f;
	}

	void hit(){
		health -= 50;
	}
	/*
	void OnTriggerEnter2D(Collider2D col){
		hit ();
		Destroy (col.gameObject);
	}*/
	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log("oncollision Func Called");
		hit ();
		//Debug.Log (col.gameObject.name);
		if (col.gameObject.name == "bananaBullet(Clone)" || col.gameObject.name == "appleBullet(Clone)") {
			Destroy (col.gameObject);
		}
		else if (col.gameObject.name == "player") {
			col.gameObject.GetComponent<playerAttributes>().healthUpdate(colDmg);
		}
	}

	// Update is called once per frame
	void Update () {
		


		if (health <= 0) {
			
			Destroy (gameObject); //destroy this object attached to
		}
	}
}
