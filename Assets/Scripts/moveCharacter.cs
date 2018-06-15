﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCharacter : MonoBehaviour {

	public float moveSpeed = 2f;
	public bool nextFloor = false;
	public bool moveNext = false;
	public Vector3 right;
	public Vector3 left;
  private Animator animator;
  private int moveHash = Animator.StringToHash("Move");
  private int walkHash = Animator.StringToHash("GuardedWalk");
  private int combatHash = Animator.StringToHash("CombatStart");
  // Update is called once per frame
  void Start(){
		Debug.Log(transform.position);
    animator = GetComponent<Animator>();
    moveSpeed = 0f;
    Invoke("StartMoving", 3f);
	}
	void FixedUpdate () {
		if (moveNext == true){
			Next();
			GameObject.Find("TowerManager").GetComponent<LevelManager>().LevelUp();
		}

		if(nextFloor == true){
			transform.Translate  (left * Time.deltaTime * moveSpeed, Space.World);
		}
		else
			transform.Translate  (right * Time.deltaTime * moveSpeed, Space.World);
	}
	public void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Enemy"){
			Debug.Log(col.collider.tag);
		}
		if(col.collider.tag == "Ladder"){
			moveNext = true;
		}
		
	}

  public void StartMoving()
  {
    moveSpeed = 2f;
    animator.SetTrigger("Move");
  }
  public void ApproachEnemy()
  {
    moveSpeed = 1f;
    animator.SetTrigger("GuardedWalk");
  }
  public void CombatStart(GameObject enemy)
  {
    moveSpeed = 0f;
    animator.SetTrigger("CombatStart");
    GameObject.Find("WordManager").GetComponent<WordDisplay>().NewWord(enemy);
    GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat = true;
  }
  public void Attack()
  {

  }
  public void Die()
  {
    moveSpeed = 0f;
  }
  public void Next(){
		Debug.Log(nextFloor);
    transform.Translate(new Vector3(0, 10, 0), Space.World);
    if (nextFloor == true){
      transform.Rotate(new Vector3(0, 180, 0));
			nextFloor = false;
		}
		else{
      transform.Rotate(new Vector3(0, 180, 0));
			nextFloor = true;
		}
		Debug.Log(transform.position);
		moveNext = false;
	}
}
