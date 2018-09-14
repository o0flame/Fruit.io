using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepsControl : MonoBehaviour {
	public float health = 100f;
	public float exp=50f;
	public float dieTime = 30f;
	public float timeCount = 0f;

	public float scaleTime = 2f;
	public Vector3 initScale;
	public float scaleSpeed;

	public GameObject player, creepSpawner,creepSpawnManager;





	// Use this for initialization
	void Start () {
		initScale = transform.localScale;

		transform.localScale = initScale / 10;
		scaleSpeed = (initScale.x - transform.localScale.x)/scaleTime;
	}

	void OnCollisionEnter2D(Collision2D col){
		
		if (col.gameObject == player) {
			hitByPlayer ();
		}
	}

	void hitByPlayer(){
		
		health -= 1000f;
	}



	// Update is called once per frame
	void Update () {
		timeCount += Time.deltaTime;



		if (timeCount >= dieTime) {
			//Destroy (gameObject);
			Destroy (creepSpawner);

			creepSpawnManager.GetComponent<CreepSpawnManager> ().numSpawner -= 1;

		}


		if (health <= 0) {
			player.GetComponent<expSystem>().getCreeps(exp);
			//Destroy (gameObject);
			Destroy (creepSpawner);

			creepSpawnManager.GetComponent<CreepSpawnManager> ().numSpawner -= 1;


		}



	}


	void LateUpdate ()
	{
		//Debug.Log (transform.localScale);
		if (transform.localScale.x < initScale.x && timeCount < scaleTime) {
			transform.localScale += new Vector3 (0.001F, 0.001f, 0);
		}

	}

}
