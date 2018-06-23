using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncouragementPopup : MonoBehaviour {

  public Text messageText;
  public Text signatureText;
  public InputField messageInput;
  public InputField signatureInput;

	// Use this for initialization
	void Start () {
    messageText.text = "";
    signatureText.text = "";
    StartCoroutine(MessageService.GetRandomMessage(message =>
    {
      messageText.text = message.text;
      signatureText.text = "- " + message.author;
    }));
    Cursor.visible = true;
  }
	
	// Update is called once per frame
	void Update () {
    
  }

  public void SendEncouragement()
  {
    var noteText = messageInput.text;
    var fromText = signatureInput.text;
    if(noteText != "" && noteText != null)
    {
      Message msg = new Message(noteText, fromText);
      StartCoroutine(MessageService.PostMessage(msg));
    }
    ClosePopup();
  }
  public void ClosePopup()
  {
    messageText.text = messageInput.text = signatureInput.text = signatureText.text = "";
    GameObject.FindGameObjectWithTag("Hero").GetComponent<moveCharacter>().StartMoving();
    Cursor.visible = false;
    Destroy(this.gameObject);
  }
}
