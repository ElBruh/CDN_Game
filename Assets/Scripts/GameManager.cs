using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//public Light dayNight;
	//private float rot;
	public Camera cam;
	public Canvas mainMenu;
	public Canvas healthBar;
	public Canvas blackFade;
	private GameObject[] trees;
	private bool zoomOut;
	// Use this for initialization
	void Start () {
		healthBar.enabled = false;
		blackFade.enabled = false;
		cam.orthographicSize = 2.75f;
	}

	public void StartGame(){
		Debug.Log("StartGame in MainMenu has been called");
		mainMenu.enabled = false;
		healthBar.enabled = true;
		blackFade.enabled = true;
		Cursor.visible = false;
		GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().moveSpeed = 4;
		trees = GameObject.FindGameObjectsWithTag("Tree");
		foreach(GameObject tree in trees){
			tree.GetComponent<MovePiece>().StopMovingPiece();
		}
		zoomOut = true;
	}

	
	// Update is called once per frame
	void Update () {
		//rot+=0.2f;
		//dayNight.transform.Rotate(0f,rot * Time.deltaTime,0f);
		if(Input.GetKeyDown("escape")){
			//Time.timeScale = 0;
			Application.Quit();
		}
		if (zoomOut == true && cam.orthographicSize <= 5.24f){
			cam.orthographicSize += 0.5f * Time.deltaTime;
		}
	}
}
