using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum LadderStates
{
  InputSetup,
  TakingInput,
  ApproachingLadder,
  ClimbingLadder,
  Transition,
  NextFloor,
  Away
}

public class LadderManager : MonoBehaviour
{

  public Camera cam;

  public EncouragementsList encouragements;
  public GameObject blackScreen;
  private GameObject hero;
  private GameObject ladder;
  public LadderStates ladderState;
  private WordDisplay wordDisplay;
  private List<string> encouragePhrase;


  void Start()
  {
    wordDisplay = GetComponent<WordDisplay>();
    ladderState = LadderStates.Away;
    encouragements = new EncouragementsList();
  }

  public void OnTextCompleted(int wordLength)
  {
    if(encouragePhrase.Count > 0)
    {
      wordDisplay.NewText(ladder.transform, encouragePhrase[0]);
      encouragePhrase.RemoveAt(0);
    }
    else
    {
      wordDisplay.StopDisplay();
      ladderState = LadderStates.ClimbingLadder;
      hero.GetComponent<moveCharacter>().Climb();
    }
    
  }
  void Update()
  {
    switch (ladderState)
    {
      case LadderStates.InputSetup:
        wordDisplay.NewText(ladder.transform, encouragePhrase[0]);
        encouragePhrase.RemoveAt(0);
        ladderState = LadderStates.TakingInput;
        break;


      default:
        break;
    }


  }

  public void LadderStart(GameObject hero, GameObject ladder)
  {
    this.hero = hero;
    this.ladder = ladder;
    wordDisplay.StartDisplay(OnTextCompleted, timerLength: 0f);
    string[] splitString = encouragements.GetPhrase().Split(null);
    encouragePhrase = new List<string>(splitString);
    ladderState = LadderStates.InputSetup;
  }

  public void FloorTransition()
  {
    BlackScreen blackScreenComp = blackScreen.GetComponent<BlackScreen>();
    ladderState = LadderStates.Transition;
    StartCoroutine(blackScreenComp.FadeToBlack(0.5f, 0.0f));
    StartCoroutine(blackScreenComp.FadeFromBlack(0.5f, 1.0f));
    Invoke("NextFloor", 1.0f);
    string floorText = "Floor " + (GameObject.Find("TowerManager").GetComponent<LevelManager>().currentFloor + 1).ToString(); // floor + 1 because NextFloor gets called too late.
    StartCoroutine(blackScreenComp.FadeInText(floorText, 0.5f, 1.5f));
    StartCoroutine(blackScreenComp.FadeOutText(0.5f, 2.5f));
    
  }

  public void NextFloor()
  {
    hero.GetComponent<moveCharacter>().Next();
  }
  
}
