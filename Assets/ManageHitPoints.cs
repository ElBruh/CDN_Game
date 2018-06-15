using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageHitPoints : MonoBehaviour {
	public float currentMax = 0;
	public Slider hitPoint;
	// Use this for initialization
	void Start () {
		hitPoint.maxValue = GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life;
		hitPoint.value =  0;
	}
	
	// Update is called once per frame
	public void BuildUp(float damage){
		
		//currentMax += damage;
		hitPoint.value += damage;
		Debug.Log("BuildUP: " + hitPoint.value);
		if(hitPoint.value >= GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life){
			//GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().Die();
			GameObject.Find("WordManager").GetComponent<WordDisplay>().timer = 0;
		}
	}

	//if we ever want to do it by keystroke
	public void SmallBuildUp(){
		
		//hitPoint.maxValue = GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life;
		Debug.Log("HitPointBarmax: " + hitPoint.maxValue);
		hitPoint.value += 5;
		GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().TakeDamage(5);
	}
	public void Clear(){
		hitPoint.value = 0;
	}
}
