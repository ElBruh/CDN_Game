using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEndSMB : StateMachineBehaviour {

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    GameObject.Find("ObstacleManager").GetComponent<CombatManager>().EnemyDeathCleanUp();
  }

}
