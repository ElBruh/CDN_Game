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
	private int next = 0;
	private int floorCount;

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
				treeLimit = 7;
				treeRangeXMax = 13f;
			}
			set = true;
			offsetx = 0;
			count = 0;
			TreeSpawn();
			GroundSpawn();
			timer = 3f;
		}
	}
	

	private void TreeSpawn(){
		while(count <= treeLimit){
			var treeIndex = Random.Range(0,trees.Length);
			var positionx = Random.Range(treeRangeXMax, treeRangeXMin);
			var positionz = Random.Range(-17, 17);
			var rotY = Random.Range(0,361);
			while(positionz > -5 && positionz < 5){
				positionz = Random.Range(-17, 17);
			}
			Instantiate(trees[treeIndex], new Vector3(positionx,0f,positionz), Quaternion.Euler(0f,rotY,0f));
			count++;
		}
	}
	private void GroundSpawn(){
		while(offsetx >= offsetXLimit){
			var groundIndex = 3;
			if (floorCount < 5){
				groundIndex = 3;
			}
			if (floorCount == 6){
				groundIndex = next;
			}
			if(floorCount > 6){
				groundIndex = 3;
			}
			if (floorCount >= 12){
				floorCount = 0;
			}
			Instantiate(ground[groundIndex],new Vector3(offsetx,0f,offsetz), Quaternion.Euler(0f,0,0f));
			floorCount++;
			if(offsetz == offsetZLimit){
				offsetx -= 5f;
				offsetz = -35;
				next++;
				if (next > 2){
					next = 0;
				}
			}
			offsetz+=5;
		}
	}	
}
