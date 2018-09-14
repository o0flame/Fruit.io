using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerAttributes : MonoBehaviour {
	public int team=0;
	public bool isEnemy; // control AI or player ctrl


	public float att;
	public float def;
	public float attrange;
	public float speed;
	public float attSpeed = 5f;
	public float health;
	public float healthRege;
	public float collisionDamage;
	public float maxHealth;
	public float timeCount;
	//public expSystem system;



	//public Text criticalAtt;
	public GameObject exMark;




	public void setEnemyID (int SpawnID){
		enemyID = SpawnID;
	}

	public enum Buff{
		normal,
		poison,
		critical,
		slow,
		burn,
		stun,
		lifesteal
	}
	public Buff buff;

	//bullet part
	public float bulletDieTime,BulletdmgToOtherBullets,bulletRange,bulletHealth,bulletBasicDmg;

	//bullet buff part
	public float slowPercent, slowTime, burnDmg,burnTime, poisonDmg, poisonTime;

	public float criticRate, criticMagnifier, lifestealPercent;

	public SimpleHealthBar healthBar;
	public string fruitname;



	//enemyCtrl part
	public bool chasePlayer, isCollided;
	//for spawner
	public GameObject target, enemySpawner;
	public int enemyID;
	public Vector3 direction;


	/*
	void selectBuff(){
		if(gameObject.name == "orange"){
			buff = Buff.slow;}
	}*/

	// Use this for initialization
	void Start () {
		if (gameObject.tag == "FruitPlayer") {
			isEnemy = false;
		} else {
			isEnemy = true;
			StartCoroutine (lockOnPlayer ());
			//StartCoroutine (moveStatusRoutine ());
		}
		isCollided = false;

		exMark = GameObject.FindWithTag ("exMark");
		fruitname ="notSelect";
		att = 10.0f;
		def = 10.0f;
		attrange = 10.0f;
		//attSpeed = 1f;
		speed = 3f;

		collisionDamage = 30.0f;
		healthRege = 1.0f;
		maxHealth = 500.0f;
		health = maxHealth;
		timeCount = 0.0f;

		attributesUpdating ();
		//healthRegeCooldown = true;
		StartCoroutine (healthRegeMet ());
	}
	/*
	public void OnCollisionEnter2D(Collision2D col){
		
		if (gameObject.tag == "FruitPlayer") {  // only enable collision when gameobject is player, handle enemy situation in EnemyCtrl
			//collision happens, how player behaves towards enemy and their bullets
			if(col.gameObject.tag == "enemyBullets"){
				// if it is a fruit enemy
				if(col.gameObject.GetComponent<EnemyController>().EnemyisFruit ==true){
					
				}
			}
		}

	}*/
	//**********************************ENEMY MOVEMENT start******************************************************************************************************//
	void getRandomDirection(){
		var x = Random.Range (-1f, 1f);
		var y = Random.Range (-1f, 1f);
		direction = new Vector3 (x , y , 0f);
	}


	IEnumerator lockOnPlayer(){
		while (true) {
			//lockTimeCool = false;
			yield return new WaitForSeconds (1.9f);
			//lockTimeCool = true;
			float distBetweenPlayer = Vector3.Distance (target.transform.position, transform.position);
			if (distBetweenPlayer < bulletRange * 2) {
				chasePlayer = true;
			} else {
				chasePlayer = false;
			}

		}
	}
	public float moveTimer=0f;

	void move(){
		//Debug.Log ("move func called");
		if (chasePlayer == false) {
			transform.position += speed * direction * Time.deltaTime;
		}
		//Debug.Log ("bug move" + direction * Time.deltaTime);
		else{
			float step = speed * Time.deltaTime;
				if (health > 0.2f * maxHealth && !isCollided) {
				//Debug.Log ("running towards player");
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step);
			} else { // running from player if collided or low health
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step * -0.8f);
			}

		}
	}
	/*
	IEnumerator moveStatusRoutine(){
		Debug.Log ("move routine called");
		while (true) {
			//random walk, every 5f;
			if (chasePlayer == false) {
				getRandomDirection ();
				move ();
				yield return new WaitForSeconds (5f);
			}
			//chase player, update every 0.5f;
			else {
				move ();
				yield return new WaitForSeconds (Time.deltaTime);
			}
		}
	}*/

	IEnumerator blinkEffect(float t){
		Color c1, c2;
		//Color blink = Color.red;

		//Debug.Log (" before loop");

		for (int i = 0; i < 2; i++) {
			c1 = gameObject.GetComponent<SpriteRenderer> ().color;
			c1.a = 0.3f;
			gameObject.GetComponent<SpriteRenderer> ().color = c1;
			//Debug.Log ("color change to white" + gameObject.GetComponent<SpriteRenderer> ().material.color);
			yield return new WaitForSeconds (t);
			c2 = gameObject.GetComponent<SpriteRenderer> ().color;
			c2.a= 1f;
			gameObject.GetComponent<SpriteRenderer> ().color = c2;
			yield return new WaitForSeconds (t);

		}
		//stop for 1s for collision
		yield return new WaitForSeconds (1);
		isCollided = false;
	}

	//**********************************ENEMY MOVEMENT END******************************************************************************************************//


	//**********************************collision part******************************************************************************************************//

	public float hitByPlayerBullet(){
		float dmg = bulletBasicDmg;
		if (criticRate > 0) {
			if (Random.value > criticRate) {
				//Instantiate (exMark, transform.position + new Vector3(0,2,0), Quaternion.identity);
				//t.transform.SetParent (gameObject.transform);
				dmg = criticMagnifier * dmg;
			}
		}
		if (lifestealPercent > 0) {
			health += dmg * lifestealPercent / 100f;
		}
		return dmg;
	}

	public void healthUpdate(float healthChange){
		health += healthChange;

	}









	//**********************************collision part******************************************************************************************************//
	public void attributesUpdating(){


		//attSpeed
		if(gameObject.GetComponent<UbhShotCtrl> ().m_shotList!=null){
			for(int i=0;i<gameObject.GetComponent<UbhShotCtrl> ().m_shotList.Capacity;i++){
				gameObject.GetComponent<UbhShotCtrl> ().m_shotList [i].m_afterDelay = attSpeed;}
			}

	}

	//public bool healthRegeCooldown;

	IEnumerator	healthRegeMet(){
		while (true) {
			if (health + healthRege < maxHealth) {
				health += healthRege;
			}
			//healthRegeCooldown = false;
			yield return new WaitForSeconds (1f);
			//healthRegeCooldown = true;
		}
	}






	// Update is called once per frame
	void Update () {

		//Debug.Log (fruitname);

		timeCount += Time.deltaTime;

		if (isEnemy == true) {
			moveTimer += Time.deltaTime;
			if (moveTimer <= 5.0f) {
				move ();
				//enemyRangeTimer += Time.deltaTime;

			}
			else {
				getRandomDirection ();
				moveTimer -= 5.0f;
			}
		}

		/*
		if (healthRegeCooldown == true) {
			//Debug.Log ("coroutine called");
			StartCoroutine (healthRegeMet ());
		}*/
		if (gameObject.tag == "FruitPlayer") {
			healthBar.UpdateBar (health, maxHealth);
		}




		if (health < 0f) {
			//enemy Died;
			if (isEnemy) {
				enemySpawner.GetComponent<addEnemy>().killEnemy (enemyID);
				Destroy (gameObject);
				if (target != null) {
					target.GetComponent<expSystem> ().killEnemy = true;
				}
			}
			//player died
			else {
				//Destroy (gameObject);
				//SceneManager.LoadScene ("End");
			}


		}
	}

	public void attributesLevelUP(){

		//set max lvl to 50;

		if (gameObject.GetComponent<expSystem> ().level < gameObject.GetComponent<expSystem> ().maxLevel) {
			att += 4f;
			if (def < 50f) {
				def += def * 0.1f;
			}
			if (attSpeed > 0.5f) {
				attSpeed = attSpeed * 0.95f;
		}
			health += 20f;
			healthRege += 0.2f;
			collisionDamage += collisionDamage * 0.2f;
			maxHealth += 20f;
		}

		attributesUpdating ();

	}
}
