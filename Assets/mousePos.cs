using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousePos : MonoBehaviour {
	public Vector3 pos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		transform.position = pos;
	}
}
