﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordDisplay : MonoBehaviour {

  public TextMeshPro mTextPrefab;
  public TextMeshPro mText;

  public bool wordExists = false;
  public float timer;
  public float timeleft = 10;
  public Camera cam;

  public delegate void TimerExpired();
  public delegate void WordCompleted();
  public delegate void IncorrectLetter();

  private bool currentLetter = false;
  private TimerExpired timerExpiredCallback;
  private WordCompleted textCompletedCallback;
  private IncorrectLetter incorrectLetterCallback;
  public AudioManager music;

  public void Start(WordCompleted textCompletedCallback, TimerExpired timerExpiredCallback = null, IncorrectLetter incorrectLetterCallback = null, float timerLength=10f)
  {
    this.textCompletedCallback = textCompletedCallback;
    this.timerExpiredCallback = timerExpiredCallback;
    this.incorrectLetterCallback = incorrectLetterCallback;
    timer = timeleft = timerLength;
  }

  public void Stop()
  {
    textCompletedCallback = null;
    timerExpiredCallback = null;
    incorrectLetterCallback = null;
    wordExists = false;
    mText.text = "";
  }
  public void Update()
  {
    if (wordExists == true)
    {

      timer -= Time.deltaTime;
      if (mText.text.Length == 0)
      {
        textCompletedCallback();
        music.FinishWord();
      }

      if (timer <= 0)
      {
        mText.text = "";
        wordExists = false;
        if(timerExpiredCallback != null)
          timerExpiredCallback();
      }

      foreach (char c in Input.inputString)
      {
        if (c == mText.text[0])
        {
          currentLetter = true;
          music.Tap();
        }
        else
        {
          music.BadTap();
          if (incorrectLetterCallback != null)
            incorrectLetterCallback();
        }


      }
      //If the current pressed key is the first letter of a word, it will be deleted
      if (currentLetter == true)
      {
        mText.text = mText.text.Remove(0, 1);
        
        currentLetter = false;
      }
      //if the word no longer exists, it will spawn a new one

    }
    else
      timer = timeleft;
  }

  public void SetTimer(float seconds)
  {
    timer = seconds;
  }
  //will call the GetWord function from the WordList class to get a new word
  public void NewText(Transform xform, string text)
  {

    cam.GetComponent<FollowPlayer>().rotOffset.x = -6.51f;
    mText = Instantiate(mTextPrefab, new Vector3(xform.position.x, xform.position.y + 1f, xform.position.z), Quaternion.Euler(0, -90, 0));
    
    mText.text = text;

    wordExists = true;
  }
}
