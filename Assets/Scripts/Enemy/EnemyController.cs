using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


	public float health = 1000f;
	public float maxHealth = 1000f;
	public float healthRege = 0f;
	public float speed = 5.0f;
	public float initSpeed;
	public float firingRate = 5.0f;
	public float initfiringRate;
	public float collisionDamage=50.0f;
	public bool lockTimeCool;
	//public float bulletDamage=10.0f;

	public GameObject target,bullet,enemySpawner;

	public Vector3 direction = new Vector3(0f,0f,0f);
	public float timeCount = 0f;

	Vector3 shootDirection;


	public int enemyID;
	public float distBetweenPlayer;
	public bool chasePlayer = false;

	public float poisonTime, poisonTimeCount, tickleTime;
	public bool isPoisoned = false;
	public bool healthRegeCooled = true;


	public bool slowInProcess = false;

	public bool EnemyisFruit=false;


	public IEnumerator coroutine;
	public bool isCollided=false;
	public int colliderCnt = 0;

	public enum Buff{
		normal,
		poison,
		critical,
		slow,
		burn,
		stun,
		lifesteal
	}
	public enum Debuff{
		normal,
		poisoned,
		slowed,
		burned,
		stunned
	}
	public Buff buff;
	public Debuff debuff;


	//range to player
	public float enemyRange;
	//enemyRangeTimer;


	public void setEnemyID(int ID){
		enemyID = ID;
	}

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<playerAttributes> () != null) {
			EnemyisFruit = true;
		} else {
			EnemyisFruit = false;
		}
		StartCoroutine(passParaFromPlayerAtt ());
		lockTimeCool = true;

		initSpeed = speed;
		initfiringRate = firingRate;

		healthRegeCooled = true;
		//enemyRangeTimer = 0f;
		enemyRange = Random.Range (3f, 6f);


		gameObject.tag = "Enemy";
		getRandomDirection ();
		health = maxHealth;

		if(gameObject.GetComponent<UbhShotCtrl> ().m_shotList!=null){
			for(int i=0;i<gameObject.GetComponent<UbhShotCtrl> ().m_shotList.Capacity;i++){
				gameObject.GetComponent<UbhShotCtrl> ().m_shotList [i].m_afterDelay = firingRate;}
		}

		healthRege = 1;
		enemySpawner = gameObject.transform.parent.gameObject;



	}


	//not complete yet
	void debuffEffect(){
		if (debuff == Debuff.normal) {
		
		} else if (debuff == Debuff.poisoned) {
			
		}
	}

	public void hitByPoison(float Poisondmg,float percentDmg, float poisonDuration){
		if (debuff == Debuff.poisoned) {

			blinkEffect (0.15f);
			poisonTime = poisonDuration;
			healthRege = -(Poisondmg + percentDmg * maxHealth); // base dmg + percent dmg
			isPoisoned = true;
			poisonTimeCount = 0f;

			//Debug.Log ("poisonTime>0f && healthRegeCooled == true:::" + (poisonTime > 0f && healthRegeCooled == true));
		}

	}

	public void hitByBurnBullet(float burnDmg, float burnDuration){
		if (debuff == Debuff.burned) {
			
			poisonTime = burnDuration;
			healthRege = -(burnDmg); // burn dmg
			health -=target.GetComponent<playerAttributes> ().att;   // hit dmg

			//Debug.Log ("poisonTime>0f && healthRegeCooled == true:::" + (poisonTime > 0f && healthRegeCooled == true));
		}

	}

	void hitBySlowBullet(float slowPer, float slowt){
		StartCoroutine (speedChange (slowPer,slowt));
	}

	IEnumerator speedChange(float slowPer, float slowt){
		//Debug.Log (" speed change executed");
		//gameObject.GetComponent<UbhShotCtrl> ().m_shotList [i].m_afterDelay = firingRate;
		firingRate = initfiringRate / (1f-slowPer/100f);
		slowInProcess = true;

		speed = initSpeed * (1f-slowPer/100f);
		yield return new WaitForSeconds (slowt);
		slowInProcess = false;
		if (slowInProcess == false) {
			if(gameObject.GetComponent<UbhShotCtrl> ().m_shotList!=null){
				for(int i=0;i<gameObject.GetComponent<UbhShotCtrl> ().m_shotList.Capacity;i++){
					gameObject.GetComponent<UbhShotCtrl> ().m_shotList [i].m_afterDelay = firingRate;}
			}
			speed = initSpeed;
			firingRate = initfiringRate;

		}

	}

	IEnumerator healthRegeChange(){
		//Debug.Log ("coroutine starts");
		healthRegeCooled = false;
		if (health + healthRege < maxHealth) {
			health += healthRege;
		}
		yield return new WaitForSeconds (1f);
		healthRegeCooled = true;

		if (poisonTime >= 0) {
			poisonTime -= 1f;
		}
		/*
		if (debuff == Debuff.poisoned && poisonTime >= 0f) {
			poisonTime -= 1f;
		} else if (debuff == Debuff.burned && poisonTime >= 0f) {
			poisonTime -= 1f;
		}*/
	}
	IEnumerator passParaFromPlayerAtt(){
		while (true) {
			yield return new WaitForSeconds (1f);
			if (gameObject.GetComponent<playerAttributes> () != null){
			health = gameObject.GetComponent<playerAttributes> ().health;
			speed = gameObject.GetComponent<playerAttributes> ().speed;
		}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		//Debug.Log ("Collision count");


		if (col.gameObject.tag =="playerBullet") {
			if (col.gameObject.name == "pepperBullet(Clone)") {
				/// burn dmg, no percent dmg
				debuff = Debuff.burned;
				hitByBurnBullet (col.gameObject.GetComponent<BulletsController> ().burnDmg, col.gameObject.GetComponent<BulletsController> ().burnTime);
				hitByBullet ();
			} 
			else if(col.gameObject.name == "orangeBullet(Clone)"){
				debuff = Debuff.slowed;
				hitByBullet ();
				hitBySlowBullet (col.gameObject.GetComponent<BulletsController>().slowPercent,col.gameObject.GetComponent<BulletsController>().slowTime);
			}else if(col.gameObject.name == "starBullet(Clone)"){
				
				hitByBullet ();

			}else {
				hitByBullet ();
			}
			col.gameObject.GetComponent<BulletsController> ().destroyObj ();

		}
		else if (col.gameObject == target) {
			target.GetComponent<playerAttributes> ().healthUpdate (-collisionDamage);
			hitByPlayer ();
			isCollided = true;
		}
		StartCoroutine (blinkEffect(0.15f));
	}

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
		



	void hitByPlayer(){
		float dmg= target.GetComponent<playerAttributes> ().collisionDamage;
		target.GetComponent<playerAttributes> ().healthUpdate (collisionDamage);
		health -= dmg;

	}

	/*
	public void hitByLimePoison(float Poisondmg,float percentDmg, float poisonDuration){
		poisonTime = poisonDuration;
		healthRege = -(Poisondmg + percentDmg * maxHealth); // base dmg + percent dmg
		isPoisoned = true;
		poisonTimeCount = 5.5f;

	}*/

	void hitByBullet (){
		health -= target.GetComponent<playerAttributes> ().hitByPlayerBullet ();
	}

	IEnumerator lockOnPlayer(){
		while (true) {
			//lockTimeCool = false;
			yield return new WaitForSeconds (1f);
			//lockTimeCool = true;
			distBetweenPlayer = Vector3.Distance (target.transform.position, transform.position);


			if (bullet.GetComponent<enemyBulletCtrl> () != null) { // npc enemies
				if (distBetweenPlayer < bullet.GetComponent<enemyBulletCtrl> ().originRange * 2) {
					chasePlayer = true;
				} else {
					chasePlayer = false;
				}
			} else { // use bulletCtrl for fruit enemy
				if (distBetweenPlayer < bullet.GetComponent<BulletsController> ().originRange * 2) {
					chasePlayer = true;
				} else {
					chasePlayer = false;
				}
			}
		}
	}

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
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, -step * 0.8f);
			}

		}
	}



	void getRandomDirection(){
		var x = Random.Range (-1f, 1f);
		var y = Random.Range (-1f, 1f);
		direction = new Vector3 (x * speed, y * speed, 0f);
	}



	// Update is called once per frame
	void Update () {
		timeCount += Time.deltaTime;
		//update lock every sec
		//lockOnPlayer ();

		//if (lockTimeCool == true) {
		StartCoroutine (lockOnPlayer ());
		//}


		if (poisonTime <= 0) {
			healthRege = 1;
		}


		if (healthRegeCooled == true) {
			//Debug.Log ("healthRegecooled");
			StartCoroutine (healthRegeChange ());
		}


		if (timeCount <= 5.0f) {
			move ();
			//enemyRangeTimer += Time.deltaTime;

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
