using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour {
  public TextMeshProUGUI text;
  public Image blackScreen;
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public IEnumerator FadeToBlack(float fadeDuration, float delayTime)
  {
    yield return new WaitForSeconds(delayTime);
    blackScreen.color = Color.black;
    blackScreen.canvasRenderer.SetAlpha(0.0f);
    blackScreen.CrossFadeAlpha(1.0f, fadeDuration, false);
  }

  public IEnumerator FadeFromBlack(float fadeDuration, float delayTime)
  {
    yield return new WaitForSeconds(delayTime);
    
    blackScreen.color = Color.black;
    blackScreen.canvasRenderer.SetAlpha(1.0f);
    blackScreen.CrossFadeAlpha(0.0f, fadeDuration, false);
  }

  public IEnumerator FadeInText(string inText, float fadeDuration, float delayTime)
  {
    yield return new WaitForSeconds(delayTime);
    text.text = inText;
    text.canvasRenderer.SetAlpha(0.0f);
    text.CrossFadeAlpha(1.0f, fadeDuration, false);
  }

  public IEnumerator FadeOutText(float fadeDuration, float delayTime)
  {
    yield return new WaitForSeconds(delayTime);
    text.canvasRenderer.SetAlpha(1.0f);
    text.CrossFadeAlpha(0.0f, fadeDuration, false);
  }

}
