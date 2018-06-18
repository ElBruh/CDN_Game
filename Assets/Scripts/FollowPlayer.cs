using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	// Use this for initialization
	public Camera cam;
	private float vertical = 5.33f;
	private float horizontal = 20f;
	private float z = 1.9f;
	public bool inCombat = false;
	public bool FirstPerson;
	public Vector3 rotOffset;
	public Vector3 posOffset;
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
			//target = GameObject.Find("ObstacleManager").GetComponent<WordDisplay>().mText.transform;
		}
		
		horizontal = target.position.x;
		vertical = target.position.y;
		z = target.position.z;
		//transform.position = new Vector3(horizontal + 3, vertical + 3, z + 5);
		if (FirstPerson == true){
			//cam.orthographic = false;
			//cam.targetDisplay = 1;
			//cam.targetDisplay = 0;
			transform.position = new Vector3(horizontal,vertical + 1.05f,z);
			transform.rotation = target.rotation;
		}
		else{
			//cam.orthographic = true;
			transform.position = target.position + posOffset;
			transform.LookAt(target.position + rotOffset);
		}
	}
}
