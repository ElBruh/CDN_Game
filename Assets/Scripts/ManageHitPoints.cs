using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageHitPoints : MonoBehaviour {
	public float currentMax = 0;
	public Slider hitPoint;
	// Use this for initialization
	void Start () {
		hitPoint.maxValue = GameObject.Find("ObstacleManager").GetComponent<CombatManager>().tempEnemy.GetComponent<Life>().life;
		hitPoint.value =  0;
	}
	
	// Update is called once per frame
	public bool BuildUp(float damage){
		
		//currentMax += damage;
		hitPoint.value += damage;
		hitPoint.maxValue = GameObject.Find("ObstacleManager").GetComponent<CombatManager>().tempEnemy.GetComponent<Life>().life;
		Debug.Log("BuildUP: " + hitPoint.value);
		if(hitPoint.value >= GameObject.Find("ObstacleManager").GetComponent<CombatManager>().tempEnemy.GetComponent<Life>().life){
			//GameObject.Find("ObstacleManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().Die();
      		return true;
		}
		return false;
	}

	//if we ever want to do it by keystroke
	public void SmallBuildUp(){
		
		//hitPoint.maxValue = GameObject.Find("ObstacleManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life;
		Debug.Log("HitPointBarmax: " + hitPoint.maxValue);
		hitPoint.value += 5;
		GameObject.Find("ObstacleManager").GetComponent<CombatManager>().tempEnemy.GetComponent<Life>().TakeDamage(5);
	}
	public void Clear(){
		hitPoint.value = 0;
	}
}
