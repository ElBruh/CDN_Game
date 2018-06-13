using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordList {
	//this will hold the words that we want to display
	//we can choose the words later on, this is just for testing
	private string[] words = {"hands","brash","young","battle","hobbies","selective","near","shivering","half","garrulous","mark","oil","attack","object","spotty",
							"snobbish","note","humdrum","frighten","truck","tumble","night","stranger","consist","sound","scold","aquatic","name","babies","hope",
							"jobless","competition","riddle","direful","kindhearted","question","books","prepare","uncovered","mean","star","brawny","cute","sidewalk",
							"caring","nifty","whine","old","reading","ear"};

	

	public string GetWord(){
		//will choose a random word
		var wordIndex = Random.Range(0, words.Length);
		return words[wordIndex];
	}

}

