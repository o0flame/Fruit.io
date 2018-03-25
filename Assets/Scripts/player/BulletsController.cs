using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour {
	public float originRange = 5.0f;
	public float distance = 0f;
	public Vector3 initPos ;
	public Vector3 currPos ;



	public float health = 100.0f;
	public GameObject bullet;

	public Vector2 speed;


	public float dieTime;
	public float time;

	// Use this for initialization
	void Start () {
		initPos = transform.position;
		currPos = transform.position;
		dieTime = 10f;
		time = 0f;
		bullet = gameObject;
		speed = bullet.GetComponent<Rigidbody2D> ().velocity; 

	}



	// Update is called once per frame
	void Update () {
		

		time += Time.deltaTime;
		currPos = transform.position;
		distance = Vector3.Distance (initPos, currPos); 
		//Debug.Log (distance);
		if (distance > originRange || time >dieTime || health <=0f) {
			Destroy (gameObject);
		}
	}
}
