using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

	public Slider text;
	
	// Update is called once per frame
	void Update () {
		var timeLeft = GameObject.Find("CombatManager").GetComponent<CombatManager>().timer;
		text.value = timeLeft;
	}
}
