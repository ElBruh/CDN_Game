using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardedTrigger : MonoBehaviour {

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
    if (other.gameObject.tag == "Enemy")
      heroController.ApproachEnemy();
  }
}
