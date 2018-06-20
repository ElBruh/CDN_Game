using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCharacter : MonoBehaviour {

	public float moveSpeed = 0f;
	public bool nextFloor = false;
	public bool moveNext = false;
	public Vector3 right;
	public Vector3 left;
  private bool climbing;
  private int climbingIterations;
  private Animator animator;
  private int moveHash = Animator.StringToHash("Move");
  private int walkHash = Animator.StringToHash("GuardedWalk");
  private int combatHash = Animator.StringToHash("CombatStart");
  private int dieHash = Animator.StringToHash("Die");
  private int blockHash = Animator.StringToHash("Block");
  private int attackHash = Animator.StringToHash("Attack");
  private int climbLeftHash = Animator.StringToHash("ClimbLeft");
  private int climbRightHash = Animator.StringToHash("ClimbRight");
  private GameObject player;
  private bool climbStairs = false;
  // Update is called once per frame
  void Start(){
		Debug.Log(transform.position);
    animator = GetComponent<Animator>();
    moveSpeed = 0f;
    climbingIterations = 0;
    player = GameObject.FindGameObjectWithTag("Hero");
    StartMoving();
	}
  public void HitEnemy()
  {
    GameObject.Find("ObstacleManager").GetComponent<CombatManager>().EnemyHitByHero();
  }
  void FixedUpdate () {
    if(climbing)
    {
      transform.Translate(new Vector3(0,0.5f,0) * Time.deltaTime, Space.World);
    }
		else if(nextFloor == true){
			transform.Translate  (left * Time.deltaTime * moveSpeed, Space.World);
		}
		else
			transform.Translate  (right * Time.deltaTime * moveSpeed, Space.World);
    if(climbStairs == true && player.transform.position.y <= 5){
      player.transform.Translate(0,2.5f * Time.deltaTime,0);
    }
	}

	public void OnCollisionEnter(Collision col){
		if(col.collider.tag == "Enemy"){
			Debug.Log(col.collider.tag);
		}
		if(col.collider.tag == "Ladder"){
      //moveNext = true;
      LadderStart(col.collider.gameObject);
    }
    if (col.gameObject.tag == "Stairs"){
      climbStairs = true;
    }
	}
  public void ClimbingIter()
  {
    climbingIterations++;
    if(climbingIterations == 1)
    {
      climbing = false;
      climbingIterations = 0;
      GameObject.Find("ObstacleManager").GetComponent<LadderManager>().FloorTransition();
    }
  }
  public void StartMoving()
  {
    animator.SetTrigger(moveHash);
  }
  public void ApproachEnemy()
  {
    moveSpeed = 2f;
    animator.SetTrigger(walkHash);
  }
  public void CombatStart(GameObject enemy)
  {
    moveSpeed = 0f;
    animator.SetTrigger(combatHash);
    GameObject.Find("ObstacleManager").GetComponent<CombatManager>().CombatStart(this.gameObject, enemy);
    GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat = true;
  }

  public void LadderStart(GameObject ladder)
  {
    moveSpeed = 0f;
    animator.SetTrigger(combatHash);
    GameObject.Find("ObstacleManager").GetComponent<LadderManager>().LadderStart(this.gameObject, ladder);
  }

  public void Climb()
  {
    climbing = true;
    FloorDirections direction = GameObject.Find("TowerManager").GetComponent<LevelManager>().GetCurrentFloorDirection();
    if (direction == FloorDirections.Left)
      animator.SetTrigger(climbLeftHash);
    else
      animator.SetTrigger(climbRightHash);
  }
  public void Attack()
  {
    Debug.Log("Performing attack!");
    moveSpeed = 0f;
    animator.SetTrigger(attackHash);
  }
  public void Die()
  {
    moveSpeed = 0f;
    animator.SetTrigger(dieHash);
  }
  public void Block()
  {
    animator.SetTrigger(blockHash);
  }
  public void Next(){
		Debug.Log(nextFloor);
    GameObject.Find("TowerManager").GetComponent<LevelManager>().LevelUp();
    animator.SetTrigger(combatHash);
    var floorY = GameObject.Find("TowerManager").GetComponent<LevelManager>().GetCurrentFloorY();
    transform.position = new Vector3(transform.position.x, floorY, transform.position.z);
    if (nextFloor == true){
      transform.Rotate(new Vector3(0, 180, 0));
			nextFloor = false;
		}
		else{
      transform.Rotate(new Vector3(0, 180, 0));
			nextFloor = true;
		}
		Debug.Log(transform.position);
    
    StartMoving();
    moveSpeed = 4;
  }
}
