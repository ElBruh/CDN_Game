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
							"caring","nifty","whine","old","reading","ear","fix","laugh","throat","friends","lumpy","war","top","steer","wink","heady","ultra","bewildered",
							"sock","wakeful","basin","modern","relieved","north","tasteless","earsplitting","cut","embarrass","rub","feeble","visit","waves","snakes","frighten",
							"remind","hollow","substance","ethereal","overflow","expand","stem","reflect","history","agreeable","straw","silk","flap","desert","therapeutic","parallel",
							"pencil","canvas","peaceful","defiant","thick","lie","airport","amused","meat","handle","deafening","tightfisted","bomb","truck","occur","trot","wet",
							"offer","blow","gabby","obtainable","applaud","end","rule","border","steam","grain","coach","excellent","jeans","order","aspiring","prepare","curtain",
							"wealthy","remain","downtown","planes","abrasive","rich","tiny","scene","boat","quack","cub","clever","sheet","alert","need","ruddy","volcano","nose","knowing",
							"book","load","uptight","heavy","melted","painful","butter","spot","bed","part","silver","habitual","hapless","poor","pull","heartbreaking","spark","tough","pass",
							"nappy","learn","zoo","trick","run","shoes","post","crow","stroke","whistle","stream","ambitious","chief","sugar","box","physical","vengeful","stale","cracker","wave",
							"calculate","rain","island","scissors","argue","flat","tan","slave","lock","float","waste","soda","bite"};

	

	public string GetWord(){
		//will choose a random word
		var wordIndex = Random.Range(0, words.Length);
		return words[wordIndex];
	}

}

[System.Serializable]
public class EncouragementsList
{
  private string[] phrases = {"keep going", "you can do it", "never give up", "you're doing well",  "keep it up", "i believe in you", "good job"};



  public string GetPhrase()
  {
    //will choose a random word
    var wordIndex = Random.Range(0, phrases.Length);
    return phrases[wordIndex];
  }

}

