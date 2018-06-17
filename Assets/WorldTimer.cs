using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldTimer : MonoBehaviour {

	// Use this for initialization
	public float timeLeft;
	public Slider slide;
	void Start () {
		timeLeft = 100;
		slide.maxValue = timeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		slide.value = timeLeft;
	}
}
