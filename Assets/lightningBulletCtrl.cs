using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBulletCtrl : MonoBehaviour {
	public GameObject self, player;
	public Vector3 startPos,endPos;

	// Use this for initialization
	void Start () {
		//self = GameObject.FindWithTag ("lightBolt");
		self = gameObject;
		player = GameObject.FindWithTag("FruitPlayer");
		//Debug.Log ("!!!!!!"+ self.name + player.name);
	}
	
	// Update is called once per frame
	void Update () {
		startPos = self.transform.position;
		endPos = player.transform.position;
		gameObject.transform.GetChild (0).transform.GetChild (0).transform.position = startPos;
		gameObject.transform.GetChild (0).transform.GetChild (1).transform.position = endPos;
	}
}
