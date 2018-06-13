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
	void FixedUpdate () {
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
    transform.Translate(new Vector3(0, 10, 0), Space.World);
    if (nextFloor == true){
      transform.Rotate(new Vector3(0, 180, 0));
			nextFloor = false;
		}
		else{
      transform.Rotate(new Vector3(0, 180, 0));
			nextFloor = true;
		}
		Debug.Log(transform.position);
		moveNext = false;
	}
}
