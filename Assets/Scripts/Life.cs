using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	public float life;
	// Use this for initialization
	void Start () {
		life = 100;
	}

	public void TakeDamage(int damage){
		life -= damage;
		if(life <= 0){
			Die();
		}
	}
	void Die(){
		life = 0;
		Destroy(gameObject);
		GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().stopMoving = false; 
		Destroy(GameObject.Find("WordManager").GetComponent<WordDisplay>().mText);
		GameObject.Find("WordManager").GetComponent<WordDisplay>().wordExists = false;
		
	}
}
