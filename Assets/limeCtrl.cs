using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limeCtrl : MonoBehaviour {
	public GameObject bullet;

	public bool bulletLvlChange = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (bulletLvlChange == true) {
			//Debug.Log("bulletLvlChanged");

			if (gameObject.transform.GetChild (0).gameObject.activeInHierarchy == true) {
				bullet.transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
				bullet.transform.GetChild (0).transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
				bullet.transform.GetChild (1).transform.localScale = new Vector3 (0.5f, 0.5f, 1f);
				bulletLvlChange = false;
			} else if (gameObject.transform.GetChild (1).gameObject.activeInHierarchy == true) {
				bullet.transform.localScale = new Vector3 (1f, 1f, 1f);
				bullet.transform.GetChild (0).transform.localScale = new Vector3 (1f, 1f, 1f);
				bullet.transform.GetChild (1).transform.localScale = new Vector3 (1f, 1f, 1f);
				bulletLvlChange = false;
			} else if (gameObject.transform.GetChild (2).gameObject.activeInHierarchy == true) {
				bullet.transform.localScale = new Vector3 (2f, 2f, 1f);
				bullet.transform.GetChild (0).transform.localScale = new Vector3 (2f, 2f, 1f);
				bullet.transform.GetChild (1).transform.localScale = new Vector3 (2f, 2f, 1f);
				bulletLvlChange = false;
			}
		}
	}
}
