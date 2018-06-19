using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {
	//We can use this for spawning enemies or something

	//I was going to spawn words randomly but nah
	public int treeLimit;
	public GameObject[] trees;
	public GameObject[] ground;
	
	private int count = 0;
	private float offsetx;
	private float offsetz;
	private float offsetXLimit;
	private float offsetZLimit;

	void Start(){
		offsetx = 120f;
		offsetz = -30;
		offsetXLimit = 0f;
		offsetZLimit = 25;
		InvokeRepeating("TreeSpawn", 0, 0.01f);
		InvokeRepeating("GroundSpawn",0,0.0001f);
	}
	
	// Update is called once per frame
	void Update () {
		
		if(TreeSpawn() && GroundSpawn()){
			CancelInvoke();
		}
	}

	private bool TreeSpawn(){
		
		if(count == treeLimit){
			return true;
		}
		else{
			var treeIndex = Random.Range(0,trees.Length);
			var positionx = Random.Range(120, 0);
			var positionz = Random.Range(-17, 17);
			Instantiate(trees[treeIndex], new Vector3(positionx,0f,positionz), Quaternion.identity);
			count++;
			return false;
		}
	}

	private bool GroundSpawn(){
		if(offsetx == offsetXLimit){
			return true;
		}
		else{
			var groundIndex = Random.Range(0,ground.Length);
			Instantiate(ground[groundIndex],new Vector3(offsetx,0f,offsetz), Quaternion.identity);
			if(offsetz == offsetZLimit){
				offsetx -= 5f;
				offsetz = -35;
			}
			offsetz+=5;
			return false;
		}
		
	}	
}
