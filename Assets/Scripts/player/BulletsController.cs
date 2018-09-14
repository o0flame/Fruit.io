using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour {
	public int team =0;
	public float originRange = 5.0f;
	public float distance = 0f;
	public Vector3 initPos ;
	public Vector3 currPos ;



	public float health = 100.0f;
	public GameObject bullet;
	public GameObject fruit,enemy;
	public Vector2 speed;


	public float dieTime = 20f;
	public float time=0f;
	public float baseDmg;
	public float dmgToEnemyBullet=50f;
	//pepper
	public float burnDmg=0;
	public float burnTime=0;

	//lime
	public float poisonDmg=0;
	public float percentDmg = 0;
	public float poisonTime = 10f;
	public bool isStatic= false;

	//orange
	public float slowPercent,slowTime;
	public float criticRate, criticMagnifier, lifestealPercent;

	// Use this for initialization
	void Start () {
		//fruit = GameObject.FindWithTag ("FruitPlayer");
		initPos = transform.position;
		currPos = transform.position;
		dieTime = 10f;
		time = 0f;
		bullet = gameObject;
		speed = bullet.GetComponent<Rigidbody2D> ().velocity; 

		if (gameObject.GetComponent<Animator> () != null) {
			gameObject.GetComponent<Animator> ().enabled = false;
		}
		updateProperties ();

	}


	public void updateProperties(){
		if (gameObject.tag == "playerBullets") {
			baseDmg = fruit.GetComponent<playerAttributes> ().bulletBasicDmg;
			health = fruit.GetComponent<playerAttributes> ().bulletHealth;
			originRange = fruit.GetComponent<playerAttributes> ().bulletRange;
			dmgToEnemyBullet = fruit.GetComponent<playerAttributes> ().BulletdmgToOtherBullets;

			poisonDmg = fruit.GetComponent<playerAttributes> ().poisonDmg;
			poisonTime = fruit.GetComponent<playerAttributes> ().poisonTime;
			burnDmg = fruit.GetComponent<playerAttributes> ().burnDmg;
			burnTime = fruit.GetComponent<playerAttributes> ().burnTime;
			slowPercent = fruit.GetComponent<playerAttributes> ().slowPercent;
			slowTime = fruit.GetComponent<playerAttributes> ().slowTime;
			criticRate = fruit.GetComponent<playerAttributes> ().criticRate;
			criticMagnifier = fruit.GetComponent<playerAttributes> ().criticMagnifier;
			lifestealPercent = fruit.GetComponent<playerAttributes> ().lifestealPercent;
		}
		else if(gameObject.tag == "EnemyBullet"){
			//Debug.Log ("enemyBullet true");
			if (enemy != null) {
				//Debug.Log ("get attr from enemy player att");

				baseDmg = enemy.GetComponent<playerAttributes> ().bulletBasicDmg;
				health = enemy.GetComponent<playerAttributes> ().bulletHealth;
				originRange = enemy.GetComponent<playerAttributes> ().bulletRange;
				dmgToEnemyBullet = enemy.GetComponent<playerAttributes> ().BulletdmgToOtherBullets;

				poisonDmg = enemy.GetComponent<playerAttributes> ().poisonDmg;
				poisonTime = enemy.GetComponent<playerAttributes> ().poisonTime;
				burnDmg = enemy.GetComponent<playerAttributes> ().burnDmg;
				burnTime = enemy.GetComponent<playerAttributes> ().burnTime;
				slowPercent = enemy.GetComponent<playerAttributes> ().slowPercent;
				slowTime = enemy.GetComponent<playerAttributes> ().slowTime;
				criticRate = enemy.GetComponent<playerAttributes> ().criticRate;
				criticMagnifier = enemy.GetComponent<playerAttributes> ().criticMagnifier;
				lifestealPercent = enemy.GetComponent<playerAttributes> ().lifestealPercent;

			}
			/*
			*/
		}
		Debug.Log ("end of update Properties");

	}

	void OnCollisionEnter2D(Collision2D col){

		//enemyBullet to playerbullet
		if (col.gameObject.tag == "playerBullet") {
			col.gameObject.GetComponent<BulletsController> ().health -= dmgToEnemyBullet;

			health -= col.gameObject.GetComponent<BulletsController> ().dmgToEnemyBullet;

			//Destroy (gameObject);
		}

		//enemyBullet to fruitplayer
		else if (col.gameObject.transform.parent != null) {
			if (col.gameObject.tag == "FruitPlayer") {
				col.gameObject.GetComponent<playerAttributes> ().healthUpdate (-baseDmg);
				//Debug.Log("yellowBullets Collision by Player itself");
				Destroy(gameObject);

			}
		}

		//set pepper animation
		if (gameObject.name == "pepperBullet(Clone)") {
			
			gameObject.GetComponent<Animator> ().enabled = true;
			//gameObject.GetComponent<Animator> ().SetBool ("isCollided", true);
		}	
	}

	public void destroyObj(){
		if (gameObject.name == "pepperBullet(Clone)") {
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			StartCoroutine (KillOnAnimationEnd ());
		} else {
			Destroy (gameObject);
		}
	}

	private IEnumerator KillOnAnimationEnd() {
		yield return new WaitForSeconds (1.25f);
		Destroy (gameObject);
	}




	//for lime poison, only lime bullet is a trigger
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.GetComponent<EnemyController> ().debuff = EnemyController.Debuff.poisoned;
			other.gameObject.GetComponent<EnemyController> ().hitByPoison (poisonDmg, percentDmg, poisonTime);


		}
	}



	// Update is called once per frame
	void Update () {
		

		time += Time.deltaTime;

		currPos = transform.position;
		distance = Vector3.Distance (initPos, currPos); 
		//Debug.Log (distance);
		if (distance > originRange || time >dieTime || health <=0f) {
			
			Destroy (gameObject);

		}
	}
}
