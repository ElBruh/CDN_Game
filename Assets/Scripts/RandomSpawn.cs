using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {
	//We can use this for spawning enemies or something

	//I was going to spawn words randomly but nah
	public int treeLimit;
	public GameObject[] trees;
	public GameObject[] ground;
	
	public int count = 0;
	private float offsetx;
	private float offsetz;
	private float offsetXLimit;
	private float offsetZLimit;
	private float treeRangeXMax;
	private float treeRangeXMin;
	private bool set = false;
	private float timer = 3f;

	void Start(){
		offsetx = 125f;
		offsetz = -30;
		offsetXLimit = 0f;
		offsetZLimit = 25;
		treeRangeXMax = 120;
		treeRangeXMin = 0;
		GroundSpawn();
		TreeSpawn();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer-=Time.deltaTime;
		if(count >= treeLimit && timer <= 0){
			if (set == false){
				treeLimit = 10;
				treeRangeXMax = 13f;
			}
			set = true;
			count = 0;
			TreeSpawn();
			timer = 3f;
		}
	}
	

	private void TreeSpawn(){
		while(count <= treeLimit){
			var treeIndex = Random.Range(0,trees.Length);
			var positionx = Random.Range(treeRangeXMax, treeRangeXMin);
			var positionz = Random.Range(-17, 17);
			while(positionz > -5 && positionz < 5){
				positionz = Random.Range(-17, 17);
			}
			Instantiate(trees[treeIndex], new Vector3(positionx,0f,positionz), Quaternion.identity);
			count++;
		}
	}
	private void GroundSpawn(){
		while(offsetx >= offsetXLimit){
			var groundIndex = Random.Range(0,ground.Length);
			Instantiate(ground[groundIndex],new Vector3(offsetx,0f,offsetz), Quaternion.identity);
			if(offsetz == offsetZLimit){
				offsetx -= 3f;
				offsetz = -35;
			}
			offsetz+=5;
		}
	}	
}
