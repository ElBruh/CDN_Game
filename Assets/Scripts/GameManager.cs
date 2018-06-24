using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Light dayNight;
	public float rotSpeed = 35f;
	public Camera cam;
	public Canvas mainMenu;
	public Canvas healthBar;
	public Canvas blackFade;
	private GameObject[] trees;
	private bool zoomOut;
  private bool zoomIn;
	private bool rotate;
	private bool keepRotating;
	// Use this for initialization
	void Start () {
		rotate  = true;
		keepRotating = true;
		healthBar.enabled = false;
		blackFade.enabled = false;
    zoomIn = false;
		cam.orthographicSize = 2.75f;
  }

	public void StartGame(){
		rotate = false;
		Debug.Log("StartGame in MainMenu has been called");
		mainMenu.enabled = false;
		healthBar.enabled = true;
		blackFade.enabled = true;
		Cursor.visible = false;
    GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().StartMoving();
		trees = GameObject.FindGameObjectsWithTag("Tree");
		foreach(GameObject tree in trees){
			tree.GetComponent<MovePiece>().StopMovingPiece();
		}
		zoomOut = true;
	}

	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("escape")){
			//Time.timeScale = 0;
			Application.Quit();
		}
		if (zoomOut == true && cam.orthographicSize <= 5.24f){
			cam.orthographicSize += 0.5f * Time.deltaTime;
		}

    if (zoomIn == true && cam.orthographicSize >= 2.75f)
    {
      cam.orthographicSize -= 0.5f * Time.deltaTime;
    }
  }
	void FixedUpdate(){
		//Debug.Log(dayNight.transform.rotation.y);
		if (rotate == true){
			dayNight.transform.Rotate(0f,rotSpeed * Time.deltaTime,0f);
		}
		if (rotate == false && keepRotating == true){
			dayNight.transform.Rotate(0f,rotSpeed * Time.deltaTime,0f);
			if(dayNight.transform.rotation.y <= 0.1 && dayNight.transform.rotation.y >= -0.1){
				keepRotating = false;
			}
		}
		
	}

  public void StartZoomIn()
  {
    zoomIn = true;
  }
}
