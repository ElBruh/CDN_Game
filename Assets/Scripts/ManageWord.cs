using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageWord : MonoBehaviour {
	public TextMeshPro word;
	void Update () {
		//if the current word no longer has letters, it will destro itself
		if (word.text.Length == 0){
			Die();
		}	
	}
	public void Die(){
		Destroy(gameObject);
	}
}
