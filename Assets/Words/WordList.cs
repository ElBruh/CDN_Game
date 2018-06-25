using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class WordList {
	//this will hold the words that we want to display
	//we can choose the words later on, this is just for testing
	
	private string oldDifficulty;
	public List<string> words;
	//public string Difficulty = "Medium";
	private string path1,path2;
	public string GetWord(string Difficulty){
		//I only want thise to be called when the Difficulty changes(for now).
		if(oldDifficulty != Difficulty){
			ChooseWords(Difficulty);
			oldDifficulty = Difficulty;
		}
		//will choose a random word
		var wordIndex = Random.Range(0, words.Count);
		return words[wordIndex];
	}

	public void ChooseWords(string Difficulty){
		Debug.Log("ChooseWords has been called");
		words.Clear();
		switch(Difficulty){
		case "Easy" :
			path1 = "Words/three_letter";
			path2 = "Words/four_letter";
			break;
		case "Medium" :
			path1 = "Words/five_letter";
			path2 = "Words/six_letter";
			break;
		case "Hard" :
			path1 = "Words/seven_letter";
			path2 = "Words/eight_letter";
			break;
		}

    TextAsset textAsset = Resources.Load(path1) as TextAsset;
    words.AddRange(textAsset.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None));

    textAsset = Resources.Load(path2) as TextAsset;
    words.AddRange(textAsset.text.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None));

		//Debug.Log(words[0]);

		//Debug.Log(words[464]);
	}

}

[System.Serializable]
public class EncouragementsList
{
  private string[] phrases = {"keep going", "you can do it", "never give up", "you're doing well",  "keep it up", "good job", "well done",
  "everything you need is already in you", "everything will work out", "do your best", "proud of you", "stay positive", "don't be discouraged",
    "don't be afraid", "look at how far you've come", "you've come a long way", "i'm cheering for you", "you are stronger than you know",
    "you are unstoppable", "just a little at a time", "don't stop trying", "don't think just do", "set your goals high", "believe that you can",
    "it can be done", "make a new ending", "keep pressing on", "just move forward", "one step at a time", "never never never give up",
    "triumph begins with you", "believe in yourself", "do it against all odds", "the monsters are tough but you are tougher"};



  public string GetPhrase()
  {
    //will choose a random word
    var wordIndex = Random.Range(0, phrases.Length);
    return phrases[wordIndex];
  }

}

