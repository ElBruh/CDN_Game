using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemyHealthBar : MonoBehaviour {

	public TextMesh word;

	
	public void Die(){
		Destroy(gameObject);
	}

	public void Damage(int damage){
		Debug.Log("healthTotake: " + damage);
		Debug.Log("Characters: " + word.text.Length);
		if(damage > word.text.Length){
			damage = word.text.Length;
		}
		word.text = word.text.Remove(0, damage);
		
	}
}
