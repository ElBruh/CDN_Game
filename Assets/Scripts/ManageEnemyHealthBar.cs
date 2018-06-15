using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageEnemyHealthBar : MonoBehaviour {
	public Slider slide;

	void Start(){
		slide.maxValue = GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life;
		slide.value =  GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life;
	}
	public void Die(){
		Destroy(transform.parent.gameObject);
	}

	public void Damage(float damage){
		//Debug.Log("healthTotake: " + damage);
		//Debug.Log("Characters: " + word.text.Length);
		//slide.maxValue = GameObject.Find("WordManager").GetComponent<WordDisplay>().tempEnemy.GetComponent<Life>().life;
		if(damage > slide.value){
			damage = slide.value;
		}
		//word.text = word.text.Remove(0, damage);
		slide.value -= damage;
		//Debug.Log(slide.value);
	}
}
