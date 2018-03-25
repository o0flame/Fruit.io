using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletRotation : MonoBehaviour {

	public float rotSpeed = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (transform.rotation.x, transform.rotation.y, 90f), Time.deltaTime*rotSpeed);

	}
}
