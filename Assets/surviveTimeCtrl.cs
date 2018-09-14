using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class surviveTimeCtrl : MonoBehaviour {
	public float surviveTime =5f;
	// Use this for initialization
	public bool live=true;
	void Start () {
		StartCoroutine (survive ());
	}

	IEnumerator survive(){
		yield return new WaitForSeconds (surviveTime);
		if (live == false) {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
