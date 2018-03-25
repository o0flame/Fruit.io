﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
	
	public Camera cam;

	public Vector3 camPos;
	public float camSize;
	public float width,height;//related to camera
	public int ScreenWidth = Screen.width;
	public int ScreenHeight = Screen.height;
	public bool incamera;
	public GameObject Spawner;
	public float worldWidth;
	public float worldHeight;

	public int maxSpawner=50;
	public int numSpawner=0;

	public float time, spawnInterval;

	// Use this for initialization
	void Start () {
		camSize = cam.orthographicSize;
		height = camSize;
		width = camSize * ScreenWidth /ScreenHeight;
		worldWidth =160f;
		worldHeight = 90f;
		spawnInterval = 0.1f;
		time = 0f;

	}

	Vector3 calcPos(){
		float x, y, z;
		x = Random.Range (-worldWidth / 2, worldWidth / 2);
		y = Random.Range (-worldHeight / 2, worldHeight / 2);
		z = 0;
		Vector3 pos = new Vector3 (x, y, z);
		return pos;
	}

	bool ItemInCamera(Vector3 pos){
		bool isTrue = false;
		if (pos.x < cam.transform.position.x + width && pos.x > cam.transform.position.x - width) {
			if (pos.y < cam.transform.position.y + height && pos.y > cam.transform.position.y - height) {
				//Debug.Log ("is true!!!");
				isTrue = true;
			}
		}
		return isTrue;
	}



	// Update is called once per frame
	void Update () {
		Vector3 spawnerPos;
		spawnerPos= calcPos ();
		while (ItemInCamera (spawnerPos)==true) {
			spawnerPos= calcPos ();
		}

		time += Time.deltaTime;




		if (numSpawner < maxSpawner && time > spawnInterval) {
			GameObject enemySpawner = (GameObject)Instantiate (Spawner, spawnerPos, Quaternion.identity);


			//test for selecting enemy, works.
			//enemySpawner.GetComponent<addEnemy> ().enemyLevels = addEnemy.EnemyLevels.Easy;
			enemySpawner.transform.SetParent(gameObject.transform);


			numSpawner++;
			time = 0f;
		}

		//incamera = ItemInCamera(cam.transform.position);
	}
}
