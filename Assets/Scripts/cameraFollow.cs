using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	public Transform target;
	public Vector3 offset= new Vector3(0f,0f,-1f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (target != null) {
			transform.position = target.position + offset;
		}
	}
}
