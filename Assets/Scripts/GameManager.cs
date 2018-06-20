using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//public Light dayNight;
	//private float rot;
	public Canvas mainMenu;
	public Canvas healthBar;
	public Canvas blackFade;
	private GameObject[] trees;
	// Use this for initialization
	void Start () {
		healthBar.enabled = false;
		blackFade.enabled = false;
	}

	public void StartGame(){
		mainMenu.enabled = false;
		healthBar.enabled = true;
		blackFade.enabled = true;
		Cursor.visible = false;
		GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().StartMoving();
		trees = GameObject.FindGameObjectsWithTag("Tree");
		foreach(GameObject tree in trees){
			tree.GetComponent<MovePiece>().StopMovingPiece();
		}
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
