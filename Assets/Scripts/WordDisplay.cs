using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordDisplay : MonoBehaviour {

	private TextMeshPro mText;
	//private GameObject enemy;
	//public GameObject enemyPrefab;
	public TextMeshPro mTextPrefab;
	//private TextMesh word;

	public WordList list;
	public Camera cam;
	private bool currentWord = false;

	void Start(){
		//InvokeRepeating("NewWord", 0, 3);
		NewWord();

	}
	void Update(){
		cam.backgroundColor = Color.black;

		//mText.transform.position = new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z);

		//I want to get the keyboard strokes and compare them between the characters that
		//exist in the current word (later on WORDS).
		foreach (char c in Input.inputString){
			if (c == mText.text[0]){
			currentWord = true;
			}
			else
				WrongLetter();

		}
		//If the current pressed key is the first letter of a word, it will be deleted
		if (currentWord == true){
			mText.text = mText.text.Remove(0,1);
			ChangeColor();
			
			//Debug.Log(mText.text);
			currentWord = false;
		}
		//if the word no longer exists, it will spawn a new one
		if(mText.text.Length == 0){
			Materialize();
			NewWord();
		}
		
	}

	//will call the GetWord function from the WordList class to get a new word
	void NewWord(){
		
		//mText = GetComponent<TextMeshPro>();
		//enemy = Instantiate(enemyPrefab);
		mText = Instantiate(mTextPrefab);
		mText.text = list.GetWord();
	}

	void Materialize(){
		//Do something cool here, or call another class to do it
	}

	void ChangeColor(){
		//Can set an active color for this word.
		//mText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
	}
	void WrongLetter(){
		Debug.Log("Wrong Letter");
		//falsh the screen red for a little
		cam.backgroundColor = Color.red;
		//cam.backgroundColor = Color.black;
	}
	
	
	
	
}
