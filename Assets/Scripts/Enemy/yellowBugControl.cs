﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowBugControl : MonoBehaviour {
	
	public float health = 100f;
	public float speed = 5.0f;
	public GameObject target;
	public Vector3 direction = new Vector3(0f,0f,0f);
	public float timeCount = 0f;
	public GameObject bullet;
	public float firingRate = 1.0f;
	Vector3 shootDirection;


	public float collisionDamage;
	public float bulletDamage;


	public GameObject enemySpawner;

	public int enemyID;
	public float distBetweenPlayer;
	public bool chasePlayer = false;

	public void setEnemyID(int ID){
		enemyID = ID;
	}

	// Use this for initialization
	void Start () {
		gameObject.tag = "Enemy";
		getRandomDirection ();

		//setShootDirection ();
		//InvokeRepeating ("Fire", 2.0f , firingRate);

		collisionDamage = -10.0f;
		bulletDamage = -10.0f;
		//GameObject go = this.transform.parent as GameObject;
		//gameObject.name
		enemySpawner = gameObject.transform.parent.gameObject;
		target = GameObject.FindWithTag ("FruitPlayer");
		//Debug.Log ("findTarget is" + target.name);


	}

	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log("oncollision Func Called");

		//Debug.Log ("collision object is" + col.gameObject.name);
		if (col.gameObject.name == "bananaBullet(Clone)" || col.gameObject.name == "appleBullet(Clone)"||col.gameObject.name =="watermelonBullet(Clone)") {
			Destroy (col.gameObject);
			hitByBullet ();
		}
		else if (col.gameObject.transform.parent.name  == "player") {
			Debug.Log("yellow bug Hit by Player");
			col.gameObject.GetComponent<playerAttributes> ().healthUpdate (collisionDamage);
			hitByPlayer ();
		}


	}

	void hitByPlayer(){
		float dmg= target.GetComponent<playerAttributes> ().collisionDamage;
		health -= 40.0f;
	}

	
	void hitByBullet (){
		health -= 20f;
	}

	void lockOnPlayer(){
		distBetweenPlayer = Vector3.Distance (target.transform.position, transform.position);
		if (distBetweenPlayer < bullet.GetComponent<yellowBulletControl> ().originRange * 2) {
			chasePlayer = true;
		} else {
			chasePlayer = false;
		}
	}

	void move(){
		if (chasePlayer == false) {
			transform.position += direction * Time.deltaTime;
		}
		//Debug.Log ("bug move" + direction * Time.deltaTime);
		else{
			Vector3 heading = target.transform.position - transform.position;
			Vector3 chaseDirection = heading / heading.magnitude;
			transform.position += speed * chaseDirection * Time.deltaTime;
		}
	}

	void getRandomDirection(){
		var x = Random.Range (-1f, 1f);
		var y = Random.Range (-1f, 1f);
		direction = new Vector3 (x * speed, y * speed, 0f);

		//Debug.Log ("dirt" + x * speed);
	}



	/*
	void setShootDirection(){
		Vector3 targetPos = target.transform.position;
		float angle = Mathf.Atan2 (targetPos.y - transform.position.y, targetPos.x - transform.position.x); 
		float xspeed = speed  * Mathf.Cos(angle);
		float yspeed = speed  * Mathf.Sin(angle);
		shootDirection =  new Vector3(xspeed,yspeed,0f);

	}

	void Fire(){
		GameObject bu  = (GameObject)Instantiate (bullet, transform.position, Quaternion.identity);


		//Debug.Log ("xspeed" + xspeed + "yspeed" + yspeed  );
		bu.GetComponent<Rigidbody2D>().velocity = shootDirection;
		//Debug.Log (shootDirection);
	}*/

	// Update is called once per frame
	void Update () {
		timeCount += Time.deltaTime;

		lockOnPlayer ();

		if (timeCount <= 5.0f) {
			move ();

		}
		else {
			getRandomDirection ();
			timeCount -= 5.0f;
		}


		if (health <= 0) {
			Destroy (gameObject);
			//gameObject.GetComponentInParent<addEnemy> ().killEnemy (enemyID);
			enemySpawner.GetComponent<addEnemy>().killEnemy (enemyID);

			if (target != null) {
				target.GetComponent<expSystem> ().killEnemy = true;
			}
		}



	}
}
