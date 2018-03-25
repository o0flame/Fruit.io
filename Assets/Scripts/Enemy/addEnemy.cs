using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addEnemy : MonoBehaviour {

	public GameObject EasyEnemy;
	public GameObject MediumEnemy;
	public GameObject HardEnemy;
	public GameObject BossEnemy;
	public GameObject ob;

	public int spawnedEnemy =0;
	public int numEnemy=0;
	public int totalEnemy = 100;



	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, new Vector3 (0.5f,0.5f,0.1f));
	}

	public enum SpawnTypes{
		Normal,
		Once,
		Wave,
		TimedWave
	}

	public enum EnemyLevels{
		Easy,
		Medium,
		Hard,
		Boss
	}

	private int SpawnID;

	//different spawn states
	private bool waveSpawn = false;
	public SpawnTypes spawnType = SpawnTypes.Normal;
	public EnemyLevels enemyLevels = EnemyLevels.Easy;
	//timed wave controls
	public float waveTimer = 30.0f;
	private float timeTillWave = 0.0f;
	//wave control
	public int totalWaves = 5;
	private int numWaves = 0;


	public bool Spawn = true;

	// Use this for initialization
	void Start () {
		SpawnID = Random.Range (1, 5000);
	}
	
	// Update is called once per frame
	void Update () {
		if (Spawn) {
			//Spawns enemies everytime one dies
			if (spawnType == SpawnTypes.Normal) {
				if (numEnemy < totalEnemy) {
					spawnEnemy ();
				}
			} 
			else if (spawnType == SpawnTypes.Once) {
				if (spawnedEnemy >= totalEnemy) {
					Spawn = false;
				} else {
					spawnEnemy ();
				}
			} 
			else if (spawnType == SpawnTypes.Wave) {
				if (numWaves < totalWaves + 1) {
					if (waveSpawn) {
						spawnEnemy ();
					}
				}
				if (numEnemy == 0) {
					waveSpawn = true;
					numWaves++;
				}
				if (numEnemy == totalEnemy) {
					waveSpawn = false;
				}
			}
			else if (spawnType == SpawnTypes.TimedWave) {
				if (numWaves <= totalWaves) {
					timeTillWave += Time.deltaTime;
					if (waveSpawn) {
						spawnEnemy ();
					}
					if (timeTillWave >= waveTimer) {
						waveSpawn = true;
						timeTillWave = 0.0f;
						numWaves++;
						numEnemy = 0;

					}
					if (numEnemy >= totalEnemy) {
						waveSpawn = false;
					}
				} else {
					Spawn = false;
				}
			}
		}


	}


	public void setEnemyCtrl(GameObject Enemy){
		Enemy.GetComponent<EnemyController> ().target = GameObject.FindWithTag ("FruitPlayer"); 
		Enemy.GetComponent<EnemyController> ().enemySpawner = gameObject;
		Enemy.GetComponent<EnemyController> ().setEnemyID (SpawnID);
		Enemy.transform.SetParent (gameObject.transform);
	}

	private void spawnEnemy(){
		if (enemyLevels == EnemyLevels.Easy) {
			if (EasyEnemy != null) {
				GameObject Enemy = Instantiate (EasyEnemy, gameObject.transform.position, Quaternion.identity) as GameObject;


				//set target to player
				//string playerName = GameObject.Find("player").GetComponent<charSelection>().fs.ToString();
				//Debug.Log ("PlayerName is " + playerName);
				Enemy.GetComponent<yellowBugControl> ().target = GameObject.FindWithTag ("FruitPlayer"); 
				Enemy.GetComponent<yellowBugControl> ().enemySpawner = gameObject;
				Enemy.GetComponent<yellowBugControl> ().setEnemyID (SpawnID);
				Enemy.transform.SetParent (gameObject.transform);
			} else {
				Debug.Log ("Error: No ez enemy Prefab loaded");
			}
		}
		else if (enemyLevels == EnemyLevels.Medium) {
			if (MediumEnemy != null) {
				GameObject Enemy = Instantiate (MediumEnemy, gameObject.transform.position, Quaternion.identity) as GameObject;
				Enemy.SendMessage ("setName", SpawnID);
			} else {
				Debug.Log ("Error: No medium enemy Prefab loaded");
			}
		}
		else if (enemyLevels == EnemyLevels.Hard) {
			if (HardEnemy != null) {
				GameObject Enemy = Instantiate (HardEnemy, gameObject.transform.position, Quaternion.identity) as GameObject;
				Enemy.SendMessage ("setName", SpawnID);
			} else {
				Debug.Log ("Error: No hard enemy Prefab loaded");
			}
		}
		else if (enemyLevels == EnemyLevels.Boss) {
			if (BossEnemy != null) {
				GameObject Enemy = Instantiate (BossEnemy, gameObject.transform.position, Quaternion.identity) as GameObject;


				setEnemyCtrl (Enemy);

				//Enemy.SendMessage ("setName", SpawnID);
			} else {
				Debug.Log ("Error: No boss enemy Prefab loaded");
			}
		}
		numEnemy++;
		spawnedEnemy++;
	}


	public void killEnemy(int sID){
		if (SpawnID == sID) {
			numEnemy--;
		}
	}

	public void enableSpawner(int sID){
		if (SpawnID == sID) {
			Spawn = true;
		}
	}

	public void disableSpawner(int sID){
		if (SpawnID == sID) {
			Spawn = false;
		}
	}

	public void enableTrigger(){
		Spawn = true;
	}


	public float TimeTillWave {
		get{
			return timeTillWave;
		}
	}


}
