using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoom : MonoBehaviour
{
  public static Object prefab;
  public static System.Random rnd = new System.Random();
  public List<GameObject> roomPieces;
  public List<GameObject> floorPieces;
  public GameObject ladder;
  public GameObject enemy;
  public float roomPieceWidth = 5;
  public float roomPieceHeight = 5;
  public float roomWidth = 20;
  private float ladderMargin = 3.0f;
  public int floorNum;
  public int roomID;
  public static GameObject Create(GameObject parentFloor, int roomID, int floorNum, int roomCount, bool hasEnemy=false)
  {
    if (prefab == null)
      prefab = Resources.Load("Prefabs/Room");
    GameObject go = Instantiate(prefab) as GameObject;
    RandomRoom rr = go.GetComponent<RandomRoom>();
    RoomLUT roomLUT = go.GetComponent<RoomLUT>();
    rr.floorNum = floorNum;
    rr.roomID = roomID;
    go.transform.SetParent(parentFloor.transform);
    var numRoomPieces = rr.roomWidth / rr.roomPieceWidth;
    float offset = 0.0f;
    if (roomCount % 2 == 0)
      offset = rr.roomWidth / 2;
    float floorWidth = rr.roomWidth * roomCount;
    bool evenFloor = floorNum % 2 == 0;
    if(evenFloor)
      go.transform.localPosition = new Vector3(-floorWidth / 2 + roomID * rr.roomWidth + offset, 0, 0);
    else
      go.transform.localPosition = new Vector3(floorWidth / 2 - roomID * rr.roomWidth - offset, 0, 0);

    offset = 0.0f;
    if (numRoomPieces % 2 == 0)
      offset = rr.roomPieceWidth / 2;
    int randomIdx;
    for (int i = 0; i < numRoomPieces; i++)
    {    
      GameObject roomPiece = null;
      if (i == 0)
      {
        // instantiate left piece
        if ((!evenFloor && floorNum > 1 && roomID == 0) ||
              (evenFloor && roomID == roomCount - 1))
        {
          roomPiece = Instantiate(roomLUT.floorLeftEnd);
        }
        else
        {
          randomIdx = rnd.Next(roomLUT.leftPieces.Count);
          roomPiece = Instantiate(roomLUT.leftPieces[randomIdx]);
        }
      }
      else if (i == numRoomPieces - 1)
      {
        // Instantiate right piece
        if ((!evenFloor && roomID == roomCount - 1) ||
              (evenFloor && roomID == 0))
        {
          roomPiece = Instantiate(roomLUT.floorRightEnd);
        }
        else
        {
          randomIdx = rnd.Next(roomLUT.rightPieces.Count);
          roomPiece = Instantiate(roomLUT.rightPieces[randomIdx]);
        }
        
      }
      else
      {
        // instantiate center
        if (RandomRoom.IsCamp(roomID, floorNum))
        {
          if(i == 1)
            roomPiece = Instantiate(roomLUT.campPiece1);
          else if(i == 2)
            roomPiece = Instantiate(roomLUT.campPiece2);
        }
        else
        {
          randomIdx = rnd.Next(roomLUT.centerPieces.Count);
          roomPiece = Instantiate(roomLUT.centerPieces[randomIdx]);
        }
        
        
      }
      roomPiece.transform.SetParent(go.transform);
      rr.roomPieces.Add(roomPiece);
      roomPiece.transform.localPosition = new Vector3(rr.roomWidth / 2 - i * rr.roomPieceWidth - offset, 0, 0);

      randomIdx = rnd.Next(roomLUT.floorPieces.Count);
      GameObject floorPiece = Instantiate(roomLUT.floorPieces[randomIdx]);
      floorPiece.transform.SetParent(go.transform);
      rr.floorPieces.Add(floorPiece);
      floorPiece.transform.localPosition = new Vector3(rr.roomWidth / 2 - i * rr.roomPieceWidth - offset, -rr.roomPieceHeight, 0);
    }
    if (roomID == roomCount - 1)
    {
      // Add ladder
      rr.ladder = Instantiate(roomLUT.ladder);
      rr.ladder.transform.SetParent(go.transform);
      if (evenFloor)
      {
        rr.ladder.transform.localPosition = new Vector3(rr.roomWidth / 2 - rr.ladderMargin, -0.1f, -0.5f);
        rr.ladder.transform.Rotate(new Vector3(0, 180, 0));
      }
      else
        rr.ladder.transform.localPosition = new Vector3(-rr.roomWidth / 2 + rr.ladderMargin, -0.1f, -0.5f);
    }
    if (hasEnemy && !RandomRoom.IsCamp(roomID, floorNum))
    {
      randomIdx = rnd.Next(roomLUT.enemies.Count);
      rr.enemy = Instantiate(roomLUT.enemies[randomIdx]);
      rr.enemy.transform.SetParent(go.transform);
      rr.enemy.transform.localPosition = new Vector3(0, 0.0f, 0);
      if (evenFloor)
        rr.enemy.transform.Rotate(new Vector3(0, -90, 0));
      else
        rr.enemy.transform.Rotate(new Vector3(0, 90, 0));
    }

    return go;
  }
  public static bool IsCamp(int roomID, int floorNum)
  {
    if (roomID == 0 && floorNum > 1)
      return true;
    else
      return false;
  }
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}