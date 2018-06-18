using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public float life;
  // Use this for initialization

  public void Start()
  {
  }
  /**
   * Returns true if dead. 
   **/
  public bool TakeDamage(float damage){
		life -= damage;
		//var takeDam = (damage / 10);
		//Debug.Log(takeDam);
		GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<ManageEnemyHealthBar>().Damage(damage);
    if (life <= 0)
    {
      life = 0;
      return true;
    }
    else
      return false;
	}

}
