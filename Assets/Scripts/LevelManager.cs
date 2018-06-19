using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorDirections
{
  Left,
  Right
}

public class LevelManager : MonoBehaviour {

  public GameObject player;
  public int numLevels;
  private int lastFloor;
  public int currentFloor;
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
    GameObject floor = floors[0];
    RandomFloor rf = floor.GetComponent<RandomFloor>();
    return rf.GetFloorY;
  }
  public FloorDirections GetCurrentFloorDirection()
  {
    if (currentFloor % 2 == 0)
      return FloorDirections.Left;
    return FloorDirections.Right;
  }
  
}