using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	// Use this for initialization
	private float vertical = 5.33f;
	private float horizontal = 20f;
	private float z = 1.9f;
	public bool inCombat = false;
	private Transform target;
	void Start () {
		//give enough time for player to spawn
		Invoke("MoveToPlayer", 1);
	}
	
	void MoveToPlayer(){
		transform.position = GameObject.FindGameObjectWithTag("Hero").transform.position;
	}
	// Update is called once per frame
	void Update () {
		target = GameObject.FindGameObjectWithTag("Hero").transform;
		
		if(inCombat == true){
			//target = GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.transform;
		}
			
		
		horizontal = target.position.x;
		vertical = target.position.y;
		z = target.position.z;
		transform.position = new Vector3(horizontal + 5, vertical + 3, z + 8);
		transform.LookAt(target);
	}
}
