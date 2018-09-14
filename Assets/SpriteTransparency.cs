using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransparency : MonoBehaviour {
	public float alpha = 0.5f;
	//public SpriteRenderer spr;
	public Color color;
	// Use this for initialization
	void Start () {
		//spr = gameObject.GetComponent<SpriteRenderer> ();
		color = gameObject.GetComponent<SpriteRenderer> ().material.color;
		color.a = alpha;

		gameObject.GetComponent<SpriteRenderer> ().material.color = color;
		//Debug.Log ("alpha value" + gameObject.GetComponent<SpriteRenderer> ().material.color.a);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
