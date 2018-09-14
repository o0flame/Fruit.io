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
		maxLevel = 25;
	}

	void gainEXPinTime(){
		if (gameObject.tag == "FruitPlayer") {
			if (timeForEXP >= 1.0f) {
				exp++;
				//exp = exp + 50f;
				timeForEXP -= 1.0f;
			}
		}
		//enemy fruit
		else {
			if (timeForEXP >= 1.0f) {
				exp += 20f;
				//exp = exp + 50f;
				timeForEXP -= 1.0f;
			}
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

	public void getCreeps(float e){
		exp += e;
	}


	void levelup(){
		if (exp >= expToNextLvl && level <maxLevel) {
			
			level++;
			attribute.attributesLevelUP ();
			exp = exp - expToNextLvl;
			expToNextLvl += expToNextLvl * 0.2f;

		}
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
		if (gameObject.tag == "FruitPlayer") {
			expBar.UpdateBar (exp, expToNextLvl);
		}
		//Debug.Log ("statement is" + (killEnemy));


		killEnemyEXP ();

		//health = player.GetComponent<playerController> ().health;
		timeForEXP += Time.deltaTime;

		gainEXPinTime ();


		if (gameObject.tag == "FruitPlayer") {
		//UI
		barText.text = "Level: " + level.ToString();
		//Debug.Log ("level.ToString()" + level.ToString());
		}

	}
}
