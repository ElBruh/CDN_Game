using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

  private Animator animator;
  private int idleHash = Animator.StringToHash("Idle");
  private int attackHash = Animator.StringToHash("Attack");
  private int dieHash = Animator.StringToHash("Die");
  private int blockHash = Animator.StringToHash("Block");
  public ParticleSystem attackPS;

  // Use this for initialization
  void Start () {
    animator = GetComponent<Animator>();
    attackPS = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  public void HitHero()
  {
    GameObject.Find("ObstacleManager").GetComponent<CombatManager>().HeroHitByEnemy();
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
  public void StartAttackEffect()
  {
    attackPS.Play();
  }

  public void StopAttackEffect()
  {
    attackPS.Stop();
  }

}
