using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageServiceTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
    StartCoroutine(MessageService.GetRandomMessage(message => { Debug.Log(message.text); }));
    StartCoroutine(MessageService.PostMessage(new Message("Test", "Aileen")));
  }
	
	// Update is called once per frame
	void Update () {
		
	}
}
