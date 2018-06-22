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
  private static System.Random rnd = new System.Random();
  private static List<Message> fallbackMessages = new List<Message>()
  {
    new Message("Our greatest weakness lies in giving up. The most certain way to succeed is always to try just one more time.", "Thomas A. Edison"),
    new Message("Never, never, never give up.", "Winston Churchill"),
    new Message("You must do the thing you think you cannot do.", "Eleanor Roosevelt"),
    new Message("Accept the challenges so that you can feel the exhilaration of victory.", "George S. Patton"),
    new Message("Even if you fall on your face, you’re still moving forward.", "Victor Kiam"),
    new Message("You will never do anything in this world without courage. It is the greatest quality in the mind next to honor.", "Aristotle"),
    new Message("We may encounter many defeats but we must not be defeated.", "Maya Angelou"),
    new Message("In order to succeed, we must first believe that we can.", "Nikos Kazantzakis"),
  };

  public static IEnumerator GetRandomMessage(System.Action<Message> callback)
  {
    Message msg;

    UnityWebRequest request = UnityWebRequest.Get(url_root + "/messages/0/");
    yield return request.SendWebRequest();

    if (request.isNetworkError || request.isHttpError)
    {
      Debug.Log("Something went wrong, and returned error: " + request.error);
      msg = GetFallbackMessage();
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
        msg = GetFallbackMessage();
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
  public static Message GetFallbackMessage()
  {
    int randomIdx = rnd.Next(fallbackMessages.Count);
    return fallbackMessages[randomIdx];
  }
}