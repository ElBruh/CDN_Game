using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFloor : MonoBehaviour {
  public static GameObject ladderTemplate;
  public static Object prefab;
  public List<GameObject> rooms;
  public int roomCount;
  public int floorNum;
  public float floorHeight;

  public static GameObject Create(int floorNum, int numEnemies)
  {
    if (prefab == null)
      prefab = Resources.Load("Prefabs/Floor");
    GameObject go = Instantiate(prefab) as GameObject;
    RandomFloor rf = go.GetComponent<RandomFloor>();
    rf.floorNum = floorNum;
    rf.rooms = new List<GameObject>();
    for (int i = 0; i < rf.roomCount; i++)
    {
      GameObject room = RandomRoom.Create(go, i, floorNum, rf.roomCount, hasEnemy:true);
      rf.rooms.Add(room);
    }

    go.transform.localPosition = new Vector3(0.0f, rf.GetFloorY, 0.0f);
    

    return go;
  }

  public float GetFloorY { get { Debug.Log(floorNum); return (floorNum - 1) * floorHeight; } }
  public int GetFloorNum { get { return floorNum; } }

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

