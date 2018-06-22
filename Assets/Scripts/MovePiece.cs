using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour {

	private float speed = 1.66f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
		transform.Translate(speed * Time.deltaTime,0f,0f, Space.World);
	}
	public void StopMovingPiece(){
		speed = 0f;
	}
}
