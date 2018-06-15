using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

	public Slider text;
	
	// Update is called once per frame
	void Update () {
		var timeLeft = GameObject.Find("WordManager").GetComponent<WordDisplay>().timer;
		text.value = timeLeft;
	}
}
