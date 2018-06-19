using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//public Light dayNight;
	//private float rot;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		//rot+=0.2f;
		//dayNight.transform.Rotate(0f,rot * Time.deltaTime,0f);
		if(Input.GetKeyDown("escape")){
			//Time.timeScale = 0;
			Application.Quit();
		}
	}
}
