using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWithCam : MonoBehaviour {

	public Transform target;
	public Vector3 startPos;
	public Vector3 offset;
	// Use this for initialization
	void Start () {
		startPos = gameObject.transform.position;
		offset = startPos;
	}

	// Update is called once per frame
	void LateUpdate () {
		
		transform.position = target.position + offset;
	}
}
