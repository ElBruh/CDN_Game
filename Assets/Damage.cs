using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour {
	public TextMeshProUGUI text;
	
	
	// Update is called once per frame
	void Update () {
		var currentDamage = GameObject.Find("WordManager").GetComponent<WordDisplay>().damage;
		text.text = "Current Damage: " + currentDamage.ToString();
	}
}
