using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public float life = 100f;
	// Use this for initialization


	public void TakeDamage(float damage){
		life -= damage;
		//var takeDam = (damage / 10);
		//Debug.Log(takeDam);
		GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<ManageEnemyHealthBar>().Damage(damage);
		if(life <= 0){
			Die();
		}
	}

	public void Die(){
		life = 0;
		GameObject.Find("WordManager").GetComponent<WordDisplay>().mText.text = "";
		GameObject.Find("WordManager").GetComponent<WordDisplay>().ChangeToDead();
		GameObject.Find("Main Camera").GetComponent<FollowPlayer>().inCombat = false;
		GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<ManageEnemyHealthBar>().Die();
		Destroy(gameObject);
    GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().StartMoving();
  }
}
