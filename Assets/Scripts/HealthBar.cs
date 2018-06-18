using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Slider health;
	
	
	// Update is called once per frame
	void Update () {
		health.value = GameObject.Find("ObstacleManager").GetComponent<CombatManager>().health;
	}
}
