using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {
	//We can use this for spawning enemies or something

	//I was going to spawn words randomly but nah
	public Transform text;

	void Start(){
		InvokeRepeating("MoveSpawn", 1, 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void MoveSpawn(){
		var positionx = Random.Range(-18, 16);
		var positionz = Random.Range(-27,27);
		text.position = transform.position = new Vector3(positionx, 1, positionz);
	}
}
