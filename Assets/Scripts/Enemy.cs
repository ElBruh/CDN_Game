using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  private Animator animator;
  private int idleHash = Animator.StringToHash("Idle");
  private int attackHash = Animator.StringToHash("Attack");
  private int dieHash = Animator.StringToHash("Die");
  private int blockHash = Animator.StringToHash("Block");

  // Use this for initialization
  void Start () {
    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public void HitHero()
  {
    GameObject.Find("WordManager").GetComponent<CombatManager>().HeroHitByEnemy();
  }
  public void Attack()
  {
    animator.SetTrigger(attackHash);
  }
  public void Idle()
  {
    animator.SetTrigger(idleHash);
  }
  public void Die()
  {
    animator.SetTrigger(dieHash);
  }

  public void Block()
  {
    animator.SetTrigger(blockHash);
  }

}
