using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkScripts : MonoBehaviour {
	public float time = 0f;
	private IEnumerator coroutine;

	public bool running = true;
	// Use this for initialization
	void Start () {
		//StartCoroutine("blinkEffect");
		//coroutine = blinkEffect(0.5f);

	}




	IEnumerator blinkEffect(float t){


		Color c1, c2;
		Color blink = Color.red;

		Debug.Log (" before loop");

		for (int i = 0; i < 5; i++) {
			c1 = gameObject.GetComponent<SpriteRenderer> ().color;
			c1.a = 0.5f;
			gameObject.GetComponent<SpriteRenderer> ().color = c1;
			Debug.Log ("color change to white" + gameObject.GetComponent<SpriteRenderer> ().material.color);
			yield return new WaitForSeconds (t);

			c2 = gameObject.GetComponent<SpriteRenderer> ().color;
			c2.a= 1f;
			gameObject.GetComponent<SpriteRenderer> ().color = c2;
			yield return new WaitForSeconds (t);

		}

		running = false;
	}
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 5f) {
			running = true;
		}
		if (running == true) {
			StartCoroutine (blinkEffect(0.5f));
		}

	}
}
