﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEndSMB : StateMachineBehaviour
{

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {

  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    GameObject.Find("CombatManager").GetComponent<CombatManager>().ResolveEnemyAttack();
  }
}
