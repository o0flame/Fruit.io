using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawnManager : MonoBehaviour {

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
		spawnInterval = 1f;
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


	int randomSelect(int range){
		
		return Random.Range (0, range+1);
	}


	// Update is called once per frame
	void Update () {
		Vector3 spawnerPos;
		spawnerPos= calcPos ();

		//regeneration in camera
		while (ItemInCamera (spawnerPos)==false) {
			spawnerPos= calcPos ();
		}

		time += Time.deltaTime;




		if (numSpawner < maxSpawner && time > spawnInterval) {
			GameObject enemySpawner = (GameObject)Instantiate (Spawner, spawnerPos, Quaternion.identity);

			int num = randomSelect (1); // select 0 or 1
			//Debug.Log("num ==" + num);
			if (num == 0) {
				//test for selecting enemy, works.
				enemySpawner.GetComponent<CreepSpawner> ().enemyLevels = CreepSpawner.EnemyLevels.Easy;
			} else if (num == 1) {
				enemySpawner.GetComponent<CreepSpawner> ().enemyLevels = CreepSpawner.EnemyLevels.Medium;
			}
			enemySpawner.transform.SetParent(gameObject.transform);


			numSpawner++;
			time = 0f;
		}

		//incamera = ItemInCamera(cam.transform.position);
	}
}
