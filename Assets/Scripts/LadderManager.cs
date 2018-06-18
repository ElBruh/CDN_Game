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
  private GameObject hero;
  private GameObject ladder;
  public LadderStates ladderState;
  public Image blackScreen;
  private WordDisplay wordDisplay;


  void Start()
  {
    wordDisplay = GetComponent<WordDisplay>();
    ladderState = LadderStates.Away;
    encouragements = new EncouragementsList();
  }

  public void OnTextCompleted()
  {
    wordDisplay.StopDisplay();
    ladderState = LadderStates.ClimbingLadder;
    hero.GetComponent<moveCharacter>().Climb();
  }
  void Update()
  {
    switch (ladderState)
    {
      case LadderStates.InputSetup:
        wordDisplay.NewText(ladder.transform, encouragements.GetPhrase());
        ladderState = LadderStates.TakingInput;
        break;


      default:
        break;
    }


  }


  void ChangeColor()
  {
    //Can set an active color for this word.
    //mText.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
  }


  public void LadderStart(GameObject hero, GameObject ladder)
  {
    this.hero = hero;
    this.ladder = ladder;
    wordDisplay.StartDisplay(OnTextCompleted, timerLength: 0f);
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
    Invoke("FadeFromBlack", 1f);
  }

  void FadeFromBlack()
  {
    hero.GetComponent<moveCharacter>().Next();
    blackScreen.color = Color.black;
    blackScreen.canvasRenderer.SetAlpha(1.0f);
    blackScreen.CrossFadeAlpha(0.0f, 0.5f, false);
  }

}
