using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelManager : MonoBehaviour {

  public GameObject player;
  public int numLevels;
  private int lastFloor;
  private int currentFloor;
  private List<GameObject> floors;
  // Use this for initialization
  void Start () {
    Instantiate(player, new Vector3((35.9f),0f,0f), Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f)));
    currentFloor = 1;
    lastFloor = 0;
    floors = new List<GameObject>();
		for(int i=0; i < numLevels; i ++)
    {
      lastFloor += 1;
      floors.Add(RandomFloor.Create(lastFloor, 4));
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public void LevelUp()
  {
    floors.RemoveAt(0);
    lastFloor += 1;
    currentFloor += 1;
    floors.Add(RandomFloor.Create(lastFloor, 4));
  }
  
  public float GetCurrentFloorY()
  {
    GameObject floor = floors[currentFloor];
    RandomFloor rf = floor.GetComponent<RandomFloor>();
    return rf.GetFloorY;
  }
  
}
/*
public class Floor
{
  
  public float floorHeight = 5;
  public float floorBottom2CeilingHeight = 10;
  public float floorWidth = 80;
  
  private int floorNum;

  public Floor(int floorNum) 
  {
    this.floorNum = floorNum;
    floor = GameObject.Instantiate(floorTemplate, new Vector3(0, (floorNum - 1) * floorBottom2CeilingHeight - floorHeight/2), Quaternion.identity);
    ceiling = GameObject.Instantiate(ceilingTemplate, new Vector3(0, floorNum * floorBottom2CeilingHeight - floorHeight), Quaternion.Euler(180, 0 , 0));
    var numEnemies = (int)Random.Range(1, 3.99f);
    var enemies = new List<GameObject>();
    for (int i = 0; i < numEnemies; i++)
    {
      enemies.Add(GameObject.Instantiate(enemiesTemplate[0], new Vector3(floorWidth/2 - floorWidth/numEnemies*(i+1) + ladderMargin*2, (floorNum - 1) * floorBottom2CeilingHeight + 1.0f / 2), Quaternion.identity));
    }
    if (floorNum % 2 == 0)
    {
      // ladder goes on left
      GameObject.Instantiate(ladderTemplate, new Vector3(floorWidth / 2 - ladderMargin, ((floorNum - 1) * floorBottom2CeilingHeight) + floorHeight / 2), Quaternion.identity);
    }
    else
    {
      GameObject.Instantiate(ladderTemplate, new Vector3(-floorWidth / 2 + ladderMargin, ((floorNum - 1) * floorBottom2CeilingHeight) + floorHeight / 2), Quaternion.identity);
    }
    
  }
  public float GetFloorY { get { return (floorNum - 1) * floorBottom2CeilingHeight; } }
  public int GetFloorNum { get { return floorNum; } }
}

public class Room
{
  
}
*/