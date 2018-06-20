using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour {

	private float speed = 4f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(speed * Time.deltaTime,0f,0f);
	}
	public void StopMovingPiece(){
		speed = 0f;
	}
}
