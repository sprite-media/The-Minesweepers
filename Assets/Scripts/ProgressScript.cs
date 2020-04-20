using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProgressScript : MonoBehaviour
{

    public static ProgressScript instance { get; private set; }

    int progress = 0;
    string userid;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void setUserid(string id) { userid = id; }

    public void setProgress(int x) { progress = 0; progress += x; }

    public void UpdateProgress()
    {
        StartCoroutine(SentProgressInfo());
    }

    IEnumerator SentProgressInfo()
    {
        string jsonString = "{\"username\":\"" + userid + "\",\"progress\":\"" + progress + "\"}";

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(jsonString);

        UnityWebRequest www = UnityWebRequest.Put("https://db5fu151ej.execute-api.us-east-1.amazonaws.com/default/MinesweeperUpdateProgress", myData);

        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}