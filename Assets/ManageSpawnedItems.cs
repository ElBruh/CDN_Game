using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawnedItems : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Hero");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x+25 < transform.position.x){
			Destroy(gameObject);
		}
	}
}
