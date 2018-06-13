using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class WordDisplay : MonoBehaviour {


	public float health = 100f;
	public List<string> ListOfWords;
	public AudioClip tap;
	public AudioClip badTap;
	public AudioClip finish;
	public AudioClip win;
	public AudioSource source;
	public TextMeshPro mText;
	public TextMesh healthBarV2PreFab;
	public TextMesh healthBarV2;
	//private GameObject enemy;
	//public GameObject enemyPrefab;
	public TextMeshPro mTextPrefab;
	//private TextMesh word;

	public WordList list;
	public Camera cam;
	public GameObject tempEnemy;
	private bool currentLetter = false;
	public bool wordExists = false;
	private float timeleft = 10;
	public float timer;
	public int damage = 0;
	//private bool currentWord = false;

	void Start(){
		//InvokeRepeating("NewWord", 0, 3);
		//NewWord();
		timer = timeleft;
	}
	void Update(){
		cam.backgroundColor = Color.black;

		//mText.transform.position = new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z);

		//I want to get the keyboard strokes and compare them between the characters that
		//exist in the current word (later on WORDS).

		

		if(wordExists == true){

			timer-=Time.deltaTime;

			if(timer <= 0){
				tempEnemy.GetComponent<Life>().TakeDamage(damage);
				damage = 0;
				timer = timeleft;
			}

			foreach (char c in Input.inputString){
			if (c == mText.text[0]){
			currentLetter = true;
			}
			else{
				source.clip = badTap;
				source.Play();
				WrongLetter();
			}
				

		}
		//If the current pressed key is the first letter of a word, it will be deleted
			if (currentLetter == true){
				mText.text = mText.text.Remove(0,1);
				source.clip = tap;
				source.Play();


				ChangeColor();
			
				//Debug.Log(mText.text);
				currentLetter = false;
			}
			//if the word no longer exists, it will spawn a new one
			if(mText.text.Length == 0){
				
				source.clip = finish;
				source.Play();
				
				//KillEnemy();
				Materialize();
				//wordExists = false;
				//NewWord();
			}
		}
		else
			timer = timeleft;
			
		
	}

	//will call the GetWord function from the WordList class to get a new word
	public void NewWord(GameObject parent){
		
		//mText = GetComponent<TextMeshPro>();
		//enemy = Instantiate(enemyPrefab);
		tempEnemy = parent;
		mText = Instantiate(mTextPrefab, new Vector3(parent.transform.position.x,parent.transform.position.y+0.5f,parent.transform.position.z), Quaternion.Euler(0,-123.688f,0));
		if(wordExists == false){
			healthBarV2 = Instantiate(healthBarV2PreFab, new Vector3(mText.transform.position.x,mText.transform.position.y+1.25f,mText.transform.position.z), Quaternion.Euler(0,-123.688f,0));
			healthBarV2.text = "aaaaaaaaaa";
		}
		mText.text = list.GetWord();
		//ListOfWords.Add(mText.text);
		wordExists = true;
	}

	void Materialize(){
		damage+=10;
		NewWord(tempEnemy);
		//Do something cool here, or call another class to do it
		
	}

	void ChangeColor(){
		//Can set an active color for this word.
		//mText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	public void ChangeToDead(){
		wordExists = false;
		source.clip = win;
		source.Play();
	}
	void WrongLetter(){
		health -= 10f;
		if(health <=0){
			SceneManager.LoadScene(0);
		}
		//Debug.Log("Wrong Letter");
		//falsh the screen red for a little
		cam.backgroundColor = Color.red;
		
		//cam.backgroundColor = Color.black;
	}
	
	
	
	
}
