using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour{

	public AudioClip tap;
	public AudioClip badTap;
	public AudioClip finish;
	public AudioClip win;
 	public AudioClip introSong;
	public AudioClip swordClash;
	public AudioClip block;
	public AudioClip fireSound;
	
	public AudioClip runSound;
	public AudioSource source;
	public AudioSource EnemyAttack;
  	//public AudioSource EncounterMusic;
  	public AudioSource MainMusic;
	// Use this for initialization
	void Start () {
		/*We can decide when to start and end the intro music */
		//MainMusic.clip = introSong;
    	//MainMusic.Play();
	}
	
	// Update is called once per frame
	void Update () {
		/*if(changeMusic == true){
    		MainMusic.volume -= 1 * Time.deltaTime/2;
    		EncounterMusic.volume += 0.5f * Time.deltaTime/2;
    	}
    	else if (changeMusic == false){
      		MainMusic.volume += 0.5f * Time.deltaTime/2;
      		EncounterMusic.volume -= 1 * Time.deltaTime/2;
    	}*/
	}
	public void Tap(){
		/*Finished Letter */
        source.clip = tap;
        source.Play();
	}
	public void BadTap(){
		/*Wrong Letter */
        source.clip = badTap;
        source.Play();
	}
	public void EnemyDeadSound(){
		/*Defeated Enemy */
		source.clip = win;
		source.Play();
	}	
	public void FinishWord(){
		/*Finished Word */
        source.clip = finish;
        source.Play();
	}
	public void RunSound(){
		//source.clip = runSound;
		//source.loop = true;
		//source.Play();
	}
	public void SneakSound(){
		
	}
	public void SwordClash(){
		source.clip = swordClash;
		source.Play();
	}
	public void Block(){
		source.clip = block;
		source.Play();
	}
	public void BlockSound(){

	}
	public void ClimbSound(){

	}
	public void FireSound(){
		EnemyAttack.clip = fireSound;
		EnemyAttack.Play();
	}
}
