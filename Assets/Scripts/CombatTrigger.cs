using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour {

  private moveCharacter heroController;
  // Use this for initialization
  void Start () {
    heroController = GetComponentInParent<moveCharacter>();
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  public void OnTriggerEnter(Collider other)
  {
    // Because re-collision can happen.  Check if we're in combat too.
    if (other.gameObject.tag == "Enemy" && !GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat)
      heroController.CombatStart(other.gameObject);
  }
}
