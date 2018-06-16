using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour {

	public TextMeshProUGUI text;
	private float enemyHealth;
	
	void Update () {
		if(GameObject.Find("WordManager").GetComponent<CombatManager>().tempEnemy != null){
			enemyHealth = GameObject.Find("WordManager").GetComponent<CombatManager>().tempEnemy.GetComponent<Life>().life;
		}
		else
			enemyHealth = 0;
		
		text.text = "Enemy Health: " + enemyHealth.ToString("0");
	}
}
