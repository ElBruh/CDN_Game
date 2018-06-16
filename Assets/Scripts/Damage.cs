using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour {
	public TextMeshProUGUI text;
	
	
	// Update is called once per frame
	void Update () {
		var currentDamage = GameObject.Find("WordManager").GetComponent<CombatManager>().damage;
		text.text = "Current Attack: " + currentDamage.ToString();
	}
}
