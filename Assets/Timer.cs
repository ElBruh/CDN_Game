using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

	public TextMeshProUGUI text;
	
	
	// Update is called once per frame
	void Update () {
		var timeLeft = GameObject.Find("WordManager").GetComponent<WordDisplay>().timer;
		text.text = "Current Damage: " + timeLeft.ToString("0");
	}
}
