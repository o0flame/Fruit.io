using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerAttributes : MonoBehaviour {
	public float att;
	public float def;
	public float attrange;
	public float attSpeed;
	public float health;
	public float healthRege;
	public float collisionDamage;
	public float maxHealth;
	public float timeCount;
	//public expSystem system;


	public SimpleHealthBar healthBar;
	public string fruitname;




	// Use this for initialization
	void Start () {
		fruitname ="notSelect";


		att = 10.0f;
		def = 10.0f;
		attrange = 10.0f;
		attSpeed = 1f;

		collisionDamage = 30.0f;
		healthRege = 1.0f;
		maxHealth = 500.0f;
		health = maxHealth;
		timeCount = 0.0f;

		attributesUpdating ();
	}

	public void healthUpdate(float healthChange){
		health += healthChange;

	}

	public void attributesUpdating(){


		//attSpeed


		if(gameObject.GetComponent<UbhShotCtrl> ().m_shotList!=null){
			for(int i=0;i<gameObject.GetComponent<UbhShotCtrl> ().m_shotList.Capacity;i++){
				gameObject.GetComponent<UbhShotCtrl> ().m_shotList [i].m_afterDelay = attSpeed;}
			}

	}


	// Update is called once per frame
	void Update () {

		//Debug.Log (fruitname);
		timeCount += Time.deltaTime;
		if (timeCount >= 1.0f) {
			health += healthRege;
			timeCount = 0f;
		}

		healthBar.UpdateBar (health, maxHealth);





		if (health < 0f) {

			//Destroy (gameObject);
			//SceneManager.LoadScene ("End");

		}
	}

	public void attributesLevelUP(){

		//set max lvl to 50;

		if (gameObject.GetComponent<expSystem> ().level < gameObject.GetComponent<expSystem> ().maxLevel) {
			att += att * 0.2f;
			def += def * 0.2f;
			attSpeed = attSpeed * 0.8f;
			health += health * 0.2f;
			healthRege += healthRege * 0.2f;
			collisionDamage += collisionDamage * 0.3f;
			maxHealth += maxHealth * 0.2f;
		}

		attributesUpdating ();

	}
}
