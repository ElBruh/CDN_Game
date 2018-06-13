using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCharacter : MonoBehaviour {

	public float moveSpeed = 2f;
	public bool stopMoving = false;
	public bool nextFloor = false;
	public bool moveNext = false;
	public Vector3 right;
	public Vector3 left;
	public float bleh;
	// Update is called once per frame
	void Start(){
		Debug.Log(transform.position);
	}
	void Update () {
		if (moveNext == true){
			Next();
			GameObject.Find("TowerManager").GetComponent<LevelManager>().LevelUp();
		}
		if(stopMoving == true){
			moveSpeed = 0;
		}
		else
			moveSpeed = 2;
		if(nextFloor == true){
			transform.Translate  (left * Time.deltaTime * moveSpeed, Space.World);
		}
		else
			transform.Translate  (right * Time.deltaTime * moveSpeed, Space.World);
	}
	public void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Enemy"){
			Debug.Log(col.collider.tag);
			stopMoving = true;
			GameObject.Find("WordManager").GetComponent<WordDisplay>().NewWord(col.gameObject);
		}
		if(col.collider.tag == "Ladder"){
			moveNext = true;
		}
		
	}
	public void Next(){
		bleh += 10;
		Debug.Log(nextFloor);
		if (nextFloor == true){
			transform.position = new Vector3((35.9f),bleh + 0.5f,0f);
			nextFloor = false;
		}
		else{
			transform.position = new Vector3((-35.9f),bleh + 0.5f,0f);
			nextFloor = true;
		}
		Debug.Log(transform.position);
		moveNext = false;
	}
}
