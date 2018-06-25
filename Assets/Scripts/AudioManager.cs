using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour{

	public AudioClip tap;
	public AudioClip badTap;
	public AudioClip finish;
	public AudioClip win;
 	public AudioClip introSong;
	public AudioClip gameMusic;
	public AudioClip swordClash;
	public AudioClip block;
	public AudioClip fireSound;
	public AudioClip deadSong;
	public AudioClip startGame;
	public AudioClip runSound;
	public AudioSource source;
	public AudioSource EnemyAttack;
  	//public AudioSource EncounterMusic;
  	public AudioSource MainMusic;
	public float timeToPlay;
	private bool playGameMusic = false;
	// Use this for initialization
	void Start () {
		/*We can decide when to start and end the intro music */
		MainMusic.clip = introSong;
    	MainMusic.Play();
		source.volume = 1f;
		//MainMusic.volume = 1f;
		timeToPlay = 100000f;
	}
	
	// Update is called once per frame
	void Update () {
		timeToPlay -= Time.deltaTime;
		if (timeToPlay <= 0 && playGameMusic == false){
			source.volume = 0.57f;
			MainMusic.volume-=0.01f;
			if(MainMusic.volume <= 0.001f){
				MainMusic.Stop();
				MainMusic.clip = gameMusic;
				MainMusic.volume = 0.75f;
				MainMusic.Play();
				playGameMusic = true;
			}
		}
	}
	public void StartGame(){
		//source.clip = startGame;
		//source.Play();
		
		Debug.Log("StartGame in AudioManager has been called");
		timeToPlay = 15.75f;
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
	public void PlayerDead(){
		MainMusic.Stop();
		source.clip = deadSong;
		source.Play();
	}
	public void SwordClash(){
		source.clip = swordClash;
		source.Play();
	}
	public void Block(){
		source.clip = block;
		source.Play();
	}
	public void ClimbSound(){

	}
	public void FireSound(){
		EnemyAttack.clip = fireSound;
		EnemyAttack.Play();
	}
}
