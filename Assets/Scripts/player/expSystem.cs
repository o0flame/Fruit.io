using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expSystem : MonoBehaviour {
	public GameObject fruit;

	public float exp;
	public float health;
	public int level;
	public int maxLevel;
	public float timeForEXP;
	public float expToNextLvl;

	public SimpleHealthBar expBar;
	public playerAttributes attribute;
	public bool killEnemy;
	public Text barText;


	// Use this for initialization
	void Start () {
		exp = 0;
		timeForEXP = 0.0f;
		expToNextLvl = 100;
		killEnemy = false;
		attribute = gameObject.GetComponent<playerAttributes>();
		level = 1;
		maxLevel = 5;
	}

	void gainEXPinTime(){
		if (timeForEXP >= 1.0f) {
			exp++;
			//exp = exp + 50f;
			timeForEXP -= 1.0f;
		}
		//Debug.Log ("!!!!!!!!!!!timeForEXP is" + timeForEXP);
	}

	public void killEnemyEXP(){

		if (killEnemy == true) {
			//Debug.Log ("killEnemy == true for once");
			exp += 100f;

			//exp += 100.0f;
			killEnemy = false;
		}

	}


	void levelup(){
		if (exp >= expToNextLvl && level <maxLevel) {
			
			level++;
			attribute.attributesLevelUP ();
			exp = exp - expToNextLvl;
			expToNextLvl += expToNextLvl * 1.5f;

		}



		//get player scripts of attributes






	}


	void changeBigLevel(){
		if (level == 2) {
			
			fruit.GetComponent<playerController> ().biglevel = bigLevel.second;

		} else if (level == 3) {
			fruit.GetComponent<playerController> ().biglevel = bigLevel.third;
		}
	}


	// Update is called once per frame
	void Update () {

		levelup ();


		changeBigLevel ();

		expBar.UpdateBar (exp, expToNextLvl);
		//Debug.Log ("statement is" + (killEnemy));


		killEnemyEXP ();

		//health = player.GetComponent<playerController> ().health;
		timeForEXP += Time.deltaTime;

		gainEXPinTime ();



		//UI
		barText.text = "Level: " + level.ToString();

	}
}
