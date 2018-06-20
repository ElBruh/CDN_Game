using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class Message
{
  public string text;
  public string author;

  public Message(string text, string author)
  {
    this.text = text;
    this.author = author;
  }
  public static Message FromJson(string json)
  {
    return JsonUtility.FromJson<Message>(json);
  }
}

public class MessageService {
  private static string url_root = "http://encourage.codesalmon.com";
  public static IEnumerator GetRandomMessage(System.Action<Message> callback)
  {
    Message msg;

    UnityWebRequest request = UnityWebRequest.Get(url_root + "/messages/0/");
    yield return request.SendWebRequest();

    if (request.isNetworkError || request.isHttpError)
    {
      Debug.Log("Something went wrong, and returned error: " + request.error);
      msg = new Message("Hello World!", "Me");
    }
    else
    {
      // Show results as text
      Debug.Log(request.downloadHandler.text);

      if (request.responseCode == 200)
      {
        Debug.Log("Request finished successfully!");
        msg = Message.FromJson(request.downloadHandler.text);
      }
      else
      {
        Debug.Log("Request failed (status:" + request.responseCode + ")");
        msg = new Message("Hello World!", "Me");
      }
    }

    callback(msg);
  }


  public static IEnumerator PostMessage(Message msg, System.Action<bool> callback=null)
  {
    bool requestErrorOccurred = false;

    // convert message into json body
    string bodyJsonString = JsonUtility.ToJson(msg);
    var request = new UnityWebRequest(url_root+"/messages/", "POST");
    byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
    request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");

    yield return request.SendWebRequest();

    if (request.isNetworkError || request.isHttpError)
    {
      Debug.Log("Something went wrong, and returned error: " + request.error);
      requestErrorOccurred = true;
    }
    else
    {
      Debug.Log("Response: " + request.downloadHandler.text);

      if (request.responseCode == 201)
      {
        Debug.Log("Request finished successfully! New Message created successfully.");
      }
      else
      {
        Debug.Log("Request failed (status:" + request.responseCode + ").");
        requestErrorOccurred = true;
      }
    }
    if(callback != null)
      callback(requestErrorOccurred);
  }

}