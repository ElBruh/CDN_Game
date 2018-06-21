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
  public TextMeshProUGUI floorText;
  private GameObject hero;
  private GameObject ladder;
  public LadderStates ladderState;
  public Image blackScreen;
  private WordDisplay wordDisplay;
  private List<string> encouragePhrase;


  void Start()
  {
    wordDisplay = GetComponent<WordDisplay>();
    ladderState = LadderStates.Away;
    encouragements = new EncouragementsList();
  }

  public void OnTextCompleted()
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
    ladderState = LadderStates.Transition;
    FadeToBlack();

  }

  void FadeToBlack()
  {
    blackScreen.color = Color.black;
    blackScreen.canvasRenderer.SetAlpha(0.0f);
    blackScreen.CrossFadeAlpha(1.0f, 0.5f, false);
    Invoke("FadeInFloorText", 1.5f);
    Invoke("FadeFromBlack", 1f);
  }

  void FadeFromBlack()
  {
    hero.GetComponent<moveCharacter>().Next();
    blackScreen.color = Color.black;
    blackScreen.canvasRenderer.SetAlpha(1.0f);
    blackScreen.CrossFadeAlpha(0.0f, 0.5f, false);
  }
  void FadeInFloorText()
  {
    floorText.text = "Floor " + GameObject.Find("TowerManager").GetComponent<LevelManager>().currentFloor.ToString();
    floorText.canvasRenderer.SetAlpha(0.0f);
    floorText.CrossFadeAlpha(1.0f, 0.5f, false);
    Invoke("FadeOutFloorText", 1f);
  }
  void FadeOutFloorText()
  {
    //floorText.text = "";
    floorText.canvasRenderer.SetAlpha(1.0f);
    floorText.CrossFadeAlpha(0.0f, 0.5f, false);
  }

}
