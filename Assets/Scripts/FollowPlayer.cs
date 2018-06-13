using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	// Use this for initialization
	private float vertical = 5.33f;
	private float horizontal = 20f;
	private float z = 1.9f;
	void Start () {
		Invoke("MoveToPlayer", 1);
		
		
	}
	
	void MoveToPlayer(){
		transform.position = GameObject.FindGameObjectWithTag("Hero").transform.position;
	}
	// Update is called once per frame
	void Update () {
		var playerPosition = GameObject.FindGameObjectWithTag("Hero").transform;
		horizontal = playerPosition.position.x;
		vertical = playerPosition.position.y;
		z = playerPosition.position.z;
		transform.position = new Vector3(horizontal + 10, vertical + 5, z + 8);
		transform.LookAt(playerPosition);
	}
}
